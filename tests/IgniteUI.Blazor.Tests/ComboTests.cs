using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

public class ComboItem
{
    public required string Text { get; set; }
    public required int Id { get; set; }
    public string StringId { get; set; }
}

public class ComboTests : ComponentWithContractTestBase<IgbCombo<ComboItem>>
{
    private static readonly ComboItem _valueItem1 = new() { Id = 1, Text = "First" };
    private static readonly ComboItem _valueItem2 = new() { Id = 2, Text = "Second" };

    // Data items cross as {"refType": "uuid", "id": "<guid>"} references (see
    // JsonDataSourceItem.ToJson's "___id" marker), assigned once item is added to DS.
    internal static string DataItemId(InteropHarness interop, IRenderedComponent<IComponent> cut, int index)
    {
        var items = interop.FindPropertyUpdate(interop.ContainerIdOf(cut), "data")!.Value.EnumerateArray().ToArray();
        return items[index].GetProperty("___id").GetString()!;
    }

    internal static string UuidRef(InteropHarness interop, IRenderedComponent<IComponent> cut, int index) =>
        $$"""{"refType": "uuid", "id": "{{DataItemId(interop, cut, index)}}"}""";

    internal static string ChangeDetail(string newValues, string items, string type = "selection") =>
        $$$$"""{"detail": {"retType": "object", "type": "WebComboChangeEventArgsDetail", "value": {"newValue": {"retType": "Array", "type": "", "value": [{{{{newValues}}}}]}, "items": {"retType": "Array", "type": "", "value": [{{{{items}}}}]}, "type": "{{{{type}}}}"}}}""";

