using System.ComponentModel;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Implemented by attributes that describe the intent of a data member for components that infer
    /// their configuration from bound data. Infrastructure for Ignite UI component libraries built on this package.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IDataIntentAttribute
    {
        /// <summary>The intent of the annotated data member.</summary>
        string Intent { get; }
    }

    /// <summary>
    /// Describes the intent of a data member for components that infer their configuration from bound data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class DataIntentAttribute
        : Attribute, IDataIntentAttribute
    {
        /// <summary>Marks the member with <paramref name="intent"/>.</summary>
        /// <param name="intent">The intent of the annotated data member.</param>
        public DataIntentAttribute(string intent)
        {
            Intent = intent;
        }

        /// <inheritdoc />
        public string Intent { get; private set; }
    }
}
