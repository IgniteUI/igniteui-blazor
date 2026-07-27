using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Tests.Interop;

namespace IgniteUI.Blazor.Tests;

public class IconTests : ComponentWithContractTestBase<IgbIcon>
{
    protected override ComponentContract<IgbIcon> InteropContract { get; } = new ComponentContract<IgbIcon>()
        .Method(c => c.RegisterIconAsync("home", "https://example.com/home.svg", "material"), c => c.RegisterIcon("home", "https://example.com/home.svg", "material"),
            "registerIcon", args: ["home", "https://example.com/home.svg", "material"], types: ["String", "String", "String"])
        .Method(c => c.RegisterIconFromTextAsync("home", "<svg></svg>", "material"), c => c.RegisterIconFromText("home", "<svg></svg>", "material"),
            "registerIconFromText", args: ["home", "<svg></svg>", "material"], types: ["String", "String", "String"])
        // IgbIconMeta is a MarshalByValueFactory type ("WebIconMeta")
        // wire object carries name/collection plus bookkeeping (___byValue, type), hence JsonSubset.
        .Method(c => c.SetIconRefAsync("chevron", "material", new IgbIconMeta { Name = "chevron_right", Collection = "custom" }), c => c.SetIconRef("chevron", "material", new IgbIconMeta { Name = "chevron_right", Collection = "custom" }),
            "setIconRef",
            args: ["chevron", "material", new JsonSubset("""{"name": "chevron_right", "collection": "custom"}""")],
            types: ["String", "String", "Json"]);

    [Fact]
    public Task Methods_FollowContract() => VerifyMethodContract();

    [Fact]
    public void Icon_RendersCorrectElement()
    {
        var cut = RenderComponent<IgbIcon>();
        Assert.NotNull(cut.Find("igc-icon"));
    }

    [Fact]
    public void Icon_TypeMetadata_IsCorrect()
    {
        var icon = new IgbIcon();
        Assert.Equal("WebIcon", icon.Type);
    }

    [Fact]
    public void Icon_Name_RendersAttribute()
    {
        var cut = RenderComponent<IgbIcon>(parameters =>
            parameters.Add(p => p.IconName, "home"));

        var element = cut.Find("igc-icon");
        Assert.Equal("home", element.GetAttribute("name"));
    }

    [Fact]
    public void Icon_Collection_RendersAttribute()
    {
        var cut = RenderComponent<IgbIcon>(parameters =>
            parameters.Add(p => p.Collection, "material"));

        var element = cut.Find("igc-icon");
        Assert.Equal("material", element.GetAttribute("collection"));
    }

    [Fact]
    public void Icon_Mirrored_RendersAttribute()
    {
        var cut = RenderComponent<IgbIcon>(parameters =>
            parameters.Add(p => p.Mirrored, true));

        var element = cut.Find("igc-icon");
        Assert.NotNull(element.GetAttribute("mirrored"));
    }

    [Fact]
    public void Icon_InheritsFromBaseRendererControl()
    {
        Assert.True(typeof(IgbIcon).IsSubclassOf(typeof(BaseRendererControl)));
    }
}
