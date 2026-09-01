using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Lite.AotSmoke
{
    internal enum SmokeKind
    {
        Alpha,
        Beta,
    }

    // The documented consumer pattern from docs/TRIMMING.md — data item types must be preserved.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicFields)]
    internal class SmokeItem
    {
        public int Id { get; set; }
        public double Ratio { get; set; }
        public string Name { get; set; }
        public DateTime Stamp { get; set; }
        public bool Flag { get; set; }
        public decimal Price { get; set; }
        public SmokeKind Kind { get; set; }
        public int? Count { get; set; }
        public DateTime? Seen { get; set; }
        public long Big;
    }

    internal static class Program
    {
        private static int _checks;

        private static void Check(bool condition, string what)
        {
            if (!condition)
            {
                throw new InvalidOperationException("FAILED: " + what);
            }
            _checks++;
        }

        private static int IndexOf(string[] names, string name)
        {
            var index = Array.IndexOf(names, name);
            Check(index >= 0, $"schema contains '{name}'");
            return index;
        }

        private static int Main()
        {
            try
            {
                var item = new SmokeItem
                {
                    Id = 42,
                    Ratio = 2.5,
                    Name = "smoke",
                    Stamp = new DateTime(2026, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    Flag = true,
                    Price = 9.99m,
                    Kind = SmokeKind.Beta,
                    Count = 7,
                    Seen = null,
                    Big = 1234567890123L,
                };

                // Reflection-built schema over a user POCO: expression getters compile (interpreted under ILC).
                var schema = JSDataSourceSchema.Create(typeof(SmokeItem));

                object Untyped(string name) => schema.PropertyGetters[IndexOf(schema.PropertyNames, name)](item);
                Delegate Typed(string name) => schema.TypedPropertyGetters[IndexOf(schema.PropertyNames, name)];

                Check((int)Untyped("Id") == 42, "untyped int getter");
                Check((string)Untyped("Name") == "smoke", "untyped string getter");
                Check(Untyped("Seen") == null, "untyped null nullable getter");
                Check((SmokeKind)Untyped("Kind") == SmokeKind.Beta, "untyped enum getter boxes the enum");

                // The closed-set typed getters — the exact casts UnmarshalledDataSource.CreateColumn performs.
                Check(((Func<object, int>)Typed("Id"))(item) == 42, "typed int getter");
                Check(((Func<object, double>)Typed("Ratio"))(item) == 2.5, "typed double getter");
                Check(((Func<object, string>)Typed("Name"))(item) == "smoke", "typed string getter");
                Check(((Func<object, DateTime>)Typed("Stamp"))(item) == item.Stamp, "typed DateTime getter");
                Check(((Func<object, bool>)Typed("Flag"))(item), "typed bool getter");
                Check(((Func<object, decimal>)Typed("Price"))(item) == 9.99m, "typed decimal getter");
                Check(((Func<object, int>)Typed("Kind"))(item) == (int)SmokeKind.Beta, "typed enum getter converts to underlying");
                Check(((Func<object, int?>)Typed("Count"))(item) == 7, "typed nullable int getter");
                Check(((Func<object, DateTime?>)Typed("Seen"))(item) == null, "typed nullable DateTime getter");

                var bigIndex = IndexOf(schema.FieldNames, "Big");
                Check((long)schema.FieldGetters[bigIndex](item) == item.Big, "untyped field getter");
                Check(((Func<object, long>)schema.TypedFieldGetters[bigIndex])(item) == item.Big, "typed field getter (Delegate[] storage)");

                // Dictionary-shaped data: indexer reflection + typed dictionary getters.
                var dict = new Dictionary<string, object> { ["n"] = 5, ["s"] = "text", ["d"] = 1.25 };
                var dictSchema = JSDataSourceSchema.CreateFromDictionary(dict);
                Check(((Func<object, int>)dictSchema.TypedPropertyGetters[IndexOf(dictSchema.PropertyNames, "n")])(dict) == 5, "typed dictionary int getter");
                Check(((Func<object, string>)dictSchema.TypedPropertyGetters[IndexOf(dictSchema.PropertyNames, "s")])(dict) == "text", "typed dictionary string getter");
                Check((double)dictSchema.PropertyGetters[IndexOf(dictSchema.PropertyNames, "d")](dict) == 1.25, "untyped dictionary getter");

                // Data-source entry points.
                Check(UnmarshalledDataSource.ExtractSchema(new List<SmokeItem> { item }) != null, "ExtractSchema over a list");
                Check(UnmarshalledDataSource.ExtractSchemaFromType(typeof(SmokeItem[])) != null, "ExtractSchemaFromType over an array type");

                // Event-args factory + source-generated JSON round-trip.
                Check(MarshalByValueFactory.CreateInstance("FocusOptions") is IgbFocusOptions, "MarshalByValueFactory switch");
                var context = new IgbJsonContext(new JsonSerializerOptions());
                var json = JsonSerializer.Serialize(dict, context.DictionaryStringObject);
                var back = JsonSerializer.Deserialize(json, context.DictionaryStringObject);
                Check(back.Count == 3 && ((JsonElement)back["n"]).GetInt32() == 5, "IgbJsonContext round-trip");

                Console.WriteLine($"AOT smoke: OK ({_checks} checks)");
                // Magic success code (aspnetcore trimming-test convention): a silent early exit
                // with the default 0 cannot count as a pass.
                return 100;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("AOT smoke: " + ex);
                return 1;
            }
        }
    }
}
