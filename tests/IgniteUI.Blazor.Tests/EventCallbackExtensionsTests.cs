using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

/// <summary> Unit coverage for <see cref="EventCallbackExtensions"/>. </summary>
public class EventCallbackExtensionsTests
{
    [Fact]
    public void HasHandler_IsFalse_ForBothWaysOfSayingNoHandler()
    {
        Assert.False(default(EventCallback<string>).HasHandler());
        Assert.False(EventCallback<string>.Empty.HasHandler());
    }

    [Fact]
    public void HasHandler_IsTrue_ForABoundCallback()
    {
        var handler = new Handler();
        Assert.True(new EventCallback<string>(handler, (Action<string>)handler.Handle).HasHandler());
    }

    [Fact]
    public void HasHandler_IsTrue_ForANoOpThatIsNotTheEmptySingleton()
    {
        // Only that one singleton reads as unbound: a no-op the consumer passed is a handler, and a
        // receiverless callback (what a plain lambda parameter produces) is still bound.
        Assert.True(new EventCallback<string>(null, (Action<string>)(_ => { })).HasHandler());
    }

    /// <summary> Stands in for a component. </summary>
    private sealed class Handler : IHandleEvent
    {
        public int Handled { get; private set; }

        public void Handle(string value) => Handled++;

        public Task HandleEventAsync(EventCallbackWorkItem item, object? arg) => item.InvokeAsync(arg);
    }
}
