using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Tests exercising property dirty tracking and serialization paths.
/// Each property set marks it dirty and triggers serialization during render.
/// </summary>
public class PropertySerializationTests : BlazorComponentTestBase
{
    // Button properties
    [Theory]
    [InlineData("https://example.com")]
    [InlineData("/page")]
    [InlineData("")]
    public void Button_Href_Values(string href)
    {
        var btn = new IgbButton();
        btn.Href = href;
        Assert.Equal(href, btn.Href);
    }

    [Theory]
    [InlineData(ButtonBaseTarget._blank)]
    [InlineData(ButtonBaseTarget._self)]
    public void Button_Target_Values(ButtonBaseTarget target)
    {
        var btn = new IgbButton();
        btn.Target = target;
        Assert.Equal(target, btn.Target);
    }

    [Fact]
    public void Button_Rel_Property()
    {
        var btn = new IgbButton();
        btn.Rel = "noopener";
        Assert.Equal("noopener", btn.Rel);
    }

    [Fact]
    public void Button_Download_Property()
    {
        var btn = new IgbButton();
        btn.Download = "file.pdf";
        Assert.Equal("file.pdf", btn.Download);
    }

    // Checkbox dirty tracking
    [Fact]
    public void Checkbox_AllProperties_SetCorrectly()
    {
        var cb = new IgbCheckbox();
        cb.Checked = true;
        cb.Disabled = true;
        cb.Required = true;
        cb.Invalid = true;
        cb.Indeterminate = true;
        cb.Value = "test-val";
        cb.LabelPosition = ToggleLabelPosition.Before;

        Assert.True(cb.Checked);
        Assert.True(cb.Disabled);
        Assert.True(cb.Required);
        Assert.True(cb.Invalid);
        Assert.True(cb.Indeterminate);
        Assert.Equal("test-val", cb.Value);
        Assert.Equal(ToggleLabelPosition.Before, cb.LabelPosition);
    }

    // Switch dirty tracking
    [Fact]
    public void Switch_AllProperties_SetCorrectly()
    {
        var sw = new IgbSwitch();
        sw.Checked = true;
        sw.Disabled = true;
        sw.Required = true;
        sw.Invalid = true;
        sw.Value = "on";
        sw.LabelPosition = ToggleLabelPosition.Before;

        Assert.True(sw.Checked);
        Assert.True(sw.Disabled);
        Assert.True(sw.Required);
        Assert.True(sw.Invalid);
        Assert.Equal("on", sw.Value);
        Assert.Equal(ToggleLabelPosition.Before, sw.LabelPosition);
    }

    // Input full property coverage
    [Fact]
    public void Input_AllProperties_SetCorrectly()
    {
        var input = new IgbInput();
        input.Value = "hello";
        input.DisplayType = InputType.Email;
        input.Placeholder = "Enter email";
        input.Label = "Email";
        input.Disabled = true;
        input.Required = true;
        input.ReadOnly = true;
        input.Invalid = true;
        input.Outlined = true;
        input.Autofocus = true;
        input.MinLength = 5;
        input.MaxLength = 100;
        input.Min = 0;
        input.Max = 999;
        input.Step = 1;
        input.Pattern = "[a-z]+";
        input.Autocomplete = "email";
        input.InputMode = "email";
        input.ValidateOnly = true;

        Assert.Equal("hello", input.Value);
        Assert.Equal(InputType.Email, input.DisplayType);
        Assert.Equal("Enter email", input.Placeholder);
        Assert.Equal("Email", input.Label);
        Assert.True(input.Disabled);
        Assert.True(input.Required);
        Assert.True(input.ReadOnly);
        Assert.True(input.Invalid);
        Assert.True(input.Outlined);
        Assert.True(input.Autofocus);
        Assert.Equal(5, input.MinLength);
        Assert.Equal(100, input.MaxLength);
        Assert.Equal(0, input.Min);
        Assert.Equal(999, input.Max);
        Assert.Equal(1, input.Step);
        Assert.Equal("[a-z]+", input.Pattern);
        Assert.Equal("email", input.Autocomplete);
        Assert.Equal("email", input.InputMode);
        Assert.True(input.ValidateOnly);
    }

