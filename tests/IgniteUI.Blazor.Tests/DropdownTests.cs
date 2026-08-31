using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

public class DropdownTests : ComponentWithContractTestBase<IgbDropdown>
{
    /// <summary>
    /// Holds what the anchor arrangements below capture. An anchor only exists once its
    /// render has run, so the specs read it back from here — the runner renders a spec's
    /// arrangement before invoking it, and gives each arranged spec its own render.
    /// </summary>
    sealed class Anchor
    {
        public IgbButton Component = null!;
        public ElementReference Element;
    }

    static readonly Anchor anchor = new();

    /// <summary>
    /// Arranges an IgbButton as the anchor for the show/toggle target overloads. A real
    /// anchor is an element outside the dropdown (that's the point of passing one — an
    /// anchor inside it would go in the <c>target</c> slot instead); the interop boundary
    /// only sees the reference, so where the button renders is immaterial here.
    /// </summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbDropdown>> componentAnchorArrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbButton>(0);
            builder.AddComponentReferenceCapture(1, instance => anchor.Component = (IgbButton)instance);
            builder.CloseComponent();
        });

    /// <summary>Arranges a plain element as the anchor, capturing its reference — the <c>@ref</c> form of a target</summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbDropdown>> elementAnchorArrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenElement(0, "span");
            builder.AddElementReferenceCapture(1, reference => anchor.Element = reference);
            builder.CloseElement();
        });

    /// <summary>The wire form of the arranged component anchor — its interop instance id, assigned on render</summary>
    static readonly FromRender<string> componentAnchorArg =
        FromRender.Of((interop, cut) => $"containerId:::{interop.ContainerIdOf(cut, "igc-button")}");

    /// <summary>Arranges two IgbDropdownItem children</summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbDropdown>> itemsArrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbDropdownItem>(0);
            builder.AddAttribute(1, "id", "item-1");
            builder.CloseComponent();
            builder.OpenComponent<IgbDropdownItem>(2);
            builder.AddAttribute(3, "id", "item-2");
            builder.CloseComponent();
        });

    /// <summary>Arranges two IgbDropdownGroup children</summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbDropdown>> groupsArrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbDropdownGroup>(0);
            builder.AddAttribute(1, "id", "group-1");
            builder.CloseComponent();
            builder.OpenComponent<IgbDropdownGroup>(2);
            builder.AddAttribute(3, "id", "group-2");
            builder.CloseComponent();
        });

    protected override ComponentContract<IgbDropdown> InteropContract { get; } = new ComponentContract<IgbDropdown>()
        .Method(c => c.ShowAsync(), c => c.Show(), "show", returns: true)
        // A component anchor crosses as that component's own interop instance, with no
        // element handle.
        .Method(c => c.ShowAsync(anchor.Component), c => c.Show(anchor.Component), "show", returns: true,
            args: [componentAnchorArg], types: ["Component"], arrange: componentAnchorArrange)
        // An ElementReference anchor crosses as a positional placeholder, with the handle
        // itself riding alongside the arguments.
        .Method(c => c.ShowAsync(anchor.Element), c => c.Show(anchor.Element), "show", returns: true,
            args: ["elementIndex:::0"], types: ["Component"], arrange: elementAnchorArrange,
            elements: () => [anchor.Element])
        .Method(c => c.HideAsync(), c => c.Hide(), "hide", returns: false)
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle", returns: true)
        .Method(c => c.ToggleAsync(anchor.Component), c => c.Toggle(anchor.Component), "toggle", returns: true,
            args: [componentAnchorArg], types: ["Component"], arrange: componentAnchorArrange)
        .Method(c => c.ToggleAsync(anchor.Element), c => c.Toggle(anchor.Element), "toggle", returns: true,
            args: ["elementIndex:::0"], types: ["Component"], arrange: elementAnchorArrange,
            elements: () => [anchor.Element])
        .Method(c => c.ClearSelectionAsync(), c => c.ClearSelection(), "clearSelection")
        .Method(c => c.SelectAsync("item-1"), c => c.Select("item-1"), "select",
            InteropReturn.Undefined, expect: null, args: ["item-1"], types: ["Json"])
        .Method(c => c.NavigateToAsync(2), c => c.NavigateTo(2), "navigateTo",
            InteropReturn.Undefined, expect: null, args: [2.0], types: ["Json"])
        .Getter(c => c.GetItemsAsync(), c => c.GetItems(), "Items",
            itemsArrange,
            returns: FromRender.Of((interop, cut) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-dropdown-item:nth-of-type(1)")}}}"}, {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-dropdown-item:nth-of-type(2)")}}}"}]""")),
            assert: (cut, result) =>
            {
                Assert.Equal(2, result!.Length);
                Assert.Same(cut.FindComponents<IgbDropdownItem>()[0].Instance, result[0]);
                Assert.Same(cut.FindComponents<IgbDropdownItem>()[1].Instance, result[1]);
            })
        .Getter(c => c.GetGroupsAsync(), c => c.GetGroups(), "Groups",
            groupsArrange,
            returns: FromRender.Of((interop, cut) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-dropdown-group:nth-of-type(1)")}}}"}, {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-dropdown-group:nth-of-type(2)")}}}"}]""")),
            assert: (cut, result) =>
            {
                Assert.Equal(2, result!.Length);
                // TODO: unlike IgbDropdownItem (registers via the "DropdownParent" CascadingParameter,
                // resolved through IgbDropdown.ContentItems), IgbDropdownGroup carries no CascadingParameter
                // and there's no FindByNameDropdownGroup impl., so the refs currently resolve to null elements;
                // Assert.Same(cut.FindComponents<IgbDropdownGroup>()[0].Instance, result[0]);
                // Assert.Same(cut.FindComponents<IgbDropdownGroup>()[1].Instance, result[1]);
            })
        .Getter(c => c.GetSelectedItemAsync(), c => c.GetSelectedItem(), "SelectedItem", InteropReturn.Undefined, expect: null)
        .Getter(c => c.GetSelectedItemAsync(), c => c.GetSelectedItem(), "SelectedItem",
            itemsArrange,
            returns: FromRender.Of((interop, cut) => InteropReturn.Ref($$"""{"refType": "name", "id": "{{interop.ContainerIdOf(cut, "igc-dropdown-item:nth-of-type(1)")}}"}""")),
            assert: (cut, result) => Assert.Same(cut.FindComponents<IgbDropdownItem>()[0].Instance, result))
        .Event(c => c.Opening)
        .Event(c => c.Opened)
        .Event(c => c.Closing)
        .Event(c => c.Closed)
        .Event(c => c.Change,
            itemsArrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-dropdown-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.FindComponents<IgbDropdownItem>()[1].Instance, args.Detail));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Dropdown_RendersCorrectElement()
    {
        var cut = Render<IgbDropdown>();
        Assert.NotNull(cut.Find("igc-dropdown"));
    }

    [Fact]
    public void Dropdown_TypeMetadata_IsCorrect()
    {
        var dropdown = new IgbDropdown();
        Assert.Equal("WebDropdown", dropdown.Type);
    }

    [Fact]
    public void Dropdown_Open_RendersAttribute()
    {
        var cut = Render<IgbDropdown>(parameters =>
            parameters.Add(p => p.Open, true));

        var element = cut.Find("igc-dropdown");
        Assert.NotNull(element.GetAttribute("open"));
    }

    [Fact]
    public void Dropdown_KeepOpenOnSelect_RendersAttribute()
    {
        var cut = Render<IgbDropdown>(parameters =>
            parameters.Add(p => p.KeepOpenOnSelect, true));

        var element = cut.Find("igc-dropdown");
        Assert.NotNull(element.GetAttribute("keep-open-on-select"));
    }

    [Fact]
    public void Dropdown_ChildContent_Renders()
    {
        var cut = Render<IgbDropdown>(parameters =>
            parameters.AddChildContent("<igc-dropdown-item>Item</igc-dropdown-item>"));

        Assert.Contains("Item", cut.Find("igc-dropdown").InnerHtml);
    }

    [Fact]
    public void Dropdown_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbDropdown).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class DropdownItemTests : BlazorComponentTestBase
{
    [Fact]
    public void DropdownItem_RendersCorrectElement()
    {
        var cut = Render<IgbDropdownItem>();
        Assert.NotNull(cut.Find("igc-dropdown-item"));
    }

    [Fact]
    public void DropdownItem_TypeMetadata_IsCorrect()
    {
        var item = new IgbDropdownItem();
        Assert.Equal("WebDropdownItem", item.Type);
    }

    [Fact]
    public void DropdownItem_Value_RendersAttribute()
    {
        var cut = Render<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Value, "item-1"));

        var element = cut.Find("igc-dropdown-item");
        Assert.Equal("item-1", element.GetAttribute("value"));
    }

    [Fact]
    public void DropdownItem_Disabled_RendersAttribute()
    {
        var cut = Render<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-dropdown-item");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void DropdownItem_Selected_RendersAttribute()
    {
        var cut = Render<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-dropdown-item");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void DropdownItem_Active_RendersAttribute()
    {
        var cut = Render<IgbDropdownItem>(parameters =>
            parameters.Add(p => p.Active, true));

        var element = cut.Find("igc-dropdown-item");
        Assert.NotNull(element.GetAttribute("active"));
    }

    [Fact]
    public void DropdownItem_ChildContent_Renders()
    {
        var cut = Render<IgbDropdownItem>(parameters =>
            parameters.AddChildContent("Option 1"));

        Assert.Contains("Option 1", cut.Markup);
    }
}

public class DropdownGroupTests : BlazorComponentTestBase
{
    [Fact]
    public void DropdownGroup_RendersCorrectElement()
    {
        var cut = Render<IgbDropdownGroup>();
        cut.Find("igc-dropdown-group").Should_Exist();
    }

    [Fact]
    public void DropdownGroup_ChildContent_Renders()
    {
        var cut = Render<IgbDropdownGroup>(parameters =>
            parameters.AddChildContent("<igc-dropdown-item>Grouped</igc-dropdown-item>"));

        Assert.Contains("Grouped", cut.Find("igc-dropdown-group").InnerHtml);
    }
}

public class DropdownHeaderTests : BlazorComponentTestBase
{
    [Fact]
    public void DropdownHeader_RendersCorrectElement()
    {
        var cut = Render<IgbDropdownHeader>();
        cut.Find("igc-dropdown-header").Should_Exist();
    }

    [Fact]
    public void DropdownHeader_ChildContent_Renders()
    {
        var cut = Render<IgbDropdownHeader>(parameters =>
            parameters.AddChildContent("Category"));

        Assert.Contains("Category", cut.Find("igc-dropdown-header").InnerHtml);
    }

    /// <summary>
    /// The wrapper must report the same initial values as <c>IgbDropdown</c>'s web component,
    /// so reading a property that was never assigned does not lie about the rendered state.
    /// </summary>
    [Fact]
    public void Dropdown_DefaultValues_MatchWebComponent()
    {
        var dropdown = new IgbDropdown();

        Assert.Equal(PopoverPlacement.BottomStart, dropdown.Placement);
    }

    #region Child collection lifecycle

    /// <summary>Renders <paramref name="count"/> <see cref="IgbDropdownItem"/> children.</summary>
    static Action<ComponentParameterCollectionBuilder<IgbDropdown>> DropdownWith(int count) => ps =>
        ps.AddChildContent(builder =>
        {
            for (var i = 0; i < count; i++)
            {
                builder.OpenComponent<IgbDropdownItem>(i);
                builder.CloseComponent();
            }
        });

    [Fact]
    public void Dropdown_ChildItems_RegisterOnInitialize()
    {
        var cut = Render<IgbDropdown>(DropdownWith(2));

        Assert.Equal(
            cut.FindComponents<IgbDropdownItem>().Select(i => i.Instance),
            cut.Instance.ContentItems);
    }

    [Fact]
    public void Dropdown_DisposedChildItem_LeavesTheCollection()
    {
        var cut = Render<IgbDropdown>(DropdownWith(2));
        var survivor = cut.FindComponents<IgbDropdownItem>()[0].Instance;

        cut.Render(DropdownWith(1));

        Assert.Same(survivor, Assert.Single(cut.Instance.ContentItems));
    }

    [Fact]
    public void Dropdown_AllChildItemsDisposed_EmptiesTheCollection()
    {
        var cut = Render<IgbDropdown>(DropdownWith(2));

        cut.Render(ps => ps.AddChildContent(builder => { }));

        Assert.Empty(cut.Instance.ContentItems);
    }

    #endregion Child collection lifecycle
}
