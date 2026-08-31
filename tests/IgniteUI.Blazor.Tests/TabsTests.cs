using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

public class TabsTests : ComponentWithContractTestBase<IgbTabs>
{
    /// <summary>What each arranged tab's <c>@bind-Selected</c> received, filled during the dispatch.</summary>
    static readonly bool?[] tabSelection = new bool?[2];

    /// <summary>Two tabs, each binding SelectedChanged â€” IgbTab has no selection event of its own.</summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbTabs>> tabsArrange = ps =>
        {
            tabSelection[0] = null;
            tabSelection[1] = null;
            ps.AddChildContent(builder =>
            {
                builder.OpenComponent<IgbTab>(0);
                builder.AddAttribute(1, "id", "tab-1");
                builder.AddAttribute(2, "SelectedChanged", new EventCallback<bool>(null, (Action<bool>)(v => tabSelection[0] = v)));
                builder.CloseComponent();
                builder.OpenComponent<IgbTab>(3);
                builder.AddAttribute(4, "id", "tab-2");
                builder.AddAttribute(5, "SelectedChanged", new EventCallback<bool>(null, (Action<bool>)(v => tabSelection[1] = v)));
                builder.CloseComponent();
            });
        };

    protected override ComponentContract<IgbTabs> InteropContract { get; } = new ComponentContract<IgbTabs>()
        .Event(c => c.Change,
            tabsArrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tab:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) =>
            {
                Assert.Same(cut.Instance.ActualTabsCollection[1], args.Detail);
                // The handler owns selection for every child: it writes each tab's Selected and
                // pushes it through that tab's @bind-Selected, which is IgbTab's only route.
                Assert.True(args.Detail.Selected);
                Assert.False(cut.Instance.ActualTabsCollection[0].Selected);
                Assert.False(tabSelection[0]);
                Assert.True(tabSelection[1]);
            })
        .Method(c => c.SelectAsync("tab-1"), c => c.Select("tab-1"), "select", args: ["tab-1"], types: ["String"])
        .Getter(c => c.GetSelectedAsync(), c => c.GetSelected(), "Selected", returns: "tab-1");

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Tabs_RendersCorrectElement()
    {
        var cut = Render<IgbTabs>();
        Assert.NotNull(cut.Find("igc-tabs"));
    }

    [Fact]
    public void Tabs_TypeMetadata_IsCorrect()
    {
        var tabs = new IgbTabs();
        Assert.Equal("WebTabs", tabs.Type);
    }

    [Fact]
    public void Tabs_Alignment_RendersAttribute()
    {
        var cut = Render<IgbTabs>(parameters =>
            parameters.Add(p => p.Alignment, TabsAlignment.Center));

        var element = cut.Find("igc-tabs");
        Assert.Equal("center", element.GetAttribute("alignment"));
    }

    [Fact]
    public void Tabs_Activation_RendersAttribute()
    {
        var cut = Render<IgbTabs>(parameters =>
            parameters.Add(p => p.Activation, TabsActivation.Manual));

        var element = cut.Find("igc-tabs");
        Assert.Equal("manual", element.GetAttribute("activation"));
    }

    [Fact]
    public void Tabs_ChildContent_Renders()
    {
        var cut = Render<IgbTabs>(parameters =>
            parameters.AddChildContent("<igc-tab>First</igc-tab>"));

        Assert.Contains("First", cut.Find("igc-tabs").InnerHtml);
    }

    [Fact]
    public void Tabs_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbTabs).IsSubclassOf(typeof(BaseRendererControl)));
    }

    #region Child collection lifecycle

    /// <summary>Renders <paramref name="labels"/> as <see cref="IgbTab"/> children.</summary>
    static Action<ComponentParameterCollectionBuilder<IgbTabs>> TabsWith(params string[] labels) => ps =>
        ps.AddChildContent(builder =>
        {
            var seq = 0;
            foreach (var label in labels)
            {
                builder.OpenComponent<IgbTab>(seq++);
                builder.AddAttribute(seq++, "Label", label);
                builder.CloseComponent();
            }
        });

    [Fact]
    public void Tabs_ChildTabs_RegisterOnInitialize()
    {
        var cut = Render<IgbTabs>(TabsWith("one", "two"));

        Assert.Equal(2, cut.Instance.ActualTabsCollection.Count);
        Assert.Equal(
            cut.FindComponents<IgbTab>().Select(t => t.Instance),
            cut.Instance.ActualTabsCollection);
    }

    [Fact]
    public void Tabs_DisposedChildTab_LeavesTheCollection()
    {
        var cut = Render<IgbTabs>(TabsWith("one", "two"));
        Assert.Equal(2, cut.Instance.ActualTabsCollection.Count);

        // Re-render without the second tab; Blazor disposes the removed component.
        cut.Render(TabsWith("one"));

        var remaining = Assert.Single(cut.FindComponents<IgbTab>()).Instance;
        Assert.Same(remaining, Assert.Single(cut.Instance.ActualTabsCollection));
    }

    [Fact]
    public void Tabs_AllChildTabsDisposed_EmptiesTheCollection()
    {
        var cut = Render<IgbTabs>(TabsWith("one", "two"));

        cut.Render(ps => ps.AddChildContent(builder => { }));

        Assert.Empty(cut.Instance.ActualTabsCollection);
    }

    [Fact]
    public void Tabs_ChildTabRenderedAgainAfterRemoval_Reregisters()
    {
        var cut = Render<IgbTabs>(TabsWith("one", "two"));

        cut.Render(TabsWith("one"));
        cut.Render(TabsWith("one", "two"));

        Assert.Equal(
            cut.FindComponents<IgbTab>().Select(t => t.Instance),
            cut.Instance.ActualTabsCollection);
    }

    #endregion
}

// IgbTab has no interop surface of its own: its @bind-Selected pair is driven entirely by
// IgbTabs' Change handler, and is covered by that event's spec above.
public class TabTests : BlazorComponentTestBase
{
    [Fact]
    public void Tab_RendersCorrectElement()
    {
        var cut = Render<IgbTab>();
        Assert.NotNull(cut.Find("igc-tab"));
    }

    [Fact]
    public void Tab_TypeMetadata_IsCorrect()
    {
        var tab = new IgbTab();
        Assert.Equal("WebTab", tab.Type);
    }

    [Fact]
    public void Tab_Disabled_RendersAttribute()
    {
        var cut = Render<IgbTab>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-tab");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Tab_Selected_RendersAttribute()
    {
        var cut = Render<IgbTab>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-tab");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void Tab_ChildContent_Renders()
    {
        var cut = Render<IgbTab>(parameters =>
            parameters.AddChildContent("Tab Label"));

        Assert.Contains("Tab Label", cut.Markup);
    }
}
