# Data Display & Action Components

Module for every component below is `Igb<Name>Module`. `IgbButton` and `IgbIconButton` are separate modules; `IgbList`, `IgbListItem` and `IgbListHeader` each have their own; card sub-parts each have their own.

## Button & Button Group

```razor
<IgbButton Variant="ButtonVariant.Contained" @onclick="Save">Save</IgbButton>
<IgbButton Variant="ButtonVariant.Outlined" Disabled="true">Cancel</IgbButton>
<IgbButton Variant="ButtonVariant.Flat" Href="/docs" Target="ButtonBaseTarget.Blank">Docs</IgbButton>

<IgbButtonGroup Selection="ButtonGroupSelection.Single" Alignment="ContentOrientation.Horizontal">
    <IgbToggleButton Value="left">Left</IgbToggleButton>
    <IgbToggleButton Value="center" Selected="true">Center</IgbToggleButton>
    <IgbToggleButton Value="right">Right</IgbToggleButton>
</IgbButtonGroup>
```

`ButtonVariant`: `Contained | Outlined | Flat | Fab`. Setting `Href` renders an anchor and enables `Target`, `Rel`, `Download`. Click handling uses Blazor's `@onclick`. `IgbButtonGroup` raises `Select` / `Deselect` (`IgbComponentValueChangedEventArgs`).

## Icon & Icon Button

```razor
<IgbIcon @ref="MyIcon" IconName="home" Collection="material" />
<IgbIconButton IconName="menu" Collection="material" Variant="IconButtonVariant.Flat" Mirrored="false" />

@code {
    IgbIcon MyIcon { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && MyIcon is not null)
        {
            await MyIcon.EnsureReady();
            await MyIcon.RegisterIconFromTextAsync("home", "<svg>...</svg>", "material");
            // or: await MyIcon.RegisterIconAsync("home", "/icons/home.svg", "material");
        }
    }
}
```

The parameter is **`IconName`**, not `Name` (`Name` is the framework element identity on every component). Icons are keyed by name + collection and must be registered before they render — always in `OnAfterRenderAsync(firstRender)` after `await EnsureReady()`. Reuse one collection name across the app.

## Card

```razor
<IgbCard style="width: 350px;">
    <IgbCardMedia><img src="photo.jpg" alt="Card image" /></IgbCardMedia>
    <IgbCardHeader>
        <h3 slot="title">Jane Doe</h3>
        <span slot="subtitle">Photographer</span>
    </IgbCardHeader>
    <IgbCardContent><p>A short description.</p></IgbCardContent>
    <IgbCardActions>
        <IgbButton slot="start" Variant="ButtonVariant.Flat">Like</IgbButton>
        <IgbButton slot="end" Variant="ButtonVariant.Contained">Buy Now</IgbButton>
    </IgbCardActions>
</IgbCard>
```

`IgbCard` has no default width — always give it one.

## List

```razor
<IgbList>
    <IgbListHeader>Contacts</IgbListHeader>
    @foreach (var contact in Contacts)
    {
        <IgbListItem>
            <IgbAvatar slot="start" Shape="AvatarShape.Circle" Initials="@contact.Initials" />
            <span slot="title">@contact.Name</span>
            <span slot="subtitle">@contact.Phone</span>
            <IgbIconButton slot="end" IconName="delete" Collection="material" />
        </IgbListItem>
    }
</IgbList>
```

`IgbListItem` slots: `start`, `title`, `subtitle`, `end`.

## Avatar, Badge, Chip

```razor
<IgbAvatar Src="avatar.png" Alt="User photo" Shape="AvatarShape.Circle" />
<IgbAvatar Shape="AvatarShape.Circle" Initials="AB" />
<IgbAvatar Shape="AvatarShape.Square"><IgbIcon IconName="person" Collection="material" /></IgbAvatar>

<IgbBadge Variant="StyleVariant.Primary">5</IgbBadge>
<IgbBadge Variant="StyleVariant.Danger" Shape="BadgeShape.Square" Dot="true" />

<IgbChip Selectable="true" Removable="true" Variant="StyleVariant.Info" Remove="OnChipRemoved">
    <IgbIcon slot="start" IconName="star" Collection="material" />
    Blazor
</IgbChip>
<IgbChip Outlined="true" Variant="StyleVariant.Primary">Outlined</IgbChip>

@code {
    void OnChipRemoved(IgbComponentBoolValueChangedEventArgs e) { }
}
```

