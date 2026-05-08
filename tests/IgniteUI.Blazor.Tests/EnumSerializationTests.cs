using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests enum serialization rules:
/// - Single word → lowercase
/// - Multi-word without WCEnumName → camelCase  
/// - With WCEnumName → specified string
/// </summary>
public class EnumSerializationTests : BlazorComponentTestBase
{
    // ButtonVariant: single word enums
    [Theory]
    [InlineData(ButtonVariant.Contained, "contained")]
    [InlineData(ButtonVariant.Outlined, "outlined")]
    [InlineData(ButtonVariant.Flat, "flat")]
    [InlineData(ButtonVariant.Fab, "fab")]
    public void ButtonVariant_Serialization(ButtonVariant variant, string expected)
    {
        var cut = RenderComponent<IgbButton>(p => p.Add(x => x.Variant, variant));
        Assert.Equal(expected, cut.Find("igc-button").GetAttribute("variant"));
    }

    // ButtonBaseType
    [Theory]
    [InlineData(ButtonBaseType.Button, "button")]
    [InlineData(ButtonBaseType.Reset, "reset")]
    [InlineData(ButtonBaseType.Submit, "submit")]
    public void ButtonBaseType_Serialization(ButtonBaseType type, string expected)
    {
        var cut = RenderComponent<IgbButton>(p => p.Add(x => x.DisplayType, type));
        Assert.Equal(expected, cut.Find("igc-button").GetAttribute("type"));
    }

    // InputType
    [Theory]
    [InlineData(InputType.Email, "email")]
    [InlineData(InputType.Number, "number")]
    [InlineData(InputType.Password, "password")]
    [InlineData(InputType.Search, "search")]
    [InlineData(InputType.Tel, "tel")]
    [InlineData(InputType.Text, "text")]
    [InlineData(InputType.Url, "url")]
    public void InputType_Serialization(InputType type, string expected)
    {
        var cut = RenderComponent<IgbInput>(p => p.Add(x => x.DisplayType, type));
        Assert.Equal(expected, cut.Find("igc-input").GetAttribute("type"));
    }

    // ToggleLabelPosition
    [Theory]
    [InlineData(ToggleLabelPosition.After, "after")]
    [InlineData(ToggleLabelPosition.Before, "before")]
    public void ToggleLabelPosition_Serialization(ToggleLabelPosition pos, string expected)
    {
        var cut = RenderComponent<IgbCheckbox>(p => p.Add(x => x.LabelPosition, pos));
        Assert.Equal(expected, cut.Find("igc-checkbox").GetAttribute("label-position"));
    }

    // BadgeShape
    [Theory]
    [InlineData(BadgeShape.Rounded, "rounded")]
    [InlineData(BadgeShape.Square, "square")]
    public void BadgeShape_Serialization(BadgeShape shape, string expected)
    {
        var cut = RenderComponent<IgbBadge>(p => p.Add(x => x.Shape, shape));
        Assert.Equal(expected, cut.Find("igc-badge").GetAttribute("shape"));
    }

    // AvatarShape
    [Theory]
    [InlineData(AvatarShape.Circle, "circle")]
    [InlineData(AvatarShape.Rounded, "rounded")]
    [InlineData(AvatarShape.Square, "square")]
    public void AvatarShape_Serialization(AvatarShape shape, string expected)
    {
        var cut = RenderComponent<IgbAvatar>(p => p.Add(x => x.Shape, shape));
        Assert.Equal(expected, cut.Find("igc-avatar").GetAttribute("shape"));
    }

    // StyleVariant
    [Theory]
    [InlineData(StyleVariant.Primary, "primary")]
    [InlineData(StyleVariant.Info, "info")]
    [InlineData(StyleVariant.Success, "success")]
    [InlineData(StyleVariant.Warning, "warning")]
    [InlineData(StyleVariant.Danger, "danger")]
    public void StyleVariant_Serialization(StyleVariant variant, string expected)
    {
        var cut = RenderComponent<IgbLinearProgress>(p => p.Add(x => x.Variant, variant));
        Assert.Equal(expected, cut.Find("igc-linear-progress").GetAttribute("variant"));
    }

    // NavDrawerPosition
    [Theory]
    [InlineData(NavDrawerPosition.Start, "start")]
    [InlineData(NavDrawerPosition.End, "end")]
    [InlineData(NavDrawerPosition.Top, "top")]
    [InlineData(NavDrawerPosition.Bottom, "bottom")]
    [InlineData(NavDrawerPosition.Relative, "relative")]
    public void NavDrawerPosition_Serialization(NavDrawerPosition pos, string expected)
    {
        var cut = RenderComponent<IgbNavDrawer>(p => p.Add(x => x.Position, pos));
        Assert.Equal(expected, cut.Find("igc-nav-drawer").GetAttribute("position"));
    }