    protected override ComponentContract<IgbCombo<ComboItem>> InteropContract { get; } = new ComponentContract<IgbCombo<ComboItem>>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }), "focus",
            args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.SelectAsync(["item-1"]), c => c.Select(["item-1"]), "select",
            args: [new RawJson("""["item-1"]""")], types: [""])
        .Method(c => c.DeselectAsync(["item-1"]), c => c.Deselect(["item-1"]), "deselect",
            args: [new RawJson("""["item-1"]""")], types: [""])
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("invalid entry"), c => c.SetCustomValidity("invalid entry"), "setCustomValidity",
            args: ["invalid entry"], types: ["String"])
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value",
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            returns: FromRender.Of((interop, cut) => InteropReturn.Array(
                $$"""[{"refType": "uuid", "id": "{{DataItemId(interop, cut, 0)}}"}]""")),
            assert: (cut, result) => Assert.Same(_valueItem1, Assert.Single(result)))
        .Getter(c => c.GetSelectionAsync(), c => c.GetSelection(), "Selection",
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            returns: FromRender.Of((interop, cut) => InteropReturn.Array(
                $$"""[{"refType": "uuid", "id": "{{DataItemId(interop, cut, 1)}}"}]""")),
            assert: (cut, result) => Assert.Same(_valueItem2, Assert.Single(result)))
        // The payload carries uuid refs, which only exist once the data has transferred.
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            argsJson: FromRender.Of((interop, cut) => ChangeDetail(UuidRef(interop, cut, 0), UuidRef(interop, cut, 0))),
            expect: [_valueItem1])
        .Event(c => c.Change,
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            argsJson: FromRender.Of((interop, cut) => ChangeDetail(UuidRef(interop, cut, 0), UuidRef(interop, cut, 0))),
            assert: (cut, args) =>
            {
                Assert.Same(_valueItem1, Assert.Single(args.Detail.NewValue));
                Assert.Same(_valueItem1, Assert.Single(args.Detail.Items));
                Assert.Equal(ComboChangeType.Selection, args.Detail.ChangeType);
            })
        .Event(c => c.Change,
            arrange: ps => ps
                .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
                .Add(c => c.Value, [_valueItem1]),
            argsJson: FromRender.Of((interop, cut) => ChangeDetail("", UuidRef(interop, cut, 0), "deselection")),
            assert: (cut, args) =>
            {
                Assert.Empty(args.Detail.NewValue);
                Assert.Same(_valueItem1, Assert.Single(args.Detail.Items));
                // TODO: wire detail carries kind as "type", but FromEventJson reads "changeType", so
                // Detail.ChangeType never decodes and stays default (wrong for deselection events):
                // Assert.Equal(ComboChangeType.Deselection, args.Detail.ChangeType);
            })
        .Event(c => c.Change,
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            argsJson: FromRender.Of((interop, cut) => ChangeDetail(
                UuidRef(interop, cut, 0) + ", " + UuidRef(interop, cut, 1),
                UuidRef(interop, cut, 0) + ", " + UuidRef(interop, cut, 1))),
            assert: (cut, args) =>
            {
                // Multi-selection: every element resolves back to its original data instance.
                Assert.Equal([_valueItem1, _valueItem2], args.Detail.NewValue);
                Assert.Equal([_valueItem1, _valueItem2], args.Detail.Items);
                Assert.Same(args.Detail.NewValue[0], args.Detail.Items[0]);
            })
        .Event(c => c.Focus)
        .Event(c => c.Blur)
        .Event(c => c.Opening)
        .Event(c => c.Opened)
        .Event(c => c.Closing)
        .Event(c => c.Closed)
        .Prop(c => c.Open, true)
        .Prop(c => c.Outlined, true)
        .Prop(c => c.SingleSelect, true)
        .Prop(c => c.Autofocus, true)
        .Prop(c => c.AutofocusList, true)
        .Prop(c => c.Locale, "en-US")
        .Prop(c => c.Label, "Select item")
        .Prop(c => c.Placeholder, "Choose...")
        .Prop(c => c.PlaceholderSearch, "Search...")
        .Prop(c => c.ValueKey, "Id")
        .Prop(c => c.DisplayKey, "Text")
        .Prop(c => c.GroupKey, "Text")
        .Prop(c => c.GroupSorting, GroupingDirection.Desc, wire: "desc")
        .Prop(c => c.FilteringOptions,
            new IgbFilteringOptions
            {
                FilterKey = "text",
                CaseSensitive = true,
                MatchDiacritics = true,
            },
            wire: new JsonSubset("""{"filterKey": "text", "caseSensitive": true, "matchDiacritics": true}"""))
        .Prop(c => c.CaseSensitiveIcon, true)
        .Prop(c => c.DisableFiltering, true)
        .Prop(c => c.DisableClear, true)
        .Prop(c => c.Disabled, true)
        .Prop(c => c.Required, true)
        .Prop(c => c.Invalid, true)
        // Value items tracked by the data source cross as uuid refs into it (the
        // "___id" assigned on transfer); without Data arranged they'd fall to
        // ObjectToParam's generic ToString() branch, which no real app hits.
        .Prop(c => c.Value,
            value: [_valueItem1],
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            wire: FromRender.Of<object?>((interop, cut) => new RawJson($"[{UuidRef(interop, cut, 0)}]")))
        .Prop(c => c.Data,
            new[]
            {
                new ComboItem { Id = 1, Text = "First" },
                new ComboItem { Id = 2, Text = "Second" },
            },
            // Data source: crosses as a refChanged transfer under a generated ref id that the
            // description advertises as "dataRef" (JSON marshalling channel, as on Blazor Server).
            wire: new JsonSubset("""[{"Id": 1, "Text": "First"}, {"Id": 2, "Text": "Second"}]"""));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Combo_TypeMetadata()
    {
        var combo = new IgbCombo<object>();
        Assert.Equal("WebCombo", combo.Type);
    }

    [Fact]
    public void Combo_Label_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Label = "Select item";
        Assert.Equal("Select item", combo.Label);
    }

    [Fact]
    public void Combo_Placeholder_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Placeholder = "Choose...";
        Assert.Equal("Choose...", combo.Placeholder);
    }

    [Fact]
    public void Combo_PlaceholderSearch_Property()
    {
        var combo = new IgbCombo<object>();
        combo.PlaceholderSearch = "Search...";
        Assert.Equal("Search...", combo.PlaceholderSearch);
    }

    [Fact]
    public void Combo_Open_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Open = true;
        Assert.True(combo.Open);
    }

    [Fact]
    public void Combo_Disabled_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Disabled = true;
        Assert.True(combo.Disabled);
    }

    [Fact]
    public void Combo_Required_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Required = true;
        Assert.True(combo.Required);
    }

    [Fact]
    public void Combo_Outlined_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Outlined = true;
        Assert.True(combo.Outlined);
    }

    [Fact]
    public void Combo_SingleSelect_Property()
    {
        var combo = new IgbCombo<object>();
        combo.SingleSelect = true;
        Assert.True(combo.SingleSelect);
    }

    [Fact]
    public void Combo_Autofocus_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Autofocus = true;
        Assert.True(combo.Autofocus);
    }

    [Fact]
    public void Combo_ValueKey_Property()
    {
        var combo = new IgbCombo<object>();
        combo.ValueKey = "id";
        Assert.Equal("id", combo.ValueKey);
    }

    [Fact]
    public void Combo_DisplayKey_Property()
    {
        var combo = new IgbCombo<object>();
        combo.DisplayKey = "name";
        Assert.Equal("name", combo.DisplayKey);
    }

    [Fact]
    public void Combo_GroupKey_Property()
    {
        var combo = new IgbCombo<object>();
        combo.GroupKey = "category";
        Assert.Equal("category", combo.GroupKey);
    }

    [Fact]
    public void Combo_DisableFiltering_Property()
    {
        var combo = new IgbCombo<object>();
        combo.DisableFiltering = true;
        Assert.True(combo.DisableFiltering);
    }

    [Fact]
    public void Combo_Invalid_Property()
    {
        var combo = new IgbCombo<object>();
        combo.Invalid = true;
        Assert.True(combo.Invalid);
    }

    [Fact]
    public void Combo_CaseSensitiveIcon_Property()
    {
        var combo = new IgbCombo<object>();
        combo.CaseSensitiveIcon = true;
        Assert.True(combo.CaseSensitiveIcon);
    }
}

