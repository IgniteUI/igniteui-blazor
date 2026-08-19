using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Controls
{
    internal static class EventCallbackExtensions
    {
        /// <summary>
        /// Whether the callback is a live subscription: not <c>default</c>, and not the
        /// <see cref="EventCallback{TValue}.Empty"/> singleton, whose no-op delegate passes
        /// <see cref="EventCallback{TValue}.HasDelegate"/>.
        /// </summary>
        internal static bool HasHandler<TValue>(this EventCallback<TValue> callback) =>
            callback.HasDelegate && !EventCallback<TValue>.Empty.Equals(callback);
    }
}
