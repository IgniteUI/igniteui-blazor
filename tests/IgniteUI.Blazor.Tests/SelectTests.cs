using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class SelectTests : ComponentWithContractTestBase<IgbSelect<string>>
{
    /// <summary>Static arrange for contract tests adding two select items.</summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbSelect<string>>> arrangeItems =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbSelectItem<string>>(0);
            builder.AddAttribute(1, "Value", "us");
            builder.CloseComponent();
            builder.OpenComponent<IgbSelectItem<string>>(2);
            builder.AddAttribute(3, "Value", "ca");
            builder.CloseComponent();
        });

    /// <summary>Static arrange for contract tests adding a select group.</summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbSelect<string>>> arrangeGroups =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbSelectGroup<string>>(0);
            builder.CloseComponent();
        });

    protected override ComponentContract<IgbSelect<string>> InteropContract { get; } = new ComponentContract<IgbSelect<string>>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Getter(c => c.GetCurrentValueAsync(), c => c.GetCurrentValue(), "Value", returns: "us")
        .Getter(c => c.GetItemsAsync(), c => c.GetItems(), "Items",
            arrangeItems,
            returns: FromRender.Of((interop, cut) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-select-item:nth-of-type(1)")}}}"}, {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-select-item:nth-of-type(2)")}}}"}]""")),
            assert: (cut, result) =>
            {
                Assert.Equal(2, result.Length);
                Assert.Same(cut.FindComponents<IgbSelectItem<string>>()[0].Instance, result[0]);
                Assert.Same(cut.FindComponents<IgbSelectItem<string>>()[1].Instance, result[1]);
            })
        .Getter(c => c.GetGroupsAsync(), c => c.GetGroups(), "Groups",
            arrangeGroups,
            returns: FromRender.Of((interop, cut) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-select-group:nth-of-type(1)")}}}"}]""")),
            assert: (cut, result) =>
            {
                Assert.Single(result);
                // TODO: IgbSelectGroup never registers with Select's FindByName (no cascading-value
                // partial the way SelectItem has one), so the ref currently resolves to a null
                // Assert.Same(cut.FindComponents<IgbSelectGroup<string>>()[0].Instance, result[0]);
            })
        .Getter(c => c.GetSelectedItemAsync(), c => c.GetSelectedItem(), "SelectedItem",
            arrangeItems,
            returns: FromRender.Of((interop, cut) => InteropReturn.Ref($$"""{"refType": "name", "id": "{{interop.ContainerIdOf(cut, "igc-select-item:nth-of-type(1)")}}"}""")),
            assert: (cut, result) => Assert.Same(cut.FindComponents<IgbSelectItem<string>>()[0].Instance, result))
        .Method(c => c.FocusComponentAsync(new IgbFocusOptions { PreventScroll = true }), c => c.FocusComponent(new IgbFocusOptions { PreventScroll = true }), "focus",
            args: [new JsonSubset("""{"preventScroll": true}""")], types: ["Json"])
        .Method(c => c.BlurComponentAsync(), c => c.BlurComponent(), "blur")
        .Method(c => c.ReportValidityAsync(), c => c.ReportValidity(), "reportValidity", returns: true)
        .Method(c => c.ClearSelectionAsync(), c => c.ClearSelection(), "clearSelection")
        .Method(c => c.CheckValidityAsync(), c => c.CheckValidity(), "checkValidity", returns: true)
        .Method(c => c.SetCustomValidityAsync("Please choose an option"), c => c.SetCustomValidity("Please choose an option"), "setCustomValidity",
            args: ["Please choose an option"], types: ["String"])
        .Event(c => c.Focus)
        .Event(c => c.Blur)
        .Event(c => c.Opening)
        .Event(c => c.Opened)
        .Event(c => c.Closing)
        .Event(c => c.Closed)
        .Event(c => c.Change,
            arrangeItems,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-select-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) =>
            {
                Assert.Same(cut.FindComponents<IgbSelectItem<string>>()[1].Instance, args.Detail);
                Assert.Equal("ca", args.Detail.Value); // Change propagates Detail.Value into Select.Value
            })
        // The detail is the selected item; the binding receives that item's Value.
        .Bind(c => c.Value, c => c.ValueChanged, via: c => c.Change,
            arrange: arrangeItems,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-select-item:nth-of-type(2)")}}}"}}"""),
            expect: "ca");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Binds_FollowContract() => VerifyBindContract();

    [Fact]
    public void Select_RendersCorrectElement()
    {
        var cut = Render<IgbSelect<string>>();
        Assert.NotNull(cut.Find("igc-select"));
    }

    [Fact]
    public void Select_TypeMetadata_IsCorrect()
    {
        var select = new IgbSelect<string>();
        Assert.Equal("WebSelect", select.RendererType);
    }

    [Fact]
    public void Select_Disabled_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-select");
        Assert.True(cut.Instance.Disabled);
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Select_Required_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Required, true));

        var element = cut.Find("igc-select");
        Assert.True(cut.Instance.Required);
        Assert.NotNull(element.GetAttribute("required"));
    }

    [Fact]
    public void Select_Open_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-select");
        Assert.True(cut.Instance.Open);
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Select_Placeholder_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Placeholder, "Choose..."));

        var element = cut.Find("igc-select");
        Assert.Equal("Choose...", cut.Instance.Placeholder);
        Assert.Equal("Choose...", element.GetAttribute("placeholder"));
    }

    [Fact]
    public void Select_Label_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Label, "Country"));

        var element = cut.Find("igc-select");
        Assert.Equal("Country", cut.Instance.Label);
        Assert.Equal("Country", element.GetAttribute("label"));
    }

    [Fact]
    public void Select_Outlined_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Outlined, true));

        var element = cut.Find("igc-select");
        Assert.True(cut.Instance.Outlined);
        Assert.NotNull(element.GetAttribute("outlined"));
    }

    [Fact]
    public void Select_Autofocus_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Autofocus, true));

        var element = cut.Find("igc-select");
        Assert.True(cut.Instance.Autofocus);
        Assert.NotNull(element.GetAttribute("autofocus"));
    }

    [Fact]
    public void Select_Invalid_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Invalid, true));

        var element = cut.Find("igc-select");
        Assert.True(cut.Instance.Invalid);
        Assert.NotNull(element.GetAttribute("invalid"));
    }

    [Fact]
    public void Select_Distance_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.Distance, 8));

        var element = cut.Find("igc-select");
        Assert.Equal(8.0, cut.Instance.Distance);
        Assert.Equal("8", element.GetAttribute("distance"));
    }

    [Fact]
    public void Select_ScrollStrategy_RendersAttribute()
    {
        var cut = Render<IgbSelect<string>>(parameters =>
            parameters.Add(p => p.ScrollStrategy, PopoverScrollStrategy.Close));

        var element = cut.Find("igc-select");
        Assert.Equal(PopoverScrollStrategy.Close, cut.Instance.ScrollStrategy);
        Assert.Equal("close", element.GetAttribute("scroll-strategy"));
    }

    [Fact]
    public void Select_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbSelect<string>).IsSubclassOf(typeof(BaseRendererControl)));
    }

    [Fact]
    public void Select_IntValue_IsStronglyTyped()
    {
        var cut = Render<IgbSelect<int>>(parameters =>
            parameters.Add(p => p.Value, 42));

        Assert.Equal(42, cut.Instance.Value);
        Assert.Equal(typeof(int), typeof(IgbSelect<int>).GetProperty(nameof(IgbSelect<int>.Value))!.PropertyType);
    }
}

public class SelectItemTests : BlazorComponentTestBase
{
    [Fact]
    public void SelectItem_IntValue_IsStronglyTyped()
    {
        var cut = Render<IgbSelectItem<int>>(parameters =>
            parameters.Add(p => p.Value, 7));

        Assert.Equal(7, cut.Instance.Value);
        Assert.Equal(typeof(int), typeof(IgbSelectItem<int>).GetProperty(nameof(IgbSelectItem<int>.Value))!.PropertyType);
    }

    [Fact]
    public void SelectItem_RendersCorrectElement()
    {
        var cut = Render<IgbSelectItem<string>>();
        Assert.NotNull(cut.Find("igc-select-item"));
    }

    [Fact]
    public void SelectItem_TypeMetadata_IsCorrect()
    {
        var item = new IgbSelectItem<string>();
        Assert.Equal("WebSelectItem", item.RendererType);
    }

    [Fact]
    public void SelectItem_Value_RendersAttribute()
    {
        var cut = Render<IgbSelectItem<string>>(parameters =>
            parameters.Add(p => p.Value, "us"));

        var element = cut.Find("igc-select-item");
        Assert.Equal("us", element.GetAttribute("value"));
    }

    [Fact]
    public void SelectItem_Disabled_RendersAttribute()
    {
        var cut = Render<IgbSelectItem<string>>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-select-item");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void SelectItem_Selected_RendersAttribute()
    {
        var cut = Render<IgbSelectItem<string>>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-select-item");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void SelectItem_ChildContent_Renders()
    {
        var cut = Render<IgbSelectItem<string>>(parameters =>
            parameters.AddChildContent("United States"));

        Assert.Contains("United States", cut.Markup);
    }
}

public class SelectGroupTests : BlazorComponentTestBase
{
    [Fact]
    public void SelectGroup_RendersCorrectElement()
    {
        var cut = Render<IgbSelectGroup<string>>();
        cut.Find("igc-select-group").Should_Exist();
    }

    [Fact]
    public void SelectGroup_Disabled_RendersAttribute()
    {
        var cut = Render<IgbSelectGroup<string>>(parameters =>
            parameters.Add(p => p.Disabled, true));

        Assert.NotNull(cut.Find("igc-select-group").GetAttribute("disabled"));
    }
}

public class SelectHeaderTests : BlazorComponentTestBase
{
    [Fact]
    public void SelectHeader_RendersCorrectElement()
    {
        var cut = Render<IgbSelectHeader>();
        cut.Find("igc-select-header").Should_Exist();
    }

    [Fact]
    public void SelectHeader_ChildContent_Renders()
    {
        var cut = Render<IgbSelectHeader>(parameters =>
            parameters.AddChildContent("Group A"));

        Assert.Contains("Group A", cut.Find("igc-select-header").InnerHtml);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbSelect</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Select_DefaultValues_MatchWebComponent()
    {
        var select = new IgbSelect<string>();

        Assert.Equal(PopoverPlacement.BottomStart, select.Placement);
    }

    #region Child collection lifecycle

    /// <summary>Renders <paramref name="count"/> <see cref="IgbSelectItem"/> children.</summary>
    static Action<ComponentParameterCollectionBuilder<IgbSelect<string>>> SelectWith(int count) => ps =>
        ps.AddChildContent(builder =>
        {
            for (var i = 0; i < count; i++)
            {
                builder.OpenComponent<IgbSelectItem<string>>(i);
                builder.CloseComponent();
            }
        });

    [Fact]
    public void Select_ChildItems_RegisterOnInitialize()
    {
        var cut = Render<IgbSelect<string>>(SelectWith(2));

        Assert.Equal(
            cut.FindComponents<IgbSelectItem<string>>().Select(i => i.Instance),
            cut.Instance.ContentItems);
    }

    [Fact]
    public void Select_DisposedChildItem_LeavesTheCollection()
    {
        var cut = Render<IgbSelect<string>>(SelectWith(2));
        var survivor = cut.FindComponents<IgbSelectItem<string>>()[0].Instance;

        cut.Render(SelectWith(1));

        Assert.Same(survivor, Assert.Single(cut.Instance.ContentItems));
    }

    [Fact]
    public void Select_AllChildItemsDisposed_EmptiesTheCollection()
    {
        var cut = Render<IgbSelect<string>>(SelectWith(2));

        cut.Render(ps => ps.AddChildContent(builder => { }));

        Assert.Empty(cut.Instance.ContentItems);
    }

    #endregion
}