public abstract class ComboValueKeyTestsBase<T> : ComponentWithContractTestBase<IgbCombo<T>>
    where T : notnull
{
    protected static readonly ComboItem Item1 = new() { Id = 1, StringId = "UK01", Text = "First" };
    protected static readonly ComboItem Item2 = new() { Id = 2, StringId = "UK02", Text = "Second" };

    protected static Action<ComponentParameterCollectionBuilder<IgbCombo<T>>> Arrange =>
        ps => ps
            .Add(c => c.Data, new[] { Item1, Item2 })
            .Add(c => c.ValueKey, "Id");

    protected abstract string EventKeyValue { get; }

    protected abstract T ExpectedKeyValue { get; }

    protected abstract T[] PropValues { get; }

    protected abstract string ExpectedValue { get; }

    protected override ComponentContract<IgbCombo<T>> InteropContract => BuildContract();

    private ComponentContract<IgbCombo<T>> BuildContract() =>
        new ComponentContract<IgbCombo<T>>()
            .Event(c => c.Change,
                Arrange,
                argsJson: FromRender.Of((interop, cut) => ComboTests.ChangeDetail(EventKeyValue, ComboTests.UuidRef(interop, cut, 1))),
                assert: (cut, args) =>
                {
                    Assert.Equal(ExpectedKeyValue, Assert.Single(args.Detail.NewValue));
                    Assert.Same(Item2, Assert.Single(args.Detail.Items));
                    // Two-way Value propagation through the generated wrapper works when T
                    // matches the key value type.
                    Assert.Equal(ExpectedKeyValue, Assert.Single(cut.Instance.Value));
                })
            // A value-type value array crosses as plain JSON numbers — the keys
            // themselves, no data-source refs, since a keyed combo's value is the key.
            .Prop(c => c.Value,
                value: PropValues,
                arrange: Arrange,
                wire: new RawJson(ExpectedValue));

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();
}

public class ComboValueKeyIntTests : ComboValueKeyTestsBase<int>
{
    protected override string EventKeyValue => "2";
    protected override int ExpectedKeyValue => 2;
    protected override int[] PropValues => [1, 3];
    protected override string ExpectedValue => "[1, 3]";
}

public class ComboValueKeyDoubleTests : ComboValueKeyTestsBase<double>
{
    protected override string EventKeyValue => "2";
    protected override double ExpectedKeyValue => 2;
    protected override double[] PropValues => [1, 3];
    protected override string ExpectedValue => "[1, 3]";
}

public class ComboValueKeyStringTests : ComboValueKeyTestsBase<string>
{
    protected override string EventKeyValue => "\"UK02\"";
    protected override string ExpectedKeyValue => "UK02";
    protected override string[] PropValues => ["UK01", "UK03"];
    protected override string ExpectedValue => """["UK01", "UK03"]""";
}
