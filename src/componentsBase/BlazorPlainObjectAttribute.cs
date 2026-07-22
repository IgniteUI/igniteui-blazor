using System.ComponentModel;

namespace IgniteUI.Blazor.Controls
{
    /// <summary>
    /// Attribute used to mark a class as a plain object for Blazor serialization.
    /// Classes marked with this attribute will implement JsonSerializable and perform simple dirty tracking for serialization.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Class)]
    public class BlazorPlainObjectAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the attribute
        /// </summary>
        public BlazorPlainObjectAttribute()
        {
        }
    }
}