    // Textarea full property coverage
    [Fact]
    public void Textarea_AllProperties_SetCorrectly()
    {
        var ta = new IgbTextarea();
        ta.Value = "text content";
        ta.Placeholder = "Enter text";
        ta.Label = "Description";
        ta.Rows = 5;
        ta.Resize = TextareaResize.Vertical;
        ta.Wrap = TextareaWrap.Hard;
        ta.MaxLength = 1000;
        ta.MinLength = 10;
        ta.Disabled = true;
        ta.Required = true;
        ta.ReadOnly = true;
        ta.Outlined = true;
        ta.Spellcheck = true;
        ta.InputMode = "text";
        ta.Autocomplete = "on";
        ta.ValidateOnly = true;

        Assert.Equal("text content", ta.Value);
        Assert.Equal("Enter text", ta.Placeholder);
        Assert.Equal("Description", ta.Label);
        Assert.Equal(5, ta.Rows);
        Assert.Equal(TextareaResize.Vertical, ta.Resize);
        Assert.Equal(TextareaWrap.Hard, ta.Wrap);
        Assert.Equal(1000, ta.MaxLength);
        Assert.Equal(10, ta.MinLength);
        Assert.True(ta.Disabled);
        Assert.True(ta.Required);
        Assert.True(ta.ReadOnly);
        Assert.True(ta.Outlined);
        Assert.True(ta.Spellcheck);
        Assert.Equal("text", ta.InputMode);
        Assert.Equal("on", ta.Autocomplete);
        Assert.True(ta.ValidateOnly);
    }

    // MaskInput full property coverage
    [Fact]
    public void MaskInput_AllProperties_SetCorrectly()
    {
        var mi = new IgbMaskInput();
        mi.Mask = "(000) 000-0000";
        mi.Value = "1234567890";
        mi.ValueMode = MaskInputValueMode.Raw;
        mi.Prompt = "_";
        mi.Label = "Phone";
        mi.Placeholder = "Enter phone";
        mi.Disabled = true;
        mi.Required = true;
        mi.ReadOnly = true;
        mi.Outlined = true;

        Assert.Equal("(000) 000-0000", mi.Mask);
        Assert.Equal("1234567890", mi.Value);
        Assert.Equal(MaskInputValueMode.Raw, mi.ValueMode);
        Assert.Equal("_", mi.Prompt);
        Assert.Equal("Phone", mi.Label);
        Assert.Equal("Enter phone", mi.Placeholder);
        Assert.True(mi.Disabled);
        Assert.True(mi.Required);
        Assert.True(mi.ReadOnly);
        Assert.True(mi.Outlined);
    }

    // Slider full property coverage
    [Fact]
    public void Slider_AllProperties_SetCorrectly()
    {
        var slider = new IgbSlider();
        slider.Value = 50;
        slider.Min = 0;
        slider.Max = 100;
        slider.Step = 5;
        slider.LowerBound = 10;
        slider.UpperBound = 90;
        slider.Disabled = true;
        slider.DiscreteTrack = true;
        slider.HideTooltip = true;
        slider.PrimaryTicks = 5;
        slider.SecondaryTicks = 3;
        slider.HidePrimaryLabels = true;
        slider.HideSecondaryLabels = true;
        slider.TickOrientation = SliderTickOrientation.Mirror;
        slider.Locale = "en-US";
        slider.ValueFormat = "{0}%";
        slider.Invalid = true;

        Assert.Equal(50, slider.Value);
        Assert.Equal(0, slider.Min);
        Assert.Equal(100, slider.Max);
        Assert.Equal(5, slider.Step);
        Assert.Equal(10, slider.LowerBound);
        Assert.Equal(90, slider.UpperBound);
        Assert.True(slider.Disabled);
        Assert.True(slider.DiscreteTrack);
        Assert.True(slider.HideTooltip);
        Assert.Equal(5, slider.PrimaryTicks);
        Assert.Equal(3, slider.SecondaryTicks);
        Assert.True(slider.HidePrimaryLabels);
        Assert.True(slider.HideSecondaryLabels);
        Assert.Equal(SliderTickOrientation.Mirror, slider.TickOrientation);
        Assert.Equal("en-US", slider.Locale);
        Assert.Equal("{0}%", slider.ValueFormat);
        Assert.True(slider.Invalid);
    }

