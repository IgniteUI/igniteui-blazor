using System.ComponentModel;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Decides whether a property is written while a description is serialized for the client renderer.
    /// Infrastructure for Ignite UI component libraries built on this package; not intended for application code.
    /// </summary>
    /// <param name="name">The renderer name of the object being serialized.</param>
    /// <param name="property">The name of the property about to be written.</param>
    /// <returns><see langword="true"/> to write the property; otherwise <see langword="false"/>.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public delegate bool SerializationFilter(string? name, string? property);

    /// <summary>
    /// The writer and filter used while a description is serialized for the client renderer.
    /// Infrastructure for Ignite UI component libraries built on this package; not intended for application code.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class SerializationContext
    {
        /// <summary>The JSON writer the description is written to.</summary>
        public System.Text.Json.Utf8JsonWriter Writer { get; set; }

        /// <summary>An optional filter that decides which properties are written.</summary>
        public SerializationFilter? Filter { get; set; }

        /// <summary>Creates a context over <paramref name="writer"/>.</summary>
        /// <param name="writer">The JSON writer the description is written to.</param>
        /// <param name="filter">An optional filter that decides which properties are written.</param>
        public SerializationContext(System.Text.Json.Utf8JsonWriter writer, SerializationFilter? filter)
        {
            Writer = writer;
            Filter = filter;
        }
    }

    /// <summary>
    /// An object that writes itself into the description sent to the client renderer.
    /// Infrastructure for Ignite UI component libraries built on this package; not intended for application code.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface JsonSerializable
    {
        /// <summary>Writes this object to <paramref name="writer"/>.</summary>
        /// <param name="writer">The serialization context to write to.</param>
        /// <param name="propertyName">The property name to write the object under, or <see langword="null"/> to write a bare value.</param>
        void Serialize(SerializationContext writer, string? propertyName = null);
    }

}
