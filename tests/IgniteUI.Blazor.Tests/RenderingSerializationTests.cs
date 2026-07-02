using Bunit;
using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests that exercise the rendering/serialization code paths by rendering
/// components with multiple properties set and verifying the HTML output attributes.
/// </summary>
public class RenderingSerializationTests : BlazorComponentTestBase
{
    [Fact]
    public void Button_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbButton>(p => p
            .Add(x => x.Variant, ButtonVariant.Outlined)
            .Add(x => x.DisplayType, ButtonBaseType.Submit)
            .Add(x => x.Disabled, true)
            .Add(x => x.Href, "https://example.com")
            .Add(x => x.Target, ButtonBaseTarget._blank)
            .Add(x => x.Rel, "noopener")
            .Add(x => x.Download, "file.pdf"));

        var el = cut.Find("igc-button");
        Assert.Equal("outlined", el.GetAttribute("variant"));
        Assert.Equal("submit", el.GetAttribute("type"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.Equal("https://example.com", el.GetAttribute("href"));
        Assert.Equal("_blank", el.GetAttribute("target"));
        Assert.Equal("noopener", el.GetAttribute("rel"));
        Assert.Equal("file.pdf", el.GetAttribute("download"));
    }

    [Fact]
    public void Input_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbInput>(p => p
            .Add(x => x.Value, "test")
            .Add(x => x.DisplayType, InputType.Password)
            .Add(x => x.Placeholder, "Enter value")
            .Add(x => x.Label, "Password")
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.ReadOnly, true)
            .Add(x => x.Outlined, true));

        var el = cut.Find("igc-input");
        Assert.Equal("test", el.GetAttribute("value"));
        Assert.Equal("password", el.GetAttribute("type"));
        Assert.Equal("Enter value", el.GetAttribute("placeholder"));
        Assert.Equal("Password", el.GetAttribute("label"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.NotNull(el.GetAttribute("readonly"));
        Assert.NotNull(el.GetAttribute("outlined"));
    }

    [Fact]
    public void Textarea_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTextarea>(p => p
            .Add(x => x.Value, "content")
            .Add(x => x.Placeholder, "Type here")
            .Add(x => x.Label, "Message")
            .Add(x => x.Rows, 5)
            .Add(x => x.Disabled, true)
            .Add(x => x.Outlined, true)
            .Add(x => x.Resize, TextareaResize.Vertical));

        var el = cut.Find("igc-textarea");
        Assert.Equal("content", el.GetAttribute("value"));
        Assert.Equal("Type here", el.GetAttribute("placeholder"));
        Assert.Equal("Message", el.GetAttribute("label"));
        Assert.Equal("5", el.GetAttribute("rows"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("outlined"));
        Assert.Equal("vertical", el.GetAttribute("resize"));
    }

    [Fact]
    public void Checkbox_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbCheckbox>(p => p
            .Add(x => x.Checked, true)
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.Indeterminate, true)
            .Add(x => x.Value, "accept")
            .Add(x => x.LabelPosition, ToggleLabelPosition.Before));

        var el = cut.Find("igc-checkbox");
        Assert.NotNull(el.GetAttribute("checked"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.NotNull(el.GetAttribute("indeterminate"));
        Assert.Equal("accept", el.GetAttribute("value"));
        Assert.Equal("before", el.GetAttribute("label-position"));
    }

    [Fact]
    public void Switch_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbSwitch>(p => p
            .Add(x => x.Checked, true)
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.Value, "on")
            .Add(x => x.LabelPosition, ToggleLabelPosition.Before));

        var el = cut.Find("igc-switch");
        Assert.NotNull(el.GetAttribute("checked"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.Equal("on", el.GetAttribute("value"));
        Assert.Equal("before", el.GetAttribute("label-position"));
    }

    [Fact]
    public void Radio_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbRadio>(p => p
            .Add(x => x.Value, "option-a")
            .Add(x => x.Checked, true)
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.LabelPosition, ToggleLabelPosition.Before));

