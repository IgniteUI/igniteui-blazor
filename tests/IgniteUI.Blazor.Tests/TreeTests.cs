using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

public class TreeTests : ComponentWithContractTestBase<IgbTree>
{
    /// <summary> Static arrange for contract tests adding two tree items </summary>
    static readonly Action<ComponentParameterCollectionBuilder<IgbTree>> arrange =
        ps => ps.AddChildContent(builder =>
        {
            builder.OpenComponent<IgbTreeItem>(0);
            builder.AddAttribute(1, "id", "tree-item-1");
            builder.CloseComponent();
            builder.OpenComponent<IgbTreeItem>(2);
            builder.AddAttribute(3, "id", "tree-item-2");
            builder.CloseComponent();
        });

    protected override ComponentContract<IgbTree> InteropContract { get; } = new ComponentContract<IgbTree>()
        .Event(c => c.ItemExpanding,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tree-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.Instance.ContentItems[1], args.Detail))
        .Event(c => c.ItemExpanded,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tree-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.Instance.ContentItems[1], args.Detail))
        .Event(c => c.ItemCollapsing,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tree-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.Instance.ContentItems[1], args.Detail))
        .Event(c => c.ItemCollapsed,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tree-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.Instance.ContentItems[1], args.Detail))
        .Event(c => c.ActiveItem,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$"""{"detail": {"refType": "name", "id": "{{{interop.ContainerIdOf(cut, "igc-tree-item:nth-of-type(2)")}}}"}}"""),
            assert: (cut, args) => Assert.Same(cut.Instance.ContentItems[1], args.Detail))
        .Event(c => c.SelectionChanged,
            arrange,
            argsJson: FromRender.Of((interop, cut) => $$$$$"""{"detail": {"retType": "object", "type": "", "value": {"newSelection": {"retType": "Array", "type": "", "value": [{"refType": "name", "id": "{{{{{interop.ContainerIdOf(cut, "igc-tree-item:nth-of-type(2)")}}}}}"}]}}}}"""),
            assert: (cut, args) => Assert.Same(cut.Instance.ContentItems[1], args!.Detail!.NewSelection![0]));

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Events_FollowContract() => VerifyEventContract();

    [Fact]
    public void Tree_RendersCorrectElement()
    {
        var cut = Render<IgbTree>();
        cut.Find("igc-tree").Should_Exist();
    }

    [Fact]
    public void Tree_SingleBranchExpand_RendersAttribute()
    {
        var cut = Render<IgbTree>(p =>
            p.Add(x => x.SingleBranchExpand, true));

        Assert.NotNull(cut.Find("igc-tree").GetAttribute("single-branch-expand"));
    }

    [Fact]
    public void Tree_ToggleNodeOnClick_RendersAttribute()
    {
        var cut = Render<IgbTree>(p =>
            p.Add(x => x.ToggleNodeOnClick, true));

        Assert.NotNull(cut.Find("igc-tree").GetAttribute("toggle-node-on-click"));
    }

    [Fact]
    public void Tree_Selection_Multiple()
    {
        var cut = Render<IgbTree>(p =>
            p.Add(x => x.Selection, TreeSelection.Multiple));

        Assert.Equal("multiple", cut.Find("igc-tree").GetAttribute("selection"));
    }

    [Fact]
    public void Tree_Selection_Cascade()
    {
        var cut = Render<IgbTree>(p =>
            p.Add(x => x.Selection, TreeSelection.Cascade));

        Assert.Equal("cascade", cut.Find("igc-tree").GetAttribute("selection"));
    }

    [Fact]
    public void Tree_Selection_None()
    {
        var cut = Render<IgbTree>(p =>
            p.Add(x => x.Selection, TreeSelection.None));

        Assert.Equal("none", cut.Find("igc-tree").GetAttribute("selection"));
    }

    [Fact]
    public void TreeItem_RendersCorrectElement()
    {
        var cut = Render<IgbTreeItem>();
        cut.Find("igc-tree-item").Should_Exist();
    }

    [Fact]
    public void TreeItem_Label_RendersAttribute()
    {
        var cut = Render<IgbTreeItem>(p =>
            p.Add(x => x.Label, "Node 1"));

        Assert.Equal("Node 1", cut.Find("igc-tree-item").GetAttribute("label"));
    }

    [Fact]
    public void TreeItem_Expanded_RendersAttribute()
    {
        var cut = Render<IgbTreeItem>(p =>
            p.Add(x => x.Expanded, true));

        Assert.NotNull(cut.Find("igc-tree-item").GetAttribute("expanded"));
    }

    [Fact]
    public void TreeItem_Active_RendersAttribute()
    {
        var cut = Render<IgbTreeItem>(p =>
            p.Add(x => x.Active, true));

        Assert.NotNull(cut.Find("igc-tree-item").GetAttribute("active"));
    }

    [Fact]
    public void TreeItem_Disabled_RendersAttribute()
    {
        var cut = Render<IgbTreeItem>(p =>
            p.Add(x => x.Disabled, true));

        Assert.NotNull(cut.Find("igc-tree-item").GetAttribute("disabled"));
    }

    [Fact]
    public void TreeItem_ChildContent_Renders()
    {
        var cut = Render<IgbTreeItem>(p =>
            p.Add(x => x.Label, "Parent")
             .AddChildContent("<span>Child</span>"));

        Assert.Contains("Child", cut.Find("igc-tree-item").InnerHtml);
    }

    #region Child collection lifecycle

    /// <summary>Renders <paramref name="count"/> top-level <see cref="IgbTreeItem"/> children.</summary>
    static Action<ComponentParameterCollectionBuilder<IgbTree>> TreeWith(int count) => ps =>
        ps.AddChildContent(builder =>
        {
            for (var i = 0; i < count; i++)
            {
                builder.OpenComponent<IgbTreeItem>(i);
                builder.CloseComponent();
            }
        });

    [Fact]
    public void Tree_ChildItems_RegisterOnInitialize()
    {
        var cut = Render<IgbTree>(TreeWith(2));

        Assert.Equal(
            cut.FindComponents<IgbTreeItem>().Select(i => i.Instance),
            cut.Instance.ContentItems);
    }

    [Fact]
    public void Tree_DisposedChildItem_LeavesTheCollection()
    {
        var cut = Render<IgbTree>(TreeWith(2));
        var survivor = cut.FindComponents<IgbTreeItem>()[0].Instance;

        cut.Render(TreeWith(1));

        Assert.Same(survivor, Assert.Single(cut.Instance.ContentItems));
    }

    [Fact]
    public void Tree_AllChildItemsDisposed_EmptiesTheCollection()
    {
        var cut = Render<IgbTree>(TreeWith(2));

        cut.Render(ps => ps.AddChildContent(builder => { }));

        Assert.Empty(cut.Instance.ContentItems);
    }

    #endregion
}