`AvatarShape`: `Circle | Rounded | Square` — use `Shape`, there is no `RoundShape`. `StyleVariant` (shared by badge and chip): `Primary | Info | Success | Warning | Danger`. `IgbChip` also has `Outlined`, `Selected` / `SelectedChanged` and a `Select` event.

## Progress

`IgbCircularProgress` and `IgbLinearProgress` share `Value`, `Max`, `Variant` (`StyleVariant`), `Indeterminate`, `HideLabel`, `LabelFormat`, `AnimationDuration`.

```razor
<IgbCircularProgress Value="65" Max="100"><span slot="default">65%</span></IgbCircularProgress>
<IgbCircularProgress Indeterminate="true" />
<IgbLinearProgress Value="42" Max="100" Striped="true" LabelAlign="LinearProgressLabelAlign.End" />
```

Use these for progress, not for data visualization — a static colored ring with a centered value is a donut chart.

## Dropdown

```razor
<IgbDropdown Placement="PopoverPlacement.BottomStart" SameWidth="false">
    <IgbButton slot="target">Options</IgbButton>
    <IgbDropdownHeader>Actions</IgbDropdownHeader>
    <IgbDropdownItem Value="edit">Edit</IgbDropdownItem>
    <IgbDropdownItem Value="delete">Delete</IgbDropdownItem>
    <IgbDropdownItem Value="archive" Disabled="true">Archive</IgbDropdownItem>
</IgbDropdown>
```

The trigger goes in `slot="target"`. `IgbDropdownGroup` groups items; `Placement`, `Flip`, `Distance`, `ScrollStrategy`, `SameWidth` control positioning; `ShowAsync()` / `HideAsync()` / `ToggleAsync()` drive it from code. For a form field that selects a value, use `IgbSelect` instead.

## Tooltip

```razor
<IgbButton id="hover-button">Hover me</IgbButton>
<IgbTooltip Anchor="hover-button" Placement="PopoverPlacement.Top" ShowDelay="200" WithArrow="true">
    This is a tooltip
</IgbTooltip>
```

`Anchor` is the **id string** of the target element. `Message` sets plain text without child content; `ShowTriggers` / `HideTriggers` override the default hover/focus behavior; `Sticky` keeps it open until dismissed.

## QR Code

```razor
<IgbQrCode Value="https://www.infragistics.com" Size="192" />

<IgbQrCode Value="https://www.infragistics.com" Size="256"
           ErrorLevel="QrErrorCorrectionLevel.Quartile"
           DotStyle="QrDotStyle.Rounded" SquareStyle="QrCornerSquareStyle.Rounded"
           LogoSrc="images/logo.svg" LogoSize="0.6" />
```

Renders the `Value` string (URL, text, any payload) as a scannable SVG. `Version` (1-40) and `ErrorLevel` (`Low | Medium | Quartile | High`) are chosen automatically when unset, and a logo raises the error level on its own unless one is set explicitly. `Size` is the rendered pixel size and `Margin` the quiet zone in modules; `LogoSrc`, `LogoSize` and `LogoMargin` place a centered logo. Color it with the `--ig-qr-code-background`, `--ig-qr-code-dark-color`, `--ig-qr-code-corner-square-color` and `--ig-qr-code-corner-dot-color` custom properties.

## Ripple, Highlight, Chat

```razor
<div style="position: relative; padding: 16px;">Click me<IgbRipple /></div>

<IgbHighlight SearchText="@query" CaseSensitive="false">@text</IgbHighlight>

<IgbChat Options="ChatOptions" DraftMessage="Draft" MessageCreated="OnMessageCreated" />
```

- `IgbRipple` needs a `position: relative` parent; customize with the `--color` custom property.
- `IgbHighlight` marks occurrences of `SearchText` inside its content.
- `IgbChat` is a full chat surface configured through `IgbChatOptions` (messages, suggestions, renderers) with `MessageCreated`, `MessageReact`, `AttachmentClick`, `TypingChange`, `InputFocus`, `InputBlur`. Read its doc before building against it — the options object is large.

## Carousel

```razor
<IgbCarousel Interval="5000" DisableLoop="false" HideIndicators="false" Vertical="false">
    <IgbCarouselSlide Active="true"><img src="slide-1.jpg" alt="Slide 1" /></IgbCarouselSlide>
    <IgbCarouselSlide><img src="slide-2.jpg" alt="Slide 2" /></IgbCarouselSlide>
</IgbCarousel>
```

`Interval` enables autoplay, `AnimationType` picks the transition, `HideNavigation` / `HideIndicators` strip the chrome, `IndicatorsOrientation` and `MaximumIndicatorsCount` tune the indicator strip.