        var el = cut.Find("igc-radio");
        Assert.Equal("option-a", el.GetAttribute("value"));
        Assert.NotNull(el.GetAttribute("checked"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.Equal("before", el.GetAttribute("label-position"));
    }

    [Fact]
    public void Slider_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbSlider>(p => p
            .Add(x => x.Value, 50)
            .Add(x => x.Min, 0)
            .Add(x => x.Max, 100)
            .Add(x => x.Step, 10)
            .Add(x => x.Disabled, true)
            .Add(x => x.DiscreteTrack, true)
            .Add(x => x.HideTooltip, true)
            .Add(x => x.PrimaryTicks, 5)
            .Add(x => x.SecondaryTicks, 3));

        var el = cut.Find("igc-slider");
        Assert.Equal("50", el.GetAttribute("value"));
        Assert.Equal("0", el.GetAttribute("min"));
        Assert.Equal("100", el.GetAttribute("max"));
        Assert.Equal("10", el.GetAttribute("step"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("discrete-track"));
        Assert.NotNull(el.GetAttribute("hide-tooltip"));
        Assert.Equal("5", el.GetAttribute("primary-ticks"));
        Assert.Equal("3", el.GetAttribute("secondary-ticks"));
    }

    [Fact]
    public void RangeSlider_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbRangeSlider>(p => p
            .Add(x => x.Lower, 20)
            .Add(x => x.Upper, 80)
            .Add(x => x.Min, 0)
            .Add(x => x.Max, 100)
            .Add(x => x.Step, 5)
            .Add(x => x.Disabled, true));

        var el = cut.Find("igc-range-slider");
        Assert.Equal("20", el.GetAttribute("lower"));
        Assert.Equal("80", el.GetAttribute("upper"));
        Assert.Equal("0", el.GetAttribute("min"));
        Assert.Equal("100", el.GetAttribute("max"));
        Assert.Equal("5", el.GetAttribute("step"));
        Assert.NotNull(el.GetAttribute("disabled"));
    }

    [Fact]
    public void Rating_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbRating>(p => p
            .Add(x => x.Value, 4)
            .Add(x => x.Max, 10)
            .Add(x => x.ReadOnly, true)
            .Add(x => x.Disabled, true)
            .Add(x => x.Single, true)
            .Add(x => x.AllowReset, true)
            .Add(x => x.HoverPreview, true)
            .Add(x => x.Label, "Quality"));

        var el = cut.Find("igc-rating");
        Assert.Equal("4", el.GetAttribute("value"));
        Assert.Equal("10", el.GetAttribute("max"));
        Assert.NotNull(el.GetAttribute("readonly"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("single"));
        Assert.NotNull(el.GetAttribute("allow-reset"));
        Assert.NotNull(el.GetAttribute("hover-preview"));
        Assert.Equal("Quality", el.GetAttribute("label"));
    }