    // RangeSlider full property coverage
    [Fact]
    public void RangeSlider_AllProperties_SetCorrectly()
    {
        var slider = new IgbRangeSlider();
        slider.Lower = 20;
        slider.Upper = 80;
        slider.Min = 0;
        slider.Max = 100;
        slider.Step = 10;
        slider.Disabled = true;

        Assert.Equal(20, slider.Lower);
        Assert.Equal(80, slider.Upper);
        Assert.Equal(0, slider.Min);
        Assert.Equal(100, slider.Max);
        Assert.Equal(10, slider.Step);
        Assert.True(slider.Disabled);
    }

    // Rating full property coverage
    [Fact]
    public void Rating_AllProperties_SetCorrectly()
    {
        var rating = new IgbRating();
        rating.Max = 10;
        rating.Value = 7;
        rating.Step = 0.5;
        rating.Label = "Rate this";
        rating.ReadOnly = true;
        rating.Disabled = true;
        rating.Single = true;
        rating.AllowReset = true;
        rating.HoverPreview = true;

        Assert.Equal(10, rating.Max);
        Assert.Equal(7, rating.Value);
        Assert.Equal(0.5, rating.Step);
        Assert.Equal("Rate this", rating.Label);
        Assert.True(rating.ReadOnly);
        Assert.True(rating.Disabled);
        Assert.True(rating.Single);
        Assert.True(rating.AllowReset);
        Assert.True(rating.HoverPreview);
    }

    // ExpansionPanel full property coverage
    [Fact]
    public void ExpansionPanel_AllProperties_SetCorrectly()
    {
        var panel = new IgbExpansionPanel();
        panel.Open = true;
        panel.Disabled = true;
        panel.IndicatorPosition = ExpansionPanelIndicatorPosition.End;

        Assert.True(panel.Open);
        Assert.True(panel.Disabled);
        Assert.Equal(ExpansionPanelIndicatorPosition.End, panel.IndicatorPosition);
    }

    // Dialog full property coverage
    [Fact]
    public void Dialog_AllProperties_SetCorrectly()
    {
        var dialog = new IgbDialog();
        dialog.Open = true;
        dialog.Title = "Confirm";
        dialog.KeepOpenOnEscape = true;
        dialog.CloseOnOutsideClick = true;
        dialog.HideDefaultAction = true;
        dialog.ReturnValue = "ok";

        Assert.True(dialog.Open);
        Assert.Equal("Confirm", dialog.Title);
        Assert.True(dialog.KeepOpenOnEscape);
        Assert.True(dialog.CloseOnOutsideClick);
        Assert.True(dialog.HideDefaultAction);
        Assert.Equal("ok", dialog.ReturnValue);
    }

    // Snackbar full property coverage
    [Fact]
    public void Snackbar_AllProperties_SetCorrectly()
    {
        var snackbar = new IgbSnackbar();
        snackbar.Open = true;
        snackbar.DisplayTime = 5000;
        snackbar.KeepOpen = true;
        snackbar.ActionText = "Undo";

        Assert.True(snackbar.Open);
        Assert.Equal(5000, snackbar.DisplayTime);
        Assert.True(snackbar.KeepOpen);
        Assert.Equal("Undo", snackbar.ActionText);
    }

