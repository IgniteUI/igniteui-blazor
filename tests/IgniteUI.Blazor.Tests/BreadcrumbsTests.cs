using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

public class BreadcrumbsTests : BlazorComponentTestBase
{
    [Fact]
    public void Breadcrumbs_RendersCorrectElement()
    {
        var cut = Render<IgbBreadcrumbs>();
        Assert.NotNull(cut.Find("igc-breadcrumbs"));
    }

    [Fact]
    public void Breadcrumbs_TypeMetadata_IsCorrect()
    {
        var breadcrumbs = new IgbBreadcrumbs();
        Assert.Equal("WebBreadcrumbs", breadcrumbs.Type);
    }

    [Fact]
    public void Breadcrumbs_Separator_RendersAttribute()
    {
        var cut = Render<IgbBreadcrumbs>(parameters =>
            parameters.Add(p => p.Separator, "chevron_right"));

        Assert.Equal("chevron_right", cut.Find("igc-breadcrumbs").GetAttribute("separator"));
    }

    /// <summary>
    /// An unset <see cref="IgbBreadcrumbs.Separator"/> that reached the element, even as an empty
    /// attribute, would replace the web component's default <c>tree_expand</c> icon with no icon.
    /// </summary>
    [Fact]
    public void Breadcrumbs_Separator_NotRenderedByDefault()
    {
        var cut = Render<IgbBreadcrumbs>();

        Assert.Null(cut.Instance.Separator);
        Assert.False(cut.Find("igc-breadcrumbs").HasAttribute("separator"));
    }

    [Fact]
    public void Breadcrumbs_ChildBreadcrumbs_RenderInOrder()
    {
        var cut = Render<IgbBreadcrumbs>(parameters =>
            parameters.AddChildContent(BreadcrumbsWith("Home", "Products", "Laptop")));

        var items = cut.FindAll("igc-breadcrumbs > igc-breadcrumb");
        Assert.Equal(new[] { "Home", "Products", "Laptop" }, items.Select(i => i.TextContent));
    }

    [Fact]
    public void Breadcrumbs_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbBreadcrumbs).IsSubclassOf(typeof(BaseRendererControl)));
    }

    private static RenderFragment BreadcrumbsWith(params string[] labels) => builder =>
    {
        foreach (var label in labels)
        {
            builder.OpenComponent<IgbBreadcrumb>(0);
            builder.AddAttribute(1, nameof(IgbBreadcrumb.ChildContent), (RenderFragment)(b => b.AddContent(0, label)));
            builder.CloseComponent();
        }
    };
}

public class BreadcrumbTests : BlazorComponentTestBase
{
    [Fact]
    public void Breadcrumb_RendersCorrectElement()
    {
        var cut = Render<IgbBreadcrumb>();
        Assert.NotNull(cut.Find("igc-breadcrumb"));
    }

    [Fact]
    public void Breadcrumb_TypeMetadata_IsCorrect()
    {
        var breadcrumb = new IgbBreadcrumb();
        Assert.Equal("WebBreadcrumb", breadcrumb.Type);
    }

    [Fact]
    public void Breadcrumb_ChildContent_Renders()
    {
        var cut = Render<IgbBreadcrumb>(parameters =>
            parameters.AddChildContent("<a href=\"/home\">Home</a>"));

        Assert.NotNull(cut.Find("igc-breadcrumb > a[href='/home']"));
    }

    [Fact]
    public void Breadcrumb_Current_RendersAttribute()
    {
        var cut = Render<IgbBreadcrumb>(parameters =>
            parameters.Add(p => p.Current, true));

        Assert.True(cut.Instance.Current);
        Assert.NotNull(cut.Find("igc-breadcrumb").GetAttribute("current"));
    }

    [Fact]
    public void Breadcrumb_Disabled_RendersAttribute()
    {
        var cut = Render<IgbBreadcrumb>(parameters =>
            parameters.Add(p => p.Disabled, true));

        Assert.True(cut.Instance.Disabled);
        Assert.NotNull(cut.Find("igc-breadcrumb").GetAttribute("disabled"));
    }

    [Fact]
    public void Breadcrumb_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbBreadcrumb).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
