using System.Text.Json;
using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

public class ComboItem
{
    public required string Text { get; set; }
    public required int Id { get; set; }
    public string? StringId { get; set; }
}

public class ComboTests : ComponentWithContractTestBase<IgbCombo<ComboItem, ComboItem>>
{
    private static readonly ComboItem _valueItem1 = new() { Id = 1, Text = "First" };
    private static readonly ComboItem _valueItem2 = new() { Id = 2, Text = "Second" };

    // Data items cross as {"refType": "uuid", "id": "<guid>"} references (see
    // JsonDataSourceItem.ToJson's "___id" marker), assigned once item is added to DS.
    internal static string DataItemId(InteropHarness interop, IRenderedComponent<IComponent> cut, int index)
    {
        var dataUpdate = interop.FindPropertyUpdate(interop.ContainerIdOf(cut), "data");
        if (dataUpdate is not { } data)
        {
            return string.Empty;
        }

        var items = data.EnumerateArray().ToArray();
        return items[index].GetProperty("___id").GetString() ?? string.Empty;
    }

    internal static string UuidRef(InteropHarness interop, IRenderedComponent<IComponent> cut, int index) =>
        $$"""{"refType": "uuid", "id": "{{DataItemId(interop, cut, index)}}"}""";

    internal static string ClosingDetail(string trigger) =>
        $$$$"""{"detail": {"retType": "object", "type": "WebComboClosingEventArgsDetail", "value": {"trigger": "{{{{trigger}}}}"}}}""";

    internal static string ChangeDetail(string newValues, string items, string type = "selection") =>
        $$$$"""{"detail": {"retType": "object", "type": "WebComboChangeEventArgsDetail", "value": {"newValue": {"retType": "Array", "type": "", "value": [{{{{newValues}}}}]}, "items": {"retType": "Array", "type": "", "value": [{{{{items}}}}]}, "type": "{{{{type}}}}"}}}""";

