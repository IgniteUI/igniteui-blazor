using System;

namespace IgniteUI.Blazor.Controls
{
    public delegate bool SerializationFilter(string name, string property);

    public class SerializationContext
    {
        public System.Text.Json.Utf8JsonWriter Writer { get; set; }
        public SerializationFilter Filter { get; set; }
        
        public SerializationContext(System.Text.Json.Utf8JsonWriter writer, SerializationFilter filter)
        {
            Writer = writer;
            Filter = filter;
        }
    }

    public interface JsonSerializable {
        void Serialize(SerializationContext writer, string propertyName = null);
    }

}