    // TextareaResize
    [Theory]
    [InlineData(TextareaResize.None, "none")]
    [InlineData(TextareaResize.Vertical, "vertical")]
    [InlineData(TextareaResize.Auto, "auto")]
    public void TextareaResize_Serialization(TextareaResize resize, string expected)
    {
        var cut = RenderComponent<IgbTextarea>(p => p.Add(x => x.Resize, resize));
        Assert.Equal(expected, cut.Find("igc-textarea").GetAttribute("resize"));
    }

    // TextareaWrap
    [Theory]
    [InlineData(TextareaWrap.Hard, "hard")]
    [InlineData(TextareaWrap.Soft, "soft")]
    [InlineData(TextareaWrap.Off, "off")]
    public void TextareaWrap_Serialization(TextareaWrap wrap, string expected)
    {
        var cut = RenderComponent<IgbTextarea>(p => p.Add(x => x.Wrap, wrap));
        Assert.Equal(expected, cut.Find("igc-textarea").GetAttribute("wrap"));
    }

    // TreeSelection
    [Theory]
    [InlineData(TreeSelection.None, "none")]
    [InlineData(TreeSelection.Multiple, "multiple")]
    [InlineData(TreeSelection.Cascade, "cascade")]
    public void TreeSelection_Serialization(TreeSelection sel, string expected)
    {
        var cut = RenderComponent<IgbTree>(p => p.Add(x => x.Selection, sel));
        Assert.Equal(expected, cut.Find("igc-tree").GetAttribute("selection"));
    }

    // ButtonGroupSelection
    [Theory]
    [InlineData(ButtonGroupSelection.Single, "single")]
    [InlineData(ButtonGroupSelection.SingleRequired, "single-required")]
    [InlineData(ButtonGroupSelection.Multiple, "multiple")]
    public void ButtonGroupSelection_Serialization(ButtonGroupSelection sel, string expected)
    {
        var cut = RenderComponent<IgbButtonGroup>(p => p.Add(x => x.Selection, sel));
        Assert.Equal(expected, cut.Find("igc-button-group").GetAttribute("selection"));
    }

    // TabsAlignment
    [Theory]
    [InlineData(TabsAlignment.Start, "start")]
    [InlineData(TabsAlignment.End, "end")]
    [InlineData(TabsAlignment.Center, "center")]
    [InlineData(TabsAlignment.Justify, "justify")]
    public void TabsAlignment_Serialization(TabsAlignment alignment, string expected)
    {
        var cut = RenderComponent<IgbTabs>(p => p.Add(x => x.Alignment, alignment));
        Assert.Equal(expected, cut.Find("igc-tabs").GetAttribute("alignment"));
    }

    // TabsActivation
    [Theory]
    [InlineData(TabsActivation.Auto, "auto")]
    [InlineData(TabsActivation.Manual, "manual")]
    public void TabsActivation_Serialization(TabsActivation activation, string expected)
    {
        var cut = RenderComponent<IgbTabs>(p => p.Add(x => x.Activation, activation));
        Assert.Equal(expected, cut.Find("igc-tabs").GetAttribute("activation"));
    }

    // SliderTickOrientation
    [Theory]
    [InlineData(SliderTickOrientation.Start, "start")]
    [InlineData(SliderTickOrientation.End, "end")]
    [InlineData(SliderTickOrientation.Mirror, "mirror")]
    public void SliderTickOrientation_Serialization(SliderTickOrientation orient, string expected)
    {
        var cut = RenderComponent<IgbSlider>(p => p.Add(x => x.TickOrientation, orient));
        Assert.Equal(expected, cut.Find("igc-slider").GetAttribute("tick-orientation"));
    }

    // StepperOrientation
    [Theory]
    [InlineData(StepperOrientation.Horizontal, "horizontal")]
    [InlineData(StepperOrientation.Vertical, "vertical")]
    public void StepperOrientation_Serialization(StepperOrientation orient, string expected)
    {
        var cut = RenderComponent<IgbStepper>(p => p.Add(x => x.Orientation, orient));
        Assert.Equal(expected, cut.Find("igc-stepper").GetAttribute("orientation"));
    }

    // StepperStepType
    [Theory]
    [InlineData(StepperStepType.Indicator, "indicator")]
    [InlineData(StepperStepType.Title, "title")]
    [InlineData(StepperStepType.Full, "full")]
    public void StepperStepType_Serialization(StepperStepType type, string expected)
    {
        var cut = RenderComponent<IgbStepper>(p => p.Add(x => x.StepType, type));
        Assert.Equal(expected, cut.Find("igc-stepper").GetAttribute("step-type"));
    }

