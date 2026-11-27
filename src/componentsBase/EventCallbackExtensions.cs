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

        /// <summary>
        /// Whether <paramref name="right"/> is set and equal to <paramref name="left"/>: the same receiver and an
        /// equal delegate, so the fresh delegate Blazor hands over for the same handler on each render compares equal.
        /// <see cref="EventCallback{TValue}.Equals(object)"/> does this since .NET 10
        /// (<see href="https://github.com/dotnet/aspnetcore/issues/53361">dotnet/aspnetcore#53361</see>).
        /// </summary>
        internal static bool EqualsCompat<TValue>(this EventCallback<TValue> left, EventCallback<TValue>? right)
        {
            return right != null && left.Equals(right.Value);
        }
    }
}