    protected override ComponentContract<IgbCombo<ComboItem, ComboItem>> InteropContract { get; } = new ComponentContract<IgbCombo<ComboItem, ComboItem>>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }), "focus",
            args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.SelectAsync([_valueItem1]), c => c.Select([_valueItem1]), "select",
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            args: [FromRender.Of<object?>((interop, cut) => new RawJson($"[{UuidRef(interop, cut, 0)}]"))], types: [""])
        .Method(c => c.DeselectAsync([_valueItem1]), c => c.Deselect([_valueItem1]), "deselect",
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            args: [FromRender.Of<object?>((interop, cut) => new RawJson($"[{UuidRef(interop, cut, 0)}]"))], types: [""])
        .Method(c => c.SelectAllItemsAsync(false), c => c.SelectAllItems(false), "selectAllItems", args: [false], types: ["Boolean"])
        .Method(c => c.SelectAllItemsAsync(true), c => c.SelectAllItems(true), "selectAllItems", args: [true], types: ["Boolean"])
        .Method(c => c.DeselectAllItemsAsync(false), c => c.DeselectAllItems(false), "deselectAllItems", args: [false], types: ["Boolean"])
        .Method(c => c.DeselectAllItemsAsync(true), c => c.DeselectAllItems(true), "deselectAllItems", args: [true], types: ["Boolean"])
        .Method(c => c.CommitSelectionAsync(), c => c.CommitSelection(), "commitSelection")
        .Method(c => c.CancelSelectionAsync(), c => c.CancelSelection(), "cancelSelection")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: false)
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("invalid entry"), c => c.SetCustomValidity("invalid entry"), "setCustomValidity",
            args: ["invalid entry"], types: ["String"])
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value",
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            returns: FromRender.Of((interop, cut) => InteropReturn.Array(
                $$"""[{"refType": "uuid", "id": "{{DataItemId(interop, cut, 0)}}"}]""")),
            assert: (cut, result) => Assert.Same(_valueItem1, Assert.Single(result)))
        .Getter(c => c.GetPendingValueAsync(), c => c.GetPendingValue(), "PendingValue",
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            returns: FromRender.Of((interop, cut) => InteropReturn.Array(
                $$"""[{"refType": "uuid", "id": "{{DataItemId(interop, cut, 1)}}"}]""")),
            assert: (cut, result) => Assert.Same(_valueItem2, Assert.Single(result)))
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
        .Event(c => c.Closing,
            argsJson: ClosingDetail("outside-click"),
            assert: (cut, args) => Assert.Equal(ComboCloseTrigger.OutsideClick, args.Detail.Trigger))
        .Event(c => c.Closed,
            argsJson: ClosingDetail("apply"),
            assert: (cut, args) => Assert.Equal(ComboCloseTrigger.Apply, args.Detail.Trigger))
        .Event(c => c.PendingChange,
            arrange: ps => ps.Add(c => c.Data, new[] { _valueItem1, _valueItem2 }),
            argsJson: FromRender.Of((interop, cut) => ChangeDetail(UuidRef(interop, cut, 1), UuidRef(interop, cut, 1))),
            assert: (cut, args) =>
            {
                Assert.Same(_valueItem2, Assert.Single(args.Detail.NewValue));
                Assert.Equal(ComboChangeType.Selection, args.Detail.ChangeType);
            })
        .Event(c => c.SearchInput,
            argsJson: """{"detail": {"retType": "object", "type": "WebComboSearchInputEventArgsDetail", "value": {"searchText": "sec"}}}""",
            assert: (cut, args) => Assert.Equal("sec", args.Detail.SearchText))
        .Event(c => c.DataRequest,
            argsJson: """{"detail": {"retType": "object", "type": "WebVirtualScrollDataRequestEventArgsDetail", "value": {"startIndex": 50, "count": 25}}}""",
            assert: (cut, args) =>
            {
                Assert.Equal(50, args.Detail.StartIndex);
                Assert.Equal(25, args.Detail.Count);
            })
        .Prop(c => c.Open, true)
        .Prop(c => c.ScrollStrategy, PopoverScrollStrategy.Close, wire: "close")
        .Prop(c => c.Outlined, true)
        .Prop(c => c.SingleSelect, true)
        .Prop(c => c.Autofocus, true)
        .Prop(c => c.AutofocusList, true)
        .Prop(c => c.Locale, "en-US")
        .Prop(c => c.Label, "Select item")
        // The description's "name" is the renderer's id for the component, so Name crosses as "formName".
        .Prop(c => c.Name, "field", wire: "field", wireName: "formName")
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
        .Prop(c => c.FilteringOptions,
            new IgbFilteringOptions { Mode = ComboFilteringMode.Remote },
            wire: new JsonSubset("""{"mode": "remote"}"""))
        .Prop(c => c.CaseSensitiveIcon, true)
        .Prop(c => c.DisableFiltering, true)
        .Prop(c => c.DisableClear, true)
        .Prop(c => c.TotalItemCount, 250.0)
        .Prop(c => c.Loading, true)
        .Prop(c => c.ShowSelectAll, true)
        .Prop(c => c.GroupSelection, true)
        .Prop(c => c.SelectionCommit, ComboSelectionCommit.Confirm, wire: "confirm")
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
            wire: new JsonSubset("""[{"Id": 1, "Text": "First"}, {"Id": 2, "Text": "Second"}]"""))
        // The JSON escape hatches ride the same data ref as Data.
        .Prop(c => c.DataJson, LocalJson.From("""[{"Id": 1, "Text": "First"}]"""),
            wireName: "data",
            wire: new JsonSubset("""[{"Id": 1, "Text": "First"}]"""))
        .Prop(c => c.DataJson, RemoteJson.From("https://example.test/items.json"),
            wireName: "data",
            wire: "https://example.test/items.json");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Props_FollowContract() => VerifyPropContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Combo_RendersCorrectElement()
    {
        var cut = Render<IgbCombo<ComboItem, ComboItem>>();
        Assert.True(cut.Find("igc-combo").HasAttribute("data-ig-root"));
    }

    [Fact]
    public void Combo_TypeMetadata()
    {
        var combo = new IgbCombo<object, object>();
        Assert.Equal("WebCombo", combo.RendererType);
    }

    [Fact]
    public void Combo_Change_SelectionEvent_HasSelectionChangeType()
    {
        Interop.PrimeReady();
        IgbComboChangeEventArgs<ComboItem, ComboItem>? received = null;
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.Change, (IgbComboChangeEventArgs<ComboItem, ComboItem> args) => received = args));

        var argsJson = ChangeDetail(UuidRef(Interop, cut, 0), UuidRef(Interop, cut, 0));
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Change", argsJson);

        Assert.NotNull(received);
        Assert.Equal(ComboChangeType.Selection, received.Detail.ChangeType);
    }

    [Fact]
    public void Combo_Change_DeselectionEvent_HasDeselectionChangeType()
    {
        Interop.PrimeReady();
        IgbComboChangeEventArgs<ComboItem, ComboItem>? received = null;
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.Value, new[] { _valueItem1 })
            .Add(c => c.Change, (IgbComboChangeEventArgs<ComboItem, ComboItem> args) => received = args));

        var argsJson = ChangeDetail("", UuidRef(Interop, cut, 0), "deselection");
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Change", argsJson);

        Assert.NotNull(received);
        Assert.Equal(ComboChangeType.Deselection, received.Detail.ChangeType);
    }

    [Fact]
    public void Combo_DowncastArray_ElementsAlreadyOfTargetType_SkipsConvertChangeType()
    {
        // DowncastArray<T> (IgbComponentBase.cs): `objArr[i] is T item ? item : (T)Convert.ChangeType(...)`.
        // ComboItem does not implement IConvertible, so without this same-type fast path,
        // converting an object[] whose elements are already ComboItem instances - the normal
        // case once a uuid/name ref has resolved against the data source - would throw. The
        // fast path assigns the element directly, skipping Convert.ChangeType entirely.
        Interop.PrimeReady();
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 }));

        object[] alreadyTyped = [_valueItem1, _valueItem2];

        var result = cut.Instance.DowncastArray<ComboItem>(alreadyTyped);

        Assert.Equal(2, result.Length);
        Assert.Same(_valueItem1, result[0]);
        Assert.Same(_valueItem2, result[1]);
    }

    // The web component invokes both templates with a plain { item } context; the client hosts
    // each in an <igc-lite-template-content> and batches an Add followed by an Update carrying it.
    private static string TemplateBatch(string containerId, string templateId, string contentId, string itemJson) =>
        JsonSerializer.Serialize(new object[]
        {
            new { containerId, contentType = "TemplateContent", templateId, contentId, actionType = "Add", args = (string?)null },
            new
            {
                containerId, contentType = "TemplateContent", templateId, contentId, actionType = "Update",
                args = $$$"""{"retType": "object", "type": "", "value": {"item": {{{itemJson}}}}}""",
            },
        });

    private string TemplateId(IRenderedComponent<IComponent> cut, string wireName) =>
        Interop.FindPropertyUpdate(Interop.ContainerIdOf(cut), wireName)?.GetString()
            ?? throw new InvalidOperationException($"{wireName} was never transferred.");

    [Fact]
    public void Combo_ItemTemplate_ReceivesResolvedDataItem()
    {
        Interop.PrimeReady();
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.ItemTemplate, (ComboItem item) => b => b.AddContent(0, $"Item: {item.Text}")));

        var containerId = Interop.ContainerIdOf(cut);
        Interop.AdjustDynamicContentBatch(containerId,
            TemplateBatch(containerId, TemplateId(cut, "itemTemplate"), "content-1", UuidRef(Interop, cut, 1)));

        cut.WaitForAssertion(() => Assert.Contains("Item: Second", cut.Markup));
    }

    [Fact]
    public void Combo_ItemTemplate_ValueTypeItems_ReceiveItem()
    {
        Interop.PrimeReady();
        var cut = Render<IgbCombo<int, int>>(ps => ps
            // A List, as JsonDataSource.Create casts arrays to object[], which an int[] is not.
            .Add(c => c.Data, new List<int> { 1, 2, 3 })
            .Add(c => c.ItemTemplate, (int item) => b => b.AddContent(0, $"Number: {item}")));

        // Primitive items cross by value, not as uuid refs.
        var containerId = Interop.ContainerIdOf(cut);
        Interop.AdjustDynamicContentBatch(containerId,
            TemplateBatch(containerId, TemplateId(cut, "itemTemplate"), "content-1", "2"));

        cut.WaitForAssertion(() => Assert.Contains("Number: 2", cut.Markup));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Combo_ItemTemplate_ReplacingAClearedScript_KeepsTheTemplate(bool flushSync)
    {
        Interop.PrimeReady();
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.ItemTemplateScript, "ItemScript"));
        Assert.Equal("ItemScript", TemplateId(cut, "itemTemplate"));

        // Both land in one flush, after the description the clear queues: the template ref is sent
        // ahead of that description, so the clear has to move with it or it overwrites the template.
        Interop.ClearObserved();
        Interop.OnDispatcher(() =>
        {
            // Set directly so the two land in this order, and in the same flush as the sync call below.
#pragma warning disable BL0005
            cut.Instance.ItemTemplateScript = null;
            cut.Instance.ItemTemplate = item => b => b.AddContent(0, item.Text);
#pragma warning restore BL0005
            if (flushSync)
            {
                // A sync method call drains the queue through UpdateSync instead of Update.
                cut.Instance.BlurComponent();
            }
        });

        var sent = Interop.FindPropertyUpdate(Interop.ContainerIdOf(cut), "itemTemplate");
        Assert.Equal(JsonValueKind.String, sent?.ValueKind);
        Assert.NotEqual("ItemScript", sent?.GetString());
    }

    [Fact]
    public void Combo_GroupHeaderTemplate_ReceivesGroupKey()
    {
        Interop.PrimeReady();
        IgbComboGroupHeaderContext? received = null;
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.ValueKey, "Id")
            .Add(c => c.GroupKey, "Text")
            .Add(c => c.GroupHeaderTemplate, (IgbComboGroupHeaderContext ctx) => b =>
            {
                received = ctx;
                b.AddContent(0, $"Group: {ctx.Key}");
            }));

        // The context is { key, item }; item is a synthetic record, not a data item, and is ignored.
        var containerId = Interop.ContainerIdOf(cut);
        var templateId = TemplateId(cut, "groupHeaderTemplate");
        var batch = JsonSerializer.Serialize(new object[]
        {
            new { containerId, contentType = "TemplateContent", templateId, contentId = "content-1", actionType = "Add", args = (string?)null },
            new
            {
                containerId, contentType = "TemplateContent", templateId, contentId = "content-1", actionType = "Update",
                args = """{"retType": "object", "type": "", "value": {"key": true, "item": {"retType": "object", "type": "", "value": {"Id": true, "Text": true}}}}""",
            },
        });
        Interop.AdjustDynamicContentBatch(containerId, batch);

        // A boolean group value arrives as a bool, not a JsonElement.
        cut.WaitForAssertion(() => Assert.Contains("Group: True", cut.Markup));
        Assert.Equal(true, received?.Key);
    }

    [Fact]
    public void Combo_GroupHeaderTemplate_ReceivesKeyOfContext()
    {
        Interop.PrimeReady();
        IgbComboGroupHeaderContext? received = null;
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.GroupKey, "Text")
            .Add(c => c.GroupHeaderTemplate, (IgbComboGroupHeaderContext ctx) => b =>
            {
                received = ctx;
                b.AddContent(0, $"Group: {ctx.Key}");
            }));

        // Since 7.5 the web component passes { key, item }; key wins over the synthetic record.
        var containerId = Interop.ContainerIdOf(cut);
        var templateId = TemplateId(cut, "groupHeaderTemplate");
        var batch = JsonSerializer.Serialize(new object[]
        {
            new { containerId, contentType = "TemplateContent", templateId, contentId = "content-1", actionType = "Add", args = (string?)null },
            new
            {
                containerId, contentType = "TemplateContent", templateId, contentId = "content-1", actionType = "Update",
                args = """{"retType": "object", "type": "", "value": {"key": "Other", "item": {"retType": "object", "type": "", "value": {"Text": "Ignored"}}}}""",
            },
        });
        Interop.AdjustDynamicContentBatch(containerId, batch);

        cut.WaitForAssertion(() => Assert.Contains("Group: Other", cut.Markup));
        Assert.Equal("Other", received?.Key);
    }

    [Fact]
    public async Task Combo_SelectionMethods_PushTheNewValueToValueChanged()
    {
        // selectAllItems / commitSelection change the value without igcChange; the wrapper reads it back.
        Interop.PrimeReady();
        int[]? bound = null;
        var cut = Render<IgbCombo<int, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.ValueKey, "Id")
            .Add(c => c.ValueChanged, (int[] v) => bound = v));

        Interop.SetupPropertyRead("Value", InteropReturn.Array("[1, 2]"));
        await cut.InvokeAsync(() => cut.Instance.SelectAllItemsAsync());

        Assert.Equal([1, 2], bound!);
        Assert.Equal([1, 2], cut.Instance.Value);

        Interop.SetupPropertyRead("Value", InteropReturn.Array("[2]"));
        await cut.InvokeAsync(() => cut.Instance.CommitSelectionAsync());

        Assert.Equal([2], bound!);
    }

    [Fact]
    public void Combo_NewData_SelectedItemOfTheOldDataStillResolves()
    {
        // A new Data keeps the selection on the client, so its events still name the old item.
        Interop.PrimeReady();
        IgbComboChangeEventArgs<int, ComboItem>? received = null;
        var oldData = new[] { _valueItem1, _valueItem2 };
        var cut = Render<IgbCombo<int, ComboItem>>(ps => ps
            .Add(c => c.Data, oldData)
            .Add(c => c.ValueKey, "Id")
            .Add(c => c.Change, (IgbComboChangeEventArgs<int, ComboItem> args) => received = args));

        var oldRef = UuidRef(Interop, cut, 0);
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Change", ChangeDetail("1", oldRef));
        Assert.Equal([1], cut.Instance.Value);

        var newItem = new ComboItem { Id = 3, Text = "Third" };
        cut.Render(ps => ps.Add(c => c.Data, new[] { newItem }));

        // Deselecting the old item: Items names it by the uuid of the released data.
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Change", ChangeDetail("", oldRef, "deselection"));
        Assert.Same(_valueItem1, Assert.Single(received!.Detail.Items));

        // Once it left Value, a later Data swap drops it.
        cut.Render(ps => ps.Add(c => c.Data, new[] { _valueItem2 }));
        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Change", ChangeDetail("", oldRef, "deselection"));
        Assert.Null(Assert.Single(received!.Detail.Items));
    }

    [Theory]
    [InlineData("""{"detail": {"retType": "object", "type": "WebComboClosingEventArgsDetail", "value": {"trigger": "some-future-trigger"}}}""")]
    [InlineData("""{"detail": {"retType": "object", "type": "WebComboClosingEventArgsDetail", "value": {"trigger": "3"}}}""")]
    [InlineData("""{"detail": {"retType": "object", "type": "WebComboClosingEventArgsDetail", "value": {}}}""")]
    public void Combo_Closing_UnknownOrMissingTrigger_IsNull(string argsJson)
    {
        // Not reported as Enter, the first member, which a handler could take for a real Enter.
        Interop.PrimeReady();
        IgbComboClosingEventArgs? received = null;
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Closing, (IgbComboClosingEventArgs args) => received = args));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Closing", argsJson);

        Assert.NotNull(received);
        Assert.Null(received.Detail.Trigger);
    }

    [Fact]
    public void Combo_ClosingHandlerTakingVoidEventArgs_StillBinds()
    {
        Interop.PrimeReady();
        IgbVoidEventArgs? received = null;
        void OnClosing(IgbVoidEventArgs args) => received = args;
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Closing, (Action<IgbVoidEventArgs>)OnClosing));

        Interop.RaiseEvent(Interop.ContainerIdOf(cut), "Closing", ClosingDetail("escape"));

        var args = Assert.IsType<IgbComboClosingEventArgs>(received);
        Assert.Equal(ComboCloseTrigger.Escape, args.Detail.Trigger);
    }

    [Fact]
    public void Combo_TemplateBatch_UnconvertibleEntryDoesNotDropTheRest()
    {
        Interop.PrimeReady();
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.ItemTemplate, (ComboItem item) => b => b.AddContent(0, $"Item: {item.Text}")));

        var containerId = Interop.ContainerIdOf(cut);
        var templateId = TemplateId(cut, "itemTemplate");
        // A context that is no ComboItem is not rendered; the entries after it still apply.
        var bad = JsonSerializer.Deserialize<JsonElement[]>(TemplateBatch(containerId, templateId, "content-bad", "42"))!;
        var good = JsonSerializer.Deserialize<JsonElement[]>(TemplateBatch(containerId, templateId, "content-good", UuidRef(Interop, cut, 0)))!;
        Interop.AdjustDynamicContentBatch(containerId, JsonSerializer.Serialize(bad.Concat(good)));

        cut.WaitForAssertion(() => Assert.Contains("Item: First", cut.Markup));
    }

    [Fact]
    public void Combo_TemplateBatch_SentUnderAnotherComponent_RendersContent()
    {
        Interop.PrimeReady();
        var other = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1 }));
        var cut = Render<IgbCombo<ComboItem, ComboItem>>(ps => ps
            .Add(c => c.Data, new[] { _valueItem1, _valueItem2 })
            .Add(c => c.ItemTemplate, (ComboItem item) => b => b.AddContent(0, $"Item: {item.Text}")));

        // The client sends one batch for every component, under the one that started it.
        var containerId = Interop.ContainerIdOf(cut);
        Interop.AdjustDynamicContentBatch(Interop.ContainerIdOf(other),
            TemplateBatch(containerId, TemplateId(cut, "itemTemplate"), "content-1", UuidRef(Interop, cut, 1)));

        cut.WaitForAssertion(() => Assert.Contains("Item: Second", cut.Markup));
    }
}

