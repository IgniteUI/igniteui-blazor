namespace IgniteUI.Blazor.Controls
{
    internal delegate bool SerializationFilter(string? name, string? property);

    internal class SerializationContext
    {
        public System.Text.Json.Utf8JsonWriter Writer { get; set; }
        public SerializationFilter? Filter { get; set; }

        public SerializationContext(System.Text.Json.Utf8JsonWriter writer, SerializationFilter? filter)
        {
            Writer = writer;
            Filter = filter;
        }
    }

    internal interface JsonSerializable
    {
        void Serialize(SerializationContext writer, string? propertyName = null);
    }

}
