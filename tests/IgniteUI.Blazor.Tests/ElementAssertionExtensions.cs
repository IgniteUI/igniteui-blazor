namespace IgniteUI.Blazor.Tests;

/// <summary>Shared element assertions used across the component suites.</summary>
internal static class ElementAssertionExtensions
{
    public static void Should_Exist(this AngleSharp.Dom.IElement element)
    {
        Assert.NotNull(element);
    }
}