    // Toast full property coverage
    [Fact]
    public void Toast_AllProperties_SetCorrectly()
    {
        var toast = new IgbToast();
        toast.Open = true;
        toast.DisplayTime = 3000;
        toast.KeepOpen = true;

        Assert.True(toast.Open);
        Assert.Equal(3000, toast.DisplayTime);
        Assert.True(toast.KeepOpen);
    }

    // Chip full property coverage
    [Fact]
    public void Chip_AllProperties_SetCorrectly()
    {
        var chip = new IgbChip();
        chip.Disabled = true;
        chip.Removable = true;
        chip.Selectable = true;
        chip.Selected = true;
        chip.Variant = StyleVariant.Success;

        Assert.True(chip.Disabled);
        Assert.True(chip.Removable);
        Assert.True(chip.Selectable);
        Assert.True(chip.Selected);
        Assert.Equal(StyleVariant.Success, chip.Variant);
    }

    // Badge full property coverage
    [Fact]
    public void Badge_AllProperties_SetCorrectly()
    {
        var badge = new IgbBadge();
        badge.Variant = StyleVariant.Danger;
        badge.Outlined = true;
        badge.Shape = BadgeShape.Square;

        Assert.Equal(StyleVariant.Danger, badge.Variant);
        Assert.True(badge.Outlined);
        Assert.Equal(BadgeShape.Square, badge.Shape);
    }

    // Avatar full property coverage
    [Fact]
    public void Avatar_AllProperties_SetCorrectly()
    {
        var avatar = new IgbAvatar();
        avatar.Src = "https://example.com/img.jpg";
        avatar.Alt = "User avatar";
        avatar.Initials = "JD";
        avatar.Shape = AvatarShape.Rounded;

        Assert.Equal("https://example.com/img.jpg", avatar.Src);
        Assert.Equal("User avatar", avatar.Alt);
        Assert.Equal("JD", avatar.Initials);
        Assert.Equal(AvatarShape.Rounded, avatar.Shape);
    }

    // Icon full property coverage
    [Fact]
    public void Icon_AllProperties_SetCorrectly()
    {
        var icon = new IgbIcon();
        icon.IconName = "home";
        icon.Collection = "material";
        icon.Mirrored = true;

        Assert.Equal("home", icon.IconName);
        Assert.Equal("material", icon.Collection);
        Assert.True(icon.Mirrored);
    }

    // Tree + TreeItem full property coverage
    [Fact]
    public void Tree_AllProperties_SetCorrectly()
    {
        var tree = new IgbTree();
        tree.SingleBranchExpand = true;
        tree.ToggleNodeOnClick = true;
        tree.Selection = TreeSelection.Cascade;

        Assert.True(tree.SingleBranchExpand);
        Assert.True(tree.ToggleNodeOnClick);
        Assert.Equal(TreeSelection.Cascade, tree.Selection);
    }

    [Fact]
    public void TreeItem_AllProperties_SetCorrectly()
    {
        var item = new IgbTreeItem();
        item.Label = "Node";
        item.Expanded = true;
        item.Active = true;
        item.Disabled = true;
        item.Selected = true;
        item.Loading = true;

        Assert.Equal("Node", item.Label);
        Assert.True(item.Expanded);
        Assert.True(item.Active);
        Assert.True(item.Disabled);
        Assert.True(item.Selected);
        Assert.True(item.Loading);
    }