public abstract class ComboValueKeyTestsBase<TValue> : ComponentWithContractTestBase<IgbCombo<TValue, ComboItem>>
    where TValue : notnull
{
    protected static readonly ComboItem Item1 = new() { Id = 1, StringId = "UK01", Text = "First" };
    protected static readonly ComboItem Item2 = new() { Id = 2, StringId = "UK02", Text = "Second" };

    protected virtual Action<ComponentParameterCollectionBuilder<IgbCombo<TValue, ComboItem>>> Arrange =>
        ps => ps
            .Add(c => c.Data, new[] { Item1, Item2 })
            .Add(c => c.ValueKey, "Id");

    protected abstract string EventKeyValue { get; }

    protected abstract TValue ExpectedKeyValue { get; }

    protected abstract TValue[] PropValues { get; }

    protected abstract string ExpectedValue { get; }

    protected override ComponentContract<IgbCombo<TValue, ComboItem>> InteropContract => BuildContract();

    private ComponentContract<IgbCombo<TValue, ComboItem>> BuildContract() =>
        new ComponentContract<IgbCombo<TValue, ComboItem>>()
            // A keyed combo selects by key, so the keys cross as they are, no data-source refs.
            .Method(c => c.SelectAsync(PropValues), c => c.Select(PropValues), "select",
                arrange: Arrange,
                args: [new RawJson(ExpectedValue)], types: [""])
            .Method(c => c.DeselectAsync(PropValues), c => c.Deselect(PropValues), "deselect",
                arrange: Arrange,
                args: [new RawJson(ExpectedValue)], types: [""])
            .Event(c => c.Change,
                Arrange,
                argsJson: FromRender.Of((interop, cut) => ComboTests.ChangeDetail(EventKeyValue, ComboTests.UuidRef(interop, cut, 1))),
                assert: (cut, args) =>
                {
                    Assert.Equal(ExpectedKeyValue, Assert.Single(args.Detail.NewValue));
                    Assert.Same(Item2, Assert.Single(args.Detail.Items));
                    // Two-way Value propagation through the generated wrapper works when TValue
                    // matches the key value type.
                    Assert.Equal(ExpectedKeyValue, Assert.Single(cut.Instance.Value));
                })
            // A keyed combo's value is the keys, so it crosses as they are too.
            .Prop(c => c.Value,
                value: PropValues,
                arrange: Arrange,
                wire: new RawJson(ExpectedValue));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

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
    protected override Action<ComponentParameterCollectionBuilder<IgbCombo<string, ComboItem>>> Arrange =>
    ps => ps
        .Add(c => c.Data, new[] { Item1, Item2 })
        .Add(c => c.ValueKey, "StringId");

    protected override string EventKeyValue => "\"UK02\"";
    protected override string ExpectedKeyValue => "UK02";
    protected override string[] PropValues => ["UK01", "UK03"];
    protected override string ExpectedValue => """["UK01", "UK03"]""";
}
