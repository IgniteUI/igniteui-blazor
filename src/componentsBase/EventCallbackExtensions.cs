#if !NET10_0_OR_GREATER
using System.Reflection;
#endif
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
        /// Workaround for <see cref="EventCallback{TValue}.Equals(object)"/>.
        /// It has been fixed only in .NET ~9~ 10.(<see href="https://github.com/dotnet/aspnetcore/issues/53361">dotnet/aspnetcore#53361</see>).
        /// </summary>
        internal static bool EqualsCompat<TValue>(this EventCallback<TValue> left, EventCallback<TValue>? right)
        {
            if (right == null)
            {
                return false;
            }

            EventCallback<TValue> other = right.Value;
#if NET10_0_OR_GREATER
            return left.Equals(other);
#else
            if (!CallbackFields<TValue>.Resolved)
            {
                return false;
            }

            // Mirrors .NET 10's EventCallback<TValue>.Equals, need the internal fields for the checks:
            MulticastDelegate? leftDelegate = (MulticastDelegate?)CallbackFields<TValue>.Delegate.GetValue(left);
            MulticastDelegate? rightDelegate = (MulticastDelegate?)CallbackFields<TValue>.Delegate.GetValue(other);

            return ReferenceEquals(CallbackFields<TValue>.Receiver.GetValue(left), CallbackFields<TValue>.Receiver.GetValue(other))
                && (leftDelegate?.Equals(rightDelegate) ?? (rightDelegate == null));
#endif
        }

#if !NET10_0_OR_GREATER
        /// <summary>Resolved once per closed callback type, rather than per component instance.</summary>
        private static class CallbackFields<TValue>
        {
            internal static readonly FieldInfo Delegate = Field("Delegate");
            internal static readonly FieldInfo Receiver = Field("Receiver");

            /// <summary> Use as guard against (unlikely) fields rename in old versions that won't resolve. </summary>
            internal static readonly bool Resolved = Delegate != null && Receiver != null;

            private static FieldInfo Field(string name) =>
                typeof(EventCallback<TValue>).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
        }
#endif
    }
}