    // Tooltip full property coverage
    [Fact]
    public void Tooltip_AllProperties_SetCorrectly()
    {
        var tooltip = new IgbTooltip();
        tooltip.Open = true;
        tooltip.WithArrow = true;
        tooltip.Offset = 10;
        tooltip.Anchor = "btn1";
        tooltip.ShowDelay = 500;
        tooltip.HideDelay = 300;
        tooltip.Message = "Hello";
        tooltip.Sticky = true;

        Assert.True(tooltip.Open);
        Assert.True(tooltip.WithArrow);
        Assert.Equal(10, tooltip.Offset);
        Assert.Equal("btn1", tooltip.Anchor);
        Assert.Equal(500, tooltip.ShowDelay);
        Assert.Equal(300, tooltip.HideDelay);
        Assert.Equal("Hello", tooltip.Message);
        Assert.True(tooltip.Sticky);
    }

    // TileManager full property coverage
    [Fact]
    public void TileManager_AllProperties_SetCorrectly()
    {
        var tm = new IgbTileManager();
        tm.ColumnCount = 4;
        tm.ResizeMode = TileManagerResizeMode.Always;
        tm.DragMode = TileManagerDragMode.TileHeader;
        tm.MinColumnWidth = "200px";
        tm.MinRowHeight = "150px";

        Assert.Equal(4, tm.ColumnCount);
        Assert.Equal(TileManagerResizeMode.Always, tm.ResizeMode);
        Assert.Equal(TileManagerDragMode.TileHeader, tm.DragMode);
        Assert.Equal("200px", tm.MinColumnWidth);
        Assert.Equal("150px", tm.MinRowHeight);
    }

    // Tile full property coverage
    [Fact]
    public void Tile_AllProperties_SetCorrectly()
    {
        var tile = new IgbTile();
        tile.ColSpan = 2;
        tile.RowSpan = 3;
        tile.ColStart = 1;
        tile.RowStart = 2;

        Assert.Equal(2, tile.ColSpan);
        Assert.Equal(3, tile.RowSpan);
        Assert.Equal(1, tile.ColStart);
        Assert.Equal(2, tile.RowStart);
    }

    // Carousel full property coverage
    [Fact]
    public void Carousel_AllProperties_SetCorrectly()
    {
        var carousel = new IgbCarousel();
        carousel.DisableLoop = true;
        carousel.DisablePauseOnInteraction = true;
        carousel.HideNavigation = true;
        carousel.HideIndicators = true;
        carousel.Vertical = true;
        carousel.IndicatorsOrientation = CarouselIndicatorsOrientation.Start;
        carousel.AnimationType = HorizontalTransitionAnimation.Fade;
        carousel.Interval = 5000;
        carousel.MaximumIndicatorsCount = 10;

        Assert.True(carousel.DisableLoop);
        Assert.True(carousel.DisablePauseOnInteraction);
        Assert.True(carousel.HideNavigation);
        Assert.True(carousel.HideIndicators);
        Assert.True(carousel.Vertical);
        Assert.Equal(CarouselIndicatorsOrientation.Start, carousel.IndicatorsOrientation);
        Assert.Equal(HorizontalTransitionAnimation.Fade, carousel.AnimationType);
        Assert.Equal(5000, carousel.Interval);
        Assert.Equal(10, carousel.MaximumIndicatorsCount);
    }

    // NavDrawer full property coverage
    [Fact]
    public void NavDrawer_AllProperties_SetCorrectly()
    {
        var drawer = new IgbNavDrawer();
        drawer.Open = true;
        drawer.Position = NavDrawerPosition.End;

        Assert.True(drawer.Open);
        Assert.Equal(NavDrawerPosition.End, drawer.Position);
    }

    [Fact]
    public void NavDrawerItem_AllProperties_SetCorrectly()
    {
        var item = new IgbNavDrawerItem();
        item.Disabled = true;
        item.Active = true;

        Assert.True(item.Disabled);
        Assert.True(item.Active);
    }

    // ButtonGroup full property coverage
    [Fact]
    public void ButtonGroup_AllProperties_SetCorrectly()
    {
        var bg = new IgbButtonGroup();
        bg.Disabled = true;
        bg.Selection = ButtonGroupSelection.Multiple;
        bg.Alignment = ContentOrientation.Vertical;

        Assert.True(bg.Disabled);
        Assert.Equal(ButtonGroupSelection.Multiple, bg.Selection);
        Assert.Equal(ContentOrientation.Vertical, bg.Alignment);
    }

