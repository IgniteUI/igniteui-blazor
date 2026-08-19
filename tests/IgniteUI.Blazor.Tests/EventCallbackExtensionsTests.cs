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

    [Fact]
    public void EqualsCompat_IsFalse_WhenTheOtherWasNeverAssigned()
    {
        var handler = new Handler();

        Assert.False(new EventCallback<string>(handler, (Action<string>)handler.Handle).EqualsCompat(null));
        Assert.False(default(EventCallback<string>).EqualsCompat(null));
    }

    [Fact]
    public void EqualsCompat_IsTrue_ForAFreshDelegateOverTheSameTargetAndMethod()
    {
        var handler = new Handler();
        Action<string> first = handler.Handle;
        Action<string> second = handler.Handle;
        // Guards the test from going vacuous if a compiler ever starts caching these conversions.
        Assert.NotSame(first, second);

        Assert.True(
            new EventCallback<string>(handler, first).EqualsCompat(new EventCallback<string>(handler, second)),
            "a re-render's equivalent callback compared unequal, so every render would re-register");
    }

    [Fact]
    public void EqualsCompat_IsFalse_ForADifferentMethodOnTheSameTarget()
    {
        var handler = new Handler();

        Assert.False(
            new EventCallback<string>(handler, (Action<string>)handler.Handle)
                .EqualsCompat(new EventCallback<string>(handler, (Action<string>)handler.HandleDifferently)));
    }

    [Fact]
    public void EqualsCompat_IsFalse_ForTheSameMethodOnADifferentTarget()
    {
        var one = new Handler();
        var two = new Handler();

        Assert.False(
            new EventCallback<string>(one, (Action<string>)one.Handle)
                .EqualsCompat(new EventCallback<string>(two, (Action<string>)two.Handle)));
    }

    [Fact]
    public void EqualsCompat_IsFalse_WhenOnlyTheReceiverDiffers()
    {
        var handler = new Handler();
        Action<string> shared = handler.Handle;

        Assert.False(
            new EventCallback<string>(handler, shared).EqualsCompat(new EventCallback<string>(new Handler(), shared)));
    }

    [Fact]
    public void EqualsCompat_SeparatesEmptyFromDefault()
    {
        Assert.False(EventCallback<string>.Empty.EqualsCompat(default(EventCallback<string>)));
        Assert.False(default(EventCallback<string>).EqualsCompat(EventCallback<string>.Empty));
        Assert.True(EventCallback<string>.Empty.EqualsCompat(EventCallback<string>.Empty));
        Assert.True(default(EventCallback<string>).EqualsCompat(default(EventCallback<string>)));
    }

    /// <summary> Stands in for a component. </summary>
    private sealed class Handler : IHandleEvent
    {
        public int Handled { get; private set; }

        public void Handle(string value) => Handled++;

        public void HandleDifferently(string value) => Handled--;

        public Task HandleEventAsync(EventCallbackWorkItem item, object? arg) => item.InvokeAsync(arg);
    }
}
