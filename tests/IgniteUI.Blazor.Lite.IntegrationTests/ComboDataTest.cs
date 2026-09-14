using System.Text.Json;
using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// Combo data-binding scenarios against the WebAssembly-rendered TestBed page
    /// (<c>/combo-data</c>). Unlike the Server-rendered component sweep, data here crosses
    /// the in-process (unmarshalled) channel — the same path a production WASM app uses.
    /// Assertions read the live web component's <c>data</c>: what the user's combo shows.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class ComboDataTest : BlazorPageTest<Program>
    {
        [Test]
        public async Task TypedFields_TransferToClient()
        {
            await OpenScenarioAsync("typed-products", expectedCount: 3);

            var first = (await ClientDataAsync())[0];
            Assert.Multiple(() =>
            {
                Assert.That(first.GetProperty("Id").GetInt32(), Is.EqualTo(1));
                Assert.That(first.GetProperty("Name").GetString(), Is.EqualTo("Chai"));
                Assert.That(first.GetProperty("Price").GetDouble(), Is.EqualTo(18.5));
                Assert.That(first.GetProperty("Discontinued").GetBoolean(), Is.False);
                Assert.That(first.GetProperty("UnitsSold").GetInt64(), Is.EqualTo(42_000_000_000));
                // The client holds a JS Date; compare instants so the machine's timezone drops out.
                Assert.That(ClientInstant(first.GetProperty("Restocked")), Is.EqualTo(new DateTime(2026, 1, 15).ToUniversalTime()));
            });
        }

        [Test]
        public async Task NullableFields_PreserveNullsAndValues()
        {
            await OpenScenarioAsync("nullable-readings", expectedCount: 3);

            var data = await ClientDataAsync();
            var full = data[0];
            var empty = data[1];
            var mixed = data[2];
            Assert.Multiple(() =>
            {
                Assert.That(full.GetProperty("Quantity").GetInt32(), Is.EqualTo(5));
                Assert.That(full.GetProperty("Value").GetDouble(), Is.EqualTo(2.5));
                Assert.That(full.GetProperty("Flagged").GetBoolean(), Is.True);
                Assert.That(ClientInstant(full.GetProperty("MeasuredOn")), Is.EqualTo(new DateTime(2026, 3, 3).ToUniversalTime()));

                Assert.That(empty.GetProperty("Quantity").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(empty.GetProperty("Value").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(empty.GetProperty("Flagged").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(empty.GetProperty("MeasuredOn").ValueKind, Is.EqualTo(JsonValueKind.Null));

                Assert.That(mixed.GetProperty("Quantity").GetInt32(), Is.EqualTo(7));
                Assert.That(mixed.GetProperty("Value").ValueKind, Is.EqualTo(JsonValueKind.Null));
                Assert.That(mixed.GetProperty("Flagged").GetBoolean(), Is.False);
            });
        }

        [Test]
        public async Task NestedObjects_TransferNestedValues()
        {
            await OpenScenarioAsync("nested-orders", expectedCount: 2);

            var data = await ClientDataAsync();
            Assert.Multiple(() =>
            {
                Assert.That(data[0].GetProperty("Reference").GetString(), Is.EqualTo("ORD-1"));
                Assert.That(data[0].GetProperty("Customer").GetProperty("Name").GetString(), Is.EqualTo("Maria"));
                Assert.That(data[1].GetProperty("Customer").GetProperty("City").GetString(), Is.EqualTo("Madrid"));
            });
        }

        [Test]
        public async Task ObjectValues_SelectionTravelsBackAsDataItems()
        {
            await OpenScenarioAsync("objects-as-values", expectedCount: 3);

            await Page.EvaluateAsync("() => document.querySelector('igc-combo').show()");
            // Playwright's own click fails its actionability check on the combo's
            // shadow-DOM items, so click through the DOM.
            await Page.WaitForFunctionAsync("() => document.querySelector('igc-combo').shadowRoot.querySelector('igc-combo-item') !== null");
            await Page.EvaluateAsync("() => document.querySelector('igc-combo').shadowRoot.querySelector('igc-combo-item').click()");
            await Page.WaitForFunctionAsync(
                "() => window.clientPageRef.invokeMethodAsync('GetLastValue').then(v => v.count === 1)");

            var last = await Page.EvaluateAsync<JsonElement>(
                "() => window.clientPageRef.invokeMethodAsync('GetLastValue')");
            Assert.Multiple(() =>
            {
                Assert.That(last.GetProperty("names")[0].GetString(), Is.EqualTo("Chai"));
                Assert.That(last.GetProperty("sameInstances").GetBoolean(), Is.True,
                    "received values should resolve back to the bound data instances");
            });
        }

        [Test]
        [Ignore("Item types with public primitive-typed fields crash schema creation: JsonDataSourceSchema.Commit " +
            "stores the typed field getters in a Func<object, object>[] and throws ArrayTypeMismatchException " +
            "(TypedPropertyGetters uses Delegate[]). Enable once the field getter array type is fixed.")]
        public async Task NestedPublicFields_TransferToClient()
        {
            await OpenScenarioAsync("nested-field-shipments", expectedCount: 2);

            var data = await ClientDataAsync();
            Assert.Multiple(() =>
            {
                Assert.That(data[0].GetProperty("Box").GetProperty("Width").GetDouble(), Is.EqualTo(2.5));
                Assert.That(data[1].GetProperty("Box").GetProperty("Height").GetDouble(), Is.EqualTo(3.0));
            });
        }

        [Test]
        public async Task PrimitiveStrings_TransferAsItems()
        {
            await OpenScenarioAsync("primitive-strings", expectedCount: 3);

            var data = await ClientDataAsync();
            Assert.That(data[0].GetString(), Is.EqualTo("alpha"));
            Assert.That(data[2].GetString(), Is.EqualTo("gamma"));
        }

        [Test]
        public async Task ObservableCollectionMutations_FlowToClient()
        {
            await OpenScenarioAsync("observable-products", expectedCount: 3);

            await MutateAsync("add");
            await WaitForItemCountAsync(4);
            var data = await ClientDataAsync();
            Assert.That(data[3].GetProperty("Name").GetString(), Is.EqualTo("Added"), "added item should appear last");

            await MutateAsync("removeFirst");
            await WaitForItemCountAsync(3);
            data = await ClientDataAsync();
            Assert.That(data[0].GetProperty("Name").GetString(), Is.EqualTo("Chang"), "remaining items should shift up on remove");

            await MutateAsync("replaceFirst");
            await Page.WaitForFunctionAsync("() => document.querySelector('igc-combo').data[0].Name === 'Replaced'");

            await MutateAsync("clear");
            await WaitForItemCountAsync(0);
        }

        [Test]
        public async Task DataSwap_ReplacesClientData()
        {
            await OpenScenarioAsync("typed-products", expectedCount: 3);

            await Page.EvaluateAsync(
                "() => window.clientPageRef.invokeMethodAsync('SetComboScenario', 'primitive-strings')");
            await Page.WaitForFunctionAsync("() => document.querySelector('igc-combo').data?.[0] === 'alpha'");
        }

        private async Task OpenScenarioAsync(string scenario, int expectedCount)
        {
            await Page.GotoAsync("http://localhost:5249/combo-data?scenario=" + scenario);
            await Page.WaitForFunctionAsync("() => !!window.clientPageRef");
            // Prerender is off on the page, so the combo holding data means the WASM
            // runtime is live and the transfer completed.
            await WaitForItemCountAsync(expectedCount);
        }

        private Task WaitForItemCountAsync(int count)
            => Page.WaitForFunctionAsync($"() => document.querySelector('igc-combo')?.data?.length === {count}");

        private async Task<JsonElement> ClientDataAsync()
        {
            var data = await Page.EvaluateAsync<JsonElement>("() => document.querySelector('igc-combo').data");
            Assert.That(data.ValueKind, Is.EqualTo(JsonValueKind.Array), "the combo should expose its data as an array");
            return data;
        }

        private Task MutateAsync(string action)
            => Page.EvaluateAsync($"() => window.clientPageRef.invokeMethodAsync('MutateComboData', '{action}')");

        /// <summary>Parses a client-side JS Date (reported as an ISO string) to its UTC instant.</summary>
        private static DateTime ClientInstant(JsonElement value)
            => DateTime.Parse(value.GetString()!, null, System.Globalization.DateTimeStyles.AdjustToUniversal);
    }
}