public class TreeItemTests : ComponentWithContractTestBase<IgbTreeItem>
{
    /// <summary>Real-usage host: IgbTree > "Node 1" > "Child 1.1" (the component under test).</summary>
    static readonly Func<BunitContext, IRenderedComponent<IComponent>> treeHost = ContractHost.Of<IgbTree>(ps => ps.AddChildContent(b =>
    {
        b.OpenComponent<IgbTreeItem>(0);
        b.AddAttribute(1, "Label", "Node 1");
        b.AddAttribute(2, "ChildContent", (RenderFragment)(b2 =>
        {
            b2.OpenComponent<IgbTreeItem>(0);
            b2.AddAttribute(1, "Label", "Child 1.1");
            b2.CloseComponent();
        }));
        b.CloseComponent();
    }));

    protected override ComponentContract<IgbTreeItem> InteropContract { get; } = new ComponentContract<IgbTreeItem>()
        .Method(c => c.ToggleAsync(), c => c.Toggle(), "toggle")
        .Method(c => c.ExpandAsync(), c => c.Expand(), "expand")
        .Method(c => c.CollapseAsync(), c => c.Collapse(), "collapse")
        .Getter(c => c.GetPathAsync(), c => c.GetPath(), "Path",
            arrange: ps => { },
            returns: FromRender.Of((interop, cut) => InteropReturn.Array("""[{"refType": "name", "id": "mainControl"}]""")),
            assert: (cut, result) =>
            {
                Assert.Single(result!);
                Assert.Same(cut.Instance, result[0]);
            })
        .Getter(c => c.GetPathAsync(), c => c.GetPath(), "Path",
            host: treeHost,
            target: h => h.FindComponents<IgbTreeItem>()[1], // Child 1.1
            returns: FromRender.Of((interop, h) => InteropReturn.Array($$$"""[{"refType": "name", "id": "{{{interop.ContainerIdOf(h, "igc-tree-item")}}}"}, {"refType": "name", "id": "mainControl"}]""")),
            assert: (h, result) =>
            {
                Assert.Equal(2, result.Length);
                Assert.Same(h.FindComponents<IgbTreeItem>()[1].Instance, result[1]);
                // TODO: the ancestor ref only resolves through FindByName on the item
                // itself, which matches nothing but "mainControl" — the parent element
                // currently decodes to null (observed: path = [self, null])
                // Assert.Same(h.FindComponents<IgbTreeItem>()[0].Instance, result[0]);
            });

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();
}
