using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class ListTests : BlazorComponentTestBase
{
    [Fact]
    public void List_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbList>();
        Assert.NotNull(cut.Find("igc-list"));
    }

    [Fact]
    public void List_TypeMetadata_IsCorrect()
    {
        var list = new IgbList();
        Assert.Equal("WebList", list.Type);
    }

    [Fact]
    public void List_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbList>(parameters =>
            parameters.AddChildContent("List items"));

        Assert.Contains("List items", cut.Markup);
    }

    [Fact]
    public void List_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbList).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class ListItemTests : BlazorComponentTestBase
{
    [Fact]
    public void ListItem_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbListItem>();
        Assert.NotNull(cut.Find("igc-list-item"));
    }

    [Fact]
    public void ListItem_TypeMetadata_IsCorrect()
    {
        var item = new IgbListItem();
        Assert.Equal("WebListItem", item.Type);
    }

    [Fact]
    public void ListItem_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbListItem>(parameters =>
            parameters.AddChildContent("Item text"));

        Assert.Contains("Item text", cut.Markup);
    }
}

public class ListHeaderTests : BlazorComponentTestBase
{
    [Fact]
    public void ListHeader_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbListHeader>();
        Assert.NotNull(cut.Find("igc-list-header"));
    }

    [Fact]
    public void ListHeader_TypeMetadata_IsCorrect()
    {
        var header = new IgbListHeader();
        Assert.Equal("WebListHeader", header.Type);
    }
}