    // ToggleButton full property coverage
    [Fact]
    public void ToggleButton_AllProperties_SetCorrectly()
    {
        var tb = new IgbToggleButton();
        tb.Value = "bold";
        tb.Selected = true;
        tb.Disabled = true;

        Assert.Equal("bold", tb.Value);
        Assert.True(tb.Selected);
        Assert.True(tb.Disabled);
    }

    // IconButton full property coverage
    [Fact]
    public void IconButton_AllProperties_SetCorrectly()
    {
        var btn = new IgbIconButton();
        btn.IconName = "search";
        btn.Collection = "material";
        btn.Mirrored = true;
        btn.Variant = IconButtonVariant.Outlined;
        btn.Disabled = true;
        btn.Href = "https://example.com";

        Assert.Equal("search", btn.IconName);
        Assert.Equal("material", btn.Collection);
        Assert.True(btn.Mirrored);
        Assert.Equal(IconButtonVariant.Outlined, btn.Variant);
        Assert.True(btn.Disabled);
        Assert.Equal("https://example.com", btn.Href);
    }

    // DateTimeInput full property coverage
    [Fact]
    public void DateTimeInput_AllProperties_SetCorrectly()
    {
        var dti = new IgbDateTimeInput();
        dti.InputFormat = "dd/MM/yyyy";
        dti.DisplayFormat = "MMMM dd, yyyy";
        dti.Label = "Date";
        dti.Placeholder = "Enter date";
        dti.Disabled = true;
        dti.Required = true;
        dti.ReadOnly = true;
        dti.Outlined = true;

        Assert.Equal("dd/MM/yyyy", dti.InputFormat);
        Assert.Equal("MMMM dd, yyyy", dti.DisplayFormat);
        Assert.Equal("Date", dti.Label);
        Assert.Equal("Enter date", dti.Placeholder);
        Assert.True(dti.Disabled);
        Assert.True(dti.Required);
        Assert.True(dti.ReadOnly);
        Assert.True(dti.Outlined);
    }

    // Linear/Circular Progress full property coverage
    [Fact]
    public void LinearProgress_AllProperties_SetCorrectly()
    {
        var p = new IgbLinearProgress();
        p.Value = 75;
        p.Max = 100;
        p.Variant = StyleVariant.Success;
        p.Indeterminate = true;
        p.Striped = true;
        p.HideLabel = true;
        p.LabelFormat = "{0}%";
        p.AnimationDuration = 500;

        Assert.Equal(75, p.Value);
        Assert.Equal(100, p.Max);
        Assert.Equal(StyleVariant.Success, p.Variant);
        Assert.True(p.Indeterminate);
        Assert.True(p.Striped);
        Assert.True(p.HideLabel);
        Assert.Equal("{0}%", p.LabelFormat);
        Assert.Equal(500, p.AnimationDuration);
    }

    [Fact]
    public void CircularProgress_AllProperties_SetCorrectly()
    {
        var p = new IgbCircularProgress();
        p.Value = 50;
        p.Max = 100;
        p.Variant = StyleVariant.Warning;
        p.Indeterminate = true;
        p.HideLabel = true;
        p.LabelFormat = "{0}/{1}";
        p.AnimationDuration = 800;

        Assert.Equal(50, p.Value);
        Assert.Equal(100, p.Max);
        Assert.Equal(StyleVariant.Warning, p.Variant);
        Assert.True(p.Indeterminate);
        Assert.True(p.HideLabel);
        Assert.Equal("{0}/{1}", p.LabelFormat);
        Assert.Equal(800, p.AnimationDuration);
    }

