using System.Text.Json.Serialization;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Source-generated serializer context for the closed set of JSON shapes exchanged with the
    /// client renderer. Deserialized <c>object</c> values surface as <see cref="System.Text.Json.JsonElement"/>,
    /// matching the reflection-based serializer behavior.
    /// </summary>
    [JsonSerializable(typeof(Dictionary<string, object>))]
    [JsonSerializable(typeof(Dictionary<string, object>[]))]
    [JsonSerializable(typeof(object))]
    [JsonSerializable(typeof(object[]))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(string[]))]
    [JsonSerializable(typeof(int[]))]
    [JsonSerializable(typeof(double[]))]
    internal partial class IgbJsonContext : JsonSerializerContext
    {
    }
}