    [Fact]
    public void Chip_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbChip>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.Removable, true)
            .Add(x => x.Selectable, true)
            .Add(x => x.Selected, true)
            .Add(x => x.Variant, StyleVariant.Success));

        var el = cut.Find("igc-chip");
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("removable"));
        Assert.NotNull(el.GetAttribute("selectable"));
        Assert.NotNull(el.GetAttribute("selected"));
        Assert.Equal("success", el.GetAttribute("variant"));
    }

    [Fact]
    public void ExpansionPanel_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbExpansionPanel>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.Disabled, true)
            .Add(x => x.IndicatorPosition, ExpansionPanelIndicatorPosition.End));

        var el = cut.Find("igc-expansion-panel");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.Equal("end", el.GetAttribute("indicator-position"));
    }

    [Fact]
    public void Dialog_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbDialog>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.Title, "Test Title")
            .Add(x => x.KeepOpenOnEscape, true)
            .Add(x => x.CloseOnOutsideClick, true)
            .Add(x => x.HideDefaultAction, true));

        var el = cut.Find("igc-dialog");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.NotNull(el.GetAttribute("keep-open-on-escape"));
        Assert.NotNull(el.GetAttribute("close-on-outside-click"));
        Assert.NotNull(el.GetAttribute("hide-default-action"));
    }

    [Fact]
    public void Snackbar_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbSnackbar>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.DisplayTime, 5000)
            .Add(x => x.KeepOpen, true)
            .Add(x => x.ActionText, "Retry"));

        var el = cut.Find("igc-snackbar");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.Equal("5000", el.GetAttribute("display-time"));
        Assert.NotNull(el.GetAttribute("keep-open"));
        Assert.Equal("Retry", el.GetAttribute("action-text"));
    }

    [Fact]
    public void Toast_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbToast>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.DisplayTime, 3000)
            .Add(x => x.KeepOpen, true));

        var el = cut.Find("igc-toast");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.Equal("3000", el.GetAttribute("display-time"));
        Assert.NotNull(el.GetAttribute("keep-open"));
    }

    [Fact]
    public void LinearProgress_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbLinearProgress>(p => p
            .Add(x => x.Value, 75)
            .Add(x => x.Max, 100)
            .Add(x => x.Variant, StyleVariant.Success)
            .Add(x => x.Indeterminate, true)
            .Add(x => x.Striped, true)
            .Add(x => x.HideLabel, true)
            .Add(x => x.AnimationDuration, 500));

        var el = cut.Find("igc-linear-progress");
        Assert.Equal("75", el.GetAttribute("value"));
        Assert.Equal("100", el.GetAttribute("max"));
        Assert.Equal("success", el.GetAttribute("variant"));
        Assert.NotNull(el.GetAttribute("indeterminate"));
        Assert.NotNull(el.GetAttribute("striped"));
        Assert.NotNull(el.GetAttribute("hide-label"));
        Assert.Equal("500", el.GetAttribute("animation-duration"));
    }

    [Fact]
    public void CircularProgress_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbCircularProgress>(p => p
            .Add(x => x.Value, 60)
            .Add(x => x.Max, 100)
            .Add(x => x.Variant, StyleVariant.Danger)
            .Add(x => x.Indeterminate, true)
            .Add(x => x.HideLabel, true)
            .Add(x => x.AnimationDuration, 800));

        var el = cut.Find("igc-circular-progress");
        Assert.Equal("60", el.GetAttribute("value"));
        Assert.Equal("100", el.GetAttribute("max"));
        Assert.Equal("danger", el.GetAttribute("variant"));
        Assert.NotNull(el.GetAttribute("indeterminate"));
        Assert.NotNull(el.GetAttribute("hide-label"));
        Assert.Equal("800", el.GetAttribute("animation-duration"));
    }

    [Fact]
    public void NavDrawer_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbNavDrawer>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.Position, NavDrawerPosition.End));

        var el = cut.Find("igc-nav-drawer");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.Equal("end", el.GetAttribute("position"));
    }

    [Fact]
    public void NavDrawerItem_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbNavDrawerItem>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.Active, true));

        var el = cut.Find("igc-nav-drawer-item");
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("active"));
    }

    [Fact]
    public void Carousel_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbCarousel>(p => p
            .Add(x => x.DisableLoop, true)
            .Add(x => x.DisablePauseOnInteraction, true)
            .Add(x => x.HideNavigation, true)
            .Add(x => x.HideIndicators, true)
            .Add(x => x.Vertical, true)
            .Add(x => x.Interval, 5000)
            .Add(x => x.MaximumIndicatorsCount, 10)
            .Add(x => x.AnimationType, HorizontalTransitionAnimation.Fade));

        var el = cut.Find("igc-carousel");
        Assert.NotNull(el.GetAttribute("disable-loop"));
        Assert.NotNull(el.GetAttribute("disable-pause-on-interaction"));
        Assert.NotNull(el.GetAttribute("hide-navigation"));
        Assert.NotNull(el.GetAttribute("hide-indicators"));
        Assert.NotNull(el.GetAttribute("vertical"));
        Assert.Equal("5000", el.GetAttribute("interval"));
        Assert.Equal("10", el.GetAttribute("maximum-indicators-count"));
        Assert.Equal("fade", el.GetAttribute("animation-type"));
    }

    [Fact]
    public void Tree_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTree>(p => p
            .Add(x => x.SingleBranchExpand, true)
            .Add(x => x.ToggleNodeOnClick, true)
            .Add(x => x.Selection, TreeSelection.Cascade));

        var el = cut.Find("igc-tree");
        Assert.NotNull(el.GetAttribute("single-branch-expand"));
        Assert.NotNull(el.GetAttribute("toggle-node-on-click"));
        Assert.Equal("cascade", el.GetAttribute("selection"));
    }

    [Fact]
    public void TreeItem_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTreeItem>(p => p
            .Add(x => x.Label, "Item 1")
            .Add(x => x.Expanded, true)
            .Add(x => x.Active, true)
            .Add(x => x.Disabled, true)
            .Add(x => x.Selected, true));

        var el = cut.Find("igc-tree-item");
        Assert.Equal("Item 1", el.GetAttribute("label"));
        Assert.NotNull(el.GetAttribute("expanded"));
        Assert.NotNull(el.GetAttribute("active"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("selected"));
    }

    [Fact]
    public void Tabs_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTabs>(p => p
            .Add(x => x.Alignment, TabsAlignment.Center)
            .Add(x => x.Activation, TabsActivation.Manual));

        var el = cut.Find("igc-tabs");
        Assert.Equal("center", el.GetAttribute("alignment"));
        Assert.Equal("manual", el.GetAttribute("activation"));
    }

    [Fact]
    public void Tab_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTab>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.Selected, true));

        var el = cut.Find("igc-tab");
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("selected"));
    }

    [Fact]
    public void Stepper_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbStepper>(p => p
            .Add(x => x.Orientation, StepperOrientation.Vertical)
            .Add(x => x.StepType, StepperStepType.Full)
            .Add(x => x.TitlePosition, StepperTitlePosition.End)
            .Add(x => x.ContentTop, true));

        var el = cut.Find("igc-stepper");
        Assert.Equal("vertical", el.GetAttribute("orientation"));
        Assert.Equal("full", el.GetAttribute("step-type"));
        Assert.Equal("end", el.GetAttribute("title-position"));
        Assert.NotNull(el.GetAttribute("content-top"));
    }

    [Fact]
    public void Step_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbStep>(p => p
            .Add(x => x.Disabled, true));

        var el = cut.Find("igc-step");
        Assert.NotNull(el.GetAttribute("disabled"));
    }

    [Fact]
    public void ToggleButton_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbToggleButton>(p => p
            .Add(x => x.Value, "bold")
            .Add(x => x.Selected, true)
            .Add(x => x.Disabled, true));

        var el = cut.Find("igc-toggle-button");
        Assert.Equal("bold", el.GetAttribute("value"));
        Assert.NotNull(el.GetAttribute("selected"));
        Assert.NotNull(el.GetAttribute("disabled"));
    }

    [Fact]
    public void IconButton_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbIconButton>(p => p
            .Add(x => x.IconName, "search")
            .Add(x => x.Collection, "material")
            .Add(x => x.Variant, IconButtonVariant.Outlined)
            .Add(x => x.Disabled, true)
            .Add(x => x.Href, "https://example.com"));

        var el = cut.Find("igc-icon-button");
        Assert.Equal("search", el.GetAttribute("name"));
        Assert.Equal("material", el.GetAttribute("collection"));
        Assert.Equal("outlined", el.GetAttribute("variant"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.Equal("https://example.com", el.GetAttribute("href"));
    }

    [Fact]
    public void Avatar_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbAvatar>(p => p
            .Add(x => x.Src, "img.png")
            .Add(x => x.Alt, "User")
            .Add(x => x.Initials, "AB")
            .Add(x => x.Shape, AvatarShape.Rounded));

        var el = cut.Find("igc-avatar");
        Assert.Equal("img.png", el.GetAttribute("src"));
        Assert.Equal("User", el.GetAttribute("alt"));
        Assert.Equal("AB", el.GetAttribute("initials"));
        Assert.Equal("rounded", el.GetAttribute("shape"));
    }

    [Fact]
    public void Badge_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbBadge>(p => p
            .Add(x => x.Variant, StyleVariant.Info)
            .Add(x => x.Outlined, true)
            .Add(x => x.Shape, BadgeShape.Square));

        var el = cut.Find("igc-badge");
        Assert.Equal("info", el.GetAttribute("variant"));
        Assert.NotNull(el.GetAttribute("outlined"));
        Assert.Equal("square", el.GetAttribute("shape"));
    }

    [Fact]
    public void Icon_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbIcon>(p => p
            .Add(x => x.IconName, "home")
            .Add(x => x.Collection, "material")
            .Add(x => x.Mirrored, true));

        var el = cut.Find("igc-icon");
        Assert.Equal("home", el.GetAttribute("name"));
        Assert.Equal("material", el.GetAttribute("collection"));
        Assert.NotNull(el.GetAttribute("mirrored"));
    }

    [Fact]
    public void Accordion_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbAccordion>(p => p
            .Add(x => x.SingleExpand, true));

        var el = cut.Find("igc-accordion");
        Assert.NotNull(el.GetAttribute("single-expand"));
    }

    [Fact]
    public void ButtonGroup_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbButtonGroup>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.Selection, ButtonGroupSelection.Multiple));

        var el = cut.Find("igc-button-group");
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.Equal("multiple", el.GetAttribute("selection"));
    }

    [Fact]
    public void Select_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbSelect>(p => p
            .Add(x => x.Label, "Choose")
            .Add(x => x.Placeholder, "Select...")
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.Outlined, true)
            .Add(x => x.Open, true));

        var el = cut.Find("igc-select");
        Assert.Equal("Choose", el.GetAttribute("label"));
        Assert.Equal("Select...", el.GetAttribute("placeholder"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.NotNull(el.GetAttribute("outlined"));
        Assert.NotNull(el.GetAttribute("open"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void DateTimeInput_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbDateTimeInput>(p => p
            .Add(x => x.InputFormat, "dd/MM/yyyy")
            .Add(x => x.Label, "Date")
            .Add(x => x.Placeholder, "Enter date")
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.Outlined, true));

        var el = cut.Find("igc-date-time-input");
        Assert.Equal("dd/MM/yyyy", el.GetAttribute("input-format"));
        Assert.Equal("Date", el.GetAttribute("label"));
        Assert.Equal("Enter date", el.GetAttribute("placeholder"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.NotNull(el.GetAttribute("outlined"));
    }

    [Fact(Skip = "Indirect rendering, awaiting render simplification.")]
    public void MaskInput_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbMaskInput>(p => p
            .Add(x => x.Mask, "(000) 000-0000")
            .Add(x => x.Label, "Phone")
            .Add(x => x.Placeholder, "Enter phone")
            .Add(x => x.Disabled, true)
            .Add(x => x.Required, true)
            .Add(x => x.Outlined, true)
            .Add(x => x.ValueMode, MaskInputValueMode.Raw));

        var el = cut.Find("igc-mask-input");
        Assert.Equal("(000) 000-0000", el.GetAttribute("mask"));
        Assert.Equal("Phone", el.GetAttribute("label"));
        Assert.Equal("Enter phone", el.GetAttribute("placeholder"));
        Assert.NotNull(el.GetAttribute("disabled"));
        Assert.NotNull(el.GetAttribute("required"));
        Assert.NotNull(el.GetAttribute("outlined"));
        Assert.Equal("raw", el.GetAttribute("value-mode"));
    }

    [Fact]
    public void Dropdown_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbDropdown>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.KeepOpenOnSelect, true));

        var el = cut.Find("igc-dropdown");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.NotNull(el.GetAttribute("keep-open-on-select"));
    }

    [Fact]
    public void TileManager_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTileManager>(p => p
            .Add(x => x.ColumnCount, 4)
            .Add(x => x.ResizeMode, TileManagerResizeMode.Always)
            .Add(x => x.DragMode, TileManagerDragMode.TileHeader)
            .Add(x => x.MinColumnWidth, "200px")
            .Add(x => x.MinRowHeight, "150px"));

        var el = cut.Find("igc-tile-manager");
        Assert.Equal("4", el.GetAttribute("column-count"));
        Assert.Equal("always", el.GetAttribute("resize-mode"));
        Assert.Equal("tile-header", el.GetAttribute("drag-mode"));
        Assert.Equal("200px", el.GetAttribute("min-column-width"));
        Assert.Equal("150px", el.GetAttribute("min-row-height"));
    }

    [Fact]
    public void Tile_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTile>(p => p
            .Add(x => x.ColSpan, 2)
            .Add(x => x.RowSpan, 3)
            .Add(x => x.ColStart, 1)
            .Add(x => x.RowStart, 2));

        var el = cut.Find("igc-tile");
        Assert.Equal("2", el.GetAttribute("col-span"));
        Assert.Equal("3", el.GetAttribute("row-span"));
        Assert.Equal("1", el.GetAttribute("col-start"));
        Assert.Equal("2", el.GetAttribute("row-start"));
    }

    [Fact]
    public void Tooltip_RendersAllAttributes()
    {
        var cut = RenderComponent<IgbTooltip>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.WithArrow, true)
            .Add(x => x.Offset, 10)
            .Add(x => x.Anchor, "btn1")
            .Add(x => x.ShowDelay, 500)
            .Add(x => x.HideDelay, 300)
            .Add(x => x.Message, "Hi")
            .Add(x => x.Sticky, true));

        var el = cut.Find("igc-tooltip");
        Assert.NotNull(el.GetAttribute("open"));
        Assert.NotNull(el.GetAttribute("with-arrow"));
        Assert.Equal("10", el.GetAttribute("offset"));
        Assert.Equal("btn1", el.GetAttribute("anchor"));
        Assert.Equal("500", el.GetAttribute("show-delay"));
        Assert.Equal("300", el.GetAttribute("hide-delay"));
        Assert.Equal("Hi", el.GetAttribute("message"));
        Assert.NotNull(el.GetAttribute("sticky"));
    }
}
