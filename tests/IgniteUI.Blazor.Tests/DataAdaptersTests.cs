using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// <see cref="LocalJson{TItem}"/> and <see cref="RemoteJson{TItem}"/> are empty
/// <see cref="IEnumerable{TItem}"/> sequences, so they are directly assignable to a strongly-typed
/// data parameter such as <see cref="IgbCombo{TValue, TItem}.Data"/> without any cast. The interop
/// layer still recognizes them (via the non-generic base class) and substitutes the wrapped
/// JSON/URL reference instead of enumerating the value.
/// </summary>
public class DataAdaptersTests : BlazorComponentTestBase
{
    [Fact]
    public void LocalJsonOfTItem_AssignableToComboData_RendersWithoutError()
    {
        Interop.PrimeReady();
        var cut = Render<IgbCombo<string, ComboItem>>(ps => ps
            .Add(c => c.Data, LocalJson<ComboItem>.From("""[{"Id":1,"Text":"First"}]""")));

        Assert.NotNull(cut.Instance.Data);
    }

    [Fact]
    public void RemoteJsonOfTItem_AssignableToComboData_RendersWithoutError()
    {
        Interop.PrimeReady();
        var cut = Render<IgbCombo<string, ComboItem>>(ps => ps
            .Add(c => c.Data, RemoteJson<ComboItem>.From("https://example.test/items.json")));

        Assert.NotNull(cut.Instance.Data);
    }

    [Fact]
    public void LocalJsonOfTItem_ToRef_MatchesNonGenericLocalJson()
    {
        const string json = """[{"Id":1,"Text":"First"}]""";
        var typed = LocalJson<ComboItem>.From(json);
        var untyped = LocalJson.From(json);
        Assert.Equal(untyped.ToRef(), typed.ToRef());
        Assert.StartsWith("localJson:::", typed.ToRef());
    }

    [Fact]
    public void RemoteJsonOfTItem_ToRef_MatchesNonGenericRemoteJson()
    {
        const string uri = "https://example.test/items.json";
        var typed = RemoteJson<ComboItem>.From(uri);
        var untyped = RemoteJson.From(uri);
        Assert.Equal(untyped.ToRef(), typed.ToRef());
        Assert.StartsWith("json:::", typed.ToRef());
    }

    [Fact]
    public void LocalJsonOfTItem_EnumeratesAsEmpty()
    {
        var json = LocalJson<ComboItem>.From("[]");
        Assert.Empty(json);
        Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<ComboItem>>(json);
        // Still recognized by the interop layer as a LocalJson, via inheritance.
        Assert.IsAssignableFrom<LocalJson>(json);
    }

    [Fact]
    public void RemoteJsonOfTItem_EnumeratesAsEmpty()
    {
        var json = RemoteJson<ComboItem>.From("https://example.test/items.json");
        Assert.Empty(json);
        Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<ComboItem>>(json);
        Assert.IsAssignableFrom<RemoteJson>(json);
    }
}
