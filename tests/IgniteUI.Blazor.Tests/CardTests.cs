using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

public class CardTests : BlazorComponentTestBase
{
    [Fact]
    public void Card_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCard>();
        Assert.NotNull(cut.Find("igc-card"));
    }

    [Fact]
    public void Card_TypeMetadata_IsCorrect()
    {
        var card = new IgbCard();
        Assert.Equal("WebCard", card.Type);
    }

    [Fact]
    public void Card_Elevated_RendersAttribute()
    {
        var cut = RenderComponent<IgbCard>(parameters =>
            parameters.Add(p => p.Elevated, true));

        var element = cut.Find("igc-card");
        Assert.NotNull(element.GetAttribute("elevated"));
    }

    [Fact]
    public void Card_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbCard>(parameters =>
            parameters.AddChildContent("<p>Card content</p>"));

        Assert.Contains("Card content", cut.Markup);
    }

    [Fact]
    public void Card_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbCard).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class CardHeaderTests : BlazorComponentTestBase
{
    [Fact]
    public void CardHeader_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCardHeader>();
        Assert.NotNull(cut.Find("igc-card-header"));
    }

    [Fact]
    public void CardHeader_TypeMetadata_IsCorrect()
    {
        var header = new IgbCardHeader();
        Assert.Equal("WebCardHeader", header.Type);
    }
}

public class CardContentTests : BlazorComponentTestBase
{
    [Fact]
    public void CardContent_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCardContent>();
        Assert.NotNull(cut.Find("igc-card-content"));
    }

    [Fact]
    public void CardContent_TypeMetadata_IsCorrect()
    {
        var content = new IgbCardContent();
        Assert.Equal("WebCardContent", content.Type);
    }
}

public class CardActionsTests : BlazorComponentTestBase
{
    [Fact]
    public void CardActions_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCardActions>();
        Assert.NotNull(cut.Find("igc-card-actions"));
    }

    [Fact]
    public void CardActions_TypeMetadata_IsCorrect()
    {
        var actions = new IgbCardActions();
        Assert.Equal("WebCardActions", actions.Type);
    }
}

public class CardMediaTests : BlazorComponentTestBase
{
    [Fact]
    public void CardMedia_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbCardMedia>();
        Assert.NotNull(cut.Find("igc-card-media"));
    }

    [Fact]
    public void CardMedia_TypeMetadata_IsCorrect()
    {
        var media = new IgbCardMedia();
        Assert.Equal("WebCardMedia", media.Type);
    }
}