    // Select full property coverage
    [Fact]
    public void Select_AllProperties_SetCorrectly()
    {
        var select = new IgbSelect();
        select.Value = "opt1";
        select.Label = "Select option";
        select.Placeholder = "Choose...";
        select.Disabled = true;
        select.Required = true;
        select.Invalid = true;
        select.Outlined = true;
        select.Autofocus = true;
        select.Open = true;
        select.Distance = 8;

        Assert.Equal("opt1", select.Value);
        Assert.Equal("Select option", select.Label);
        Assert.Equal("Choose...", select.Placeholder);
        Assert.True(select.Disabled);
        Assert.True(select.Required);
        Assert.True(select.Invalid);
        Assert.True(select.Outlined);
        Assert.True(select.Autofocus);
        Assert.True(select.Open);
        Assert.Equal(8, select.Distance);
    }

    // Dropdown full property coverage
    [Fact]
    public void Dropdown_AllProperties_SetCorrectly()
    {
        var dd = new IgbDropdown();
        dd.Open = true;
        dd.KeepOpenOnSelect = true;

        Assert.True(dd.Open);
        Assert.True(dd.KeepOpenOnSelect);
    }

    // Accordion property
    [Fact]
    public void Accordion_SingleExpand_Property()
    {
        var acc = new IgbAccordion();
        acc.SingleExpand = true;
        Assert.True(acc.SingleExpand);
    }

    // Banner property
    [Fact]
    public void Banner_Open_Property()
    {
        var banner = new IgbBanner();
        banner.Open = true;
        Assert.True(banner.Open);
    }

    // Stepper + Step
    [Fact]
    public void Stepper_AllProperties_SetCorrectly()
    {
        var stepper = new IgbStepper();
        stepper.Orientation = StepperOrientation.Vertical;
        stepper.StepType = StepperStepType.Full;
        stepper.TitlePosition = StepperTitlePosition.End;
        stepper.ContentTop = true;

        Assert.Equal(StepperOrientation.Vertical, stepper.Orientation);
        Assert.Equal(StepperStepType.Full, stepper.StepType);
        Assert.Equal(StepperTitlePosition.End, stepper.TitlePosition);
        Assert.True(stepper.ContentTop);
    }

    [Fact]
    public void Step_AllProperties_SetCorrectly()
    {
        var step = new IgbStep();
        step.Disabled = true;

        Assert.True(step.Disabled);
    }

    // Radio and RadioGroup
    [Fact]
    public void Radio_AllProperties_SetCorrectly()
    {
        var radio = new IgbRadio();
        radio.Value = "opt-a";
        radio.Checked = true;
        radio.Disabled = true;
        radio.Required = true;
        radio.Invalid = true;
        radio.LabelPosition = ToggleLabelPosition.Before;

        Assert.Equal("opt-a", radio.Value);
        Assert.True(radio.Checked);
        Assert.True(radio.Disabled);
        Assert.True(radio.Required);
        Assert.True(radio.Invalid);
        Assert.Equal(ToggleLabelPosition.Before, radio.LabelPosition);
    }

    [Fact]
    public void RadioGroup_Alignment_Property()
    {
        var rg = new IgbRadioGroup();
        rg.Alignment = ContentOrientation.Horizontal;
        Assert.Equal(ContentOrientation.Horizontal, rg.Alignment);
    }

    // Tabs and Tab
    [Fact]
    public void Tabs_AllProperties_SetCorrectly()
    {
        var tabs = new IgbTabs();
        tabs.Alignment = TabsAlignment.Center;
        tabs.Activation = TabsActivation.Manual;

        Assert.Equal(TabsAlignment.Center, tabs.Alignment);
        Assert.Equal(TabsActivation.Manual, tabs.Activation);
    }

    [Fact]
    public void Tab_AllProperties_SetCorrectly()
    {
        var tab = new IgbTab();
        tab.Disabled = true;
        tab.Selected = true;

        Assert.True(tab.Disabled);
        Assert.True(tab.Selected);
    }
}