    // StepperTitlePosition
    [Theory]
    [InlineData(StepperTitlePosition.Bottom, "bottom")]
    [InlineData(StepperTitlePosition.Top, "top")]
    [InlineData(StepperTitlePosition.End, "end")]
    [InlineData(StepperTitlePosition.Start, "start")]
    public void StepperTitlePosition_Serialization(StepperTitlePosition pos, string expected)
    {
        var cut = RenderComponent<IgbStepper>(p => p.Add(x => x.TitlePosition, pos));
        Assert.Equal(expected, cut.Find("igc-stepper").GetAttribute("title-position"));
    }

    // ExpansionPanelIndicatorPosition
    [Theory]
    [InlineData(ExpansionPanelIndicatorPosition.Start, "start")]
    [InlineData(ExpansionPanelIndicatorPosition.End, "end")]
    [InlineData(ExpansionPanelIndicatorPosition.None, "none")]
    public void ExpansionPanelIndicatorPosition_Serialization(ExpansionPanelIndicatorPosition pos, string expected)
    {
        var cut = RenderComponent<IgbExpansionPanel>(p => p.Add(x => x.IndicatorPosition, pos));
        Assert.Equal(expected, cut.Find("igc-expansion-panel").GetAttribute("indicator-position"));
    }

    // IconButtonVariant
    [Theory]
    [InlineData(IconButtonVariant.Contained, "contained")]
    [InlineData(IconButtonVariant.Outlined, "outlined")]
    [InlineData(IconButtonVariant.Flat, "flat")]
    public void IconButtonVariant_Serialization(IconButtonVariant variant, string expected)
    {
        var cut = RenderComponent<IgbIconButton>(p => p.Add(x => x.Variant, variant));
        Assert.Equal(expected, cut.Find("igc-icon-button").GetAttribute("variant"));
    }

    // MaskInputValueMode
    [Theory]
    [InlineData(MaskInputValueMode.Raw, "raw")]
    [InlineData(MaskInputValueMode.WithFormatting, "withFormatting")]
    public void MaskInputValueMode_Serialization(MaskInputValueMode mode, string expected)
    {
        var cut = RenderComponent<IgbMaskInput>(p => p.Add(x => x.ValueMode, mode));
        Assert.Equal(expected, cut.Find("igc-mask-input").GetAttribute("value-mode"));
    }

    // TileManagerResizeMode - uses WCEnumName
    [Theory]
    [InlineData(TileManagerResizeMode.None, "none")]
    [InlineData(TileManagerResizeMode.Always, "always")]
    [InlineData(TileManagerResizeMode.Hover, "hover")]
    public void TileManagerResizeMode_Serialization(TileManagerResizeMode mode, string expected)
    {
        var cut = RenderComponent<IgbTileManager>(p => p.Add(x => x.ResizeMode, mode));
        Assert.Equal(expected, cut.Find("igc-tile-manager").GetAttribute("resize-mode"));
    }

    // TileManagerDragMode - uses WCEnumName with "tile-header"
    [Theory]
    [InlineData(TileManagerDragMode.None, "none")]
    [InlineData(TileManagerDragMode.Tile, "tile")]
    [InlineData(TileManagerDragMode.TileHeader, "tile-header")]
    public void TileManagerDragMode_Serialization(TileManagerDragMode mode, string expected)
    {
        var cut = RenderComponent<IgbTileManager>(p => p.Add(x => x.DragMode, mode));
        Assert.Equal(expected, cut.Find("igc-tile-manager").GetAttribute("drag-mode"));
    }

    // HorizontalTransitionAnimation (Carousel)
    [Theory]
    [InlineData(HorizontalTransitionAnimation.None, "none")]
    [InlineData(HorizontalTransitionAnimation.Slide, "slide")]
    [InlineData(HorizontalTransitionAnimation.Fade, "fade")]
    public void CarouselAnimationType_Serialization(HorizontalTransitionAnimation anim, string expected)
    {
        var cut = RenderComponent<IgbCarousel>(p => p.Add(x => x.AnimationType, anim));
        Assert.Equal(expected, cut.Find("igc-carousel").GetAttribute("animation-type"));
    }

    // CarouselIndicatorsOrientation
    [Theory]
    [InlineData(CarouselIndicatorsOrientation.Start, "start")]
    [InlineData(CarouselIndicatorsOrientation.End, "end")]
    public void CarouselIndicatorsOrientation_Serialization(CarouselIndicatorsOrientation orient, string expected)
    {
        var cut = RenderComponent<IgbCarousel>(p => p.Add(x => x.IndicatorsOrientation, orient));
        Assert.Equal(expected, cut.Find("igc-carousel").GetAttribute("indicators-orientation"));
    }
}
