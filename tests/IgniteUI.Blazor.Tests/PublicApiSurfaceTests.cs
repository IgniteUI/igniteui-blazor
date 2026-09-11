using System.Reflection;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Guards the library's public API against obsolete members that used to be emitted for
/// every component by the legacy generation pipeline. <c>SetNativeElement</c>/
/// <c>SetNativeElementAsync</c> forwarded to a client-side <c>setNativeElement</c> hook that
/// is never reachable from Blazor (the element is owned by the renderer), had no XML
/// documentation, and had zero references — a single assembly-wide sweep keeps them from
/// coming back with a new or regenerated component.
/// </summary>
public class PublicApiSurfaceTests
{
    [Fact]
    public void PublicApi_DoesNotExposeSetNativeElement()
    {
        var offenders = typeof(IgbBanner).Assembly.GetExportedTypes()
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            .Where(m => m.Name is "SetNativeElement" or "SetNativeElementAsync")
            .Select(m => $"{m.DeclaringType?.FullName}.{m.Name}")
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();

        Assert.True(offenders.Count == 0,
            $"Obsolete setNativeElement wrappers are public again on: {string.Join(", ", offenders)}");
    }
}
