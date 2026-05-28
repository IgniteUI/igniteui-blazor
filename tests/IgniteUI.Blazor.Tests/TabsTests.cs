using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace IgniteUI.Blazor.Tests;

public class TabsTests : BlazorComponentTestBase
{
    [Fact]
    public void Tabs_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTabs>();
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
        var cut = RenderComponent<IgbTabs>(parameters =>
            parameters.Add(p => p.Alignment, TabsAlignment.Center));

        var element = cut.Find("igc-tabs");
        Assert.Equal("center", element.GetAttribute("alignment"));
    }

    [Fact]
    public void Tabs_Activation_RendersAttribute()
    {
        var cut = RenderComponent<IgbTabs>(parameters =>
            parameters.Add(p => p.Activation, TabsActivation.Manual));

        var element = cut.Find("igc-tabs");
        Assert.Equal("manual", element.GetAttribute("activation"));
    }

    [Fact]
    public void Tabs_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbTabs).IsSubclassOf(typeof(BaseRendererControl)));
    }
}

public class TabTests : BlazorComponentTestBase
{
    [Fact]
    public void Tab_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbTab>();
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
        var cut = RenderComponent<IgbTab>(parameters =>
            parameters.Add(p => p.Disabled, true));

        var element = cut.Find("igc-tab");
        Assert.NotNull(element.GetAttribute("disabled"));
    }

    [Fact]
    public void Tab_Selected_RendersAttribute()
    {
        var cut = RenderComponent<IgbTab>(parameters =>
            parameters.Add(p => p.Selected, true));

        var element = cut.Find("igc-tab");
        Assert.NotNull(element.GetAttribute("selected"));
    }

    [Fact]
    public void Tab_ChildContent_Renders()
    {
        var cut = RenderComponent<IgbTab>(parameters =>
            parameters.AddChildContent("Tab Label"));

        Assert.Contains("Tab Label", cut.Markup);
    }

    [Fact]
    public void Tab_SelectedChanged_EmptyCallback_ClearsBackingField()
    {
        var tab = new IgbTab();
        tab.SelectedChanged = EventCallback.Factory.Create<bool>(this, static _ => { });
        tab.SelectedChanged = EventCallback<bool>.Empty;

        var field = typeof(IgbTab).GetField("_selectedChanged", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(field);
        Assert.Null(field!.GetValue(tab));
    }
}
