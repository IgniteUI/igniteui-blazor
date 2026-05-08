using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class TreeTests : BlazorComponentTestBase
{
    [Fact]
    public void Tree_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTree>();
        cut.Find("igc-tree").Should_Exist();
    }

    [Fact]
    public void Tree_SingleBranchExpand_RendersAttribute()
    {
        var cut = RenderComponent<IgbTree>(p =>
            p.Add(x => x.SingleBranchExpand, true));

        Assert.NotNull(cut.Find("igc-tree").GetAttribute("single-branch-expand"));
    }

    [Fact]
    public void Tree_ToggleNodeOnClick_RendersAttribute()
    {
        var cut = RenderComponent<IgbTree>(p =>
            p.Add(x => x.ToggleNodeOnClick, true));

        Assert.NotNull(cut.Find("igc-tree").GetAttribute("toggle-node-on-click"));
    }

    [Fact]
    public void Tree_Selection_Multiple()
    {
        var cut = RenderComponent<IgbTree>(p =>
            p.Add(x => x.Selection, TreeSelection.Multiple));

        Assert.Equal("multiple", cut.Find("igc-tree").GetAttribute("selection"));
    }

    [Fact]
    public void Tree_Selection_Cascade()
    {
        var cut = RenderComponent<IgbTree>(p =>
            p.Add(x => x.Selection, TreeSelection.Cascade));

        Assert.Equal("cascade", cut.Find("igc-tree").GetAttribute("selection"));
    }

    [Fact]
    public void Tree_Selection_None()
    {
        var cut = RenderComponent<IgbTree>(p =>
            p.Add(x => x.Selection, TreeSelection.None));

        Assert.Equal("none", cut.Find("igc-tree").GetAttribute("selection"));
    }

    [Fact]
    public void TreeItem_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTreeItem>();
        cut.Find("igc-tree-item").Should_Exist();
    }

    [Fact]
    public void TreeItem_Label_RendersAttribute()
    {
        var cut = RenderComponent<IgbTreeItem>(p =>
            p.Add(x => x.Label, "Node 1"));

        Assert.Equal("Node 1", cut.Find("igc-tree-item").GetAttribute("label"));
    }

    [Fact]
    public void TreeItem_Expanded_RendersAttribute()
    {
        var cut = RenderComponent<IgbTreeItem>(p =>
            p.Add(x => x.Expanded, true));

        Assert.NotNull(cut.Find("igc-tree-item").GetAttribute("expanded"));
    }

    [Fact]
    public void TreeItem_Active_RendersAttribute()
    {
        var cut = RenderComponent<IgbTreeItem>(p =>
            p.Add(x => x.Active, true));

        Assert.NotNull(cut.Find("igc-tree-item").GetAttribute("active"));
    }

    [Fact]
    public void TreeItem_Disabled_RendersAttribute()
    {
        var cut = RenderComponent<IgbTreeItem>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-tree-item").GetAttribute("disabled"));
    }

    [Fact]
    public void TreeItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTreeItem>(p =>
            p.Add(x => x.Label, "Parent")
             .AddChildContent("<span>Child</span>"));

        Assert.Contains("Child", cut.Find("igc-tree-item").InnerHtml);
    }
}
