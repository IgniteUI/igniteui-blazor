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
<div class="card-row">
    <IgbCard>
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
</div>
```

```css
/* MyView.razor.css */
.card-row ::deep igc-card {
    width: 350px;
}
```

`IgbCard` has no width of its own — in a block container it stretches to the container's width, in a flex row it shrinks to its content — so give it one from CSS, not a `style` attribute. In a `.razor.css` file, use `::deep` under an element the view renders itself: `igc-card` carries no CSS-isolation attribute, so a plain `.my-card { … }` rule there never matches, even with `class="my-card"` on the `IgbCard`. In `app.css`, a `class` plus a plain rule works. `IgbCard` has no `Width` parameter: `Width="350px"` compiles, but only renders a `width` attribute on `igc-card`, which does not size it.

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

<IgbChip Selectable="true" Removable="true" Variant="StyleVariant.Info" @bind-Selected="BlazorSelected" Remove="OnChipRemoved">
    <IgbIcon slot="start" IconName="star" Collection="material" />
    Blazor
</IgbChip>
<IgbChip Outlined="true" Variant="StyleVariant.Primary">Outlined</IgbChip>

@code {
    bool BlazorSelected { get; set; }
    void OnChipRemoved(IgbVoidEventArgs e) { }   // the chip does not remove itself: drop it from your data here
}
```

`Remove` is an `EventCallback<IgbVoidEventArgs>`. `Outlined="true"` gives a transparent chip with a 1px border; with a `Variant` the border takes the variant color and the text stays the theme's text color (Indigo gives an outlined chip without `Variant` a light fill and a grey border). Selecting a chip adds a check icon. In Bootstrap and Material that is all it changes. In Fluent and Indigo, selecting a chip **without** a `Variant` also fills a non-outlined chip with the primary color and gives an outlined one a primary border and darker text. A chip with a `Variant` keeps its variant colors when selected, and an outlined chip is transparent when selected.

Without a `Variant`, restyle an outlined chip with `--ig-chip-outlined-background`, `--ig-chip-outlined-text-color` and `--ig-chip-border-color`; once it is selected these are replaced by `--ig-chip-selected-outlined-background`, `--ig-chip-selected-outlined-text-color` and `--ig-chip-selected-border-color`. With a `Variant`, the variant rules win over every background, text and border token (including `--ig-chip-background` / `--ig-chip-text-color`); size and shape tokens such as `--ig-chip-size` and `--ig-chip-border-radius` still apply. To recolor a variant chip, style the part, e.g. `.chips ::deep igc-chip[selected]::part(base) { background: …; border-color: …; color: …; }` in the view's `.razor.css`, where `.chips` is an element the view renders. Tokens alone do not outline a chip without `Outlined` (it gets no border). `IgbChip` also has `Selected` / `SelectedChanged` (`@bind-Selected`) and a `Select` event (`IgbComponentBoolValueChangedEventArgs`; `Detail` is the new state).

`AvatarShape`: `Circle | Rounded | Square` — use `Shape`, there is no `RoundShape`. `StyleVariant` (shared by badge and chip): `Primary | Info | Success | Warning | Danger`; a chip without `Variant` has no color variant.

`IgbBadge` is a non-interactive visual notification, such as a status or a count. It also works as a label, such as a category, a role ("Maintainer") or a tag. It defaults to `role="status"`. For a label, set a `role` that fits the context, for example `role="listitem"` in a `role="list"` row. `IgbChip` is interactive: a filter, a contextual action, or an item the user selects or removes. It renders a `<button>`, so it is focusable and has hover and pressed states.

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

Renders the `Value` string (URL, text, any payload) as a scannable SVG. `Version` (1-40) is chosen automatically when unset; `ErrorLevel` (`Low | Medium | Quartile | High`) is `null` by default, which leaves the web component's default level, and should be raised explicitly when a larger logo needs more error correction. `Size` is the rendered pixel size and `Margin` the quiet zone in modules; `LogoSrc`, `LogoSize` and `LogoMargin` place a centered logo. Color it with the `--ig-qr-code-background`, `--ig-qr-code-dark-color`, `--ig-qr-code-corner-square-color` and `--ig-qr-code-corner-dot-color` custom properties.

`ToImageAsync(IgbQrCodeExportOptions)` exports the code as an image file (`Format`: `Svg | Png | Jpeg | Webp`, default `Png`; `Scale` multiplies `Size`; `FileName` defaults to `qr-code`). The exported file stays on the client and is always handed to the user through the browser save dialog — there is no way to read the file bytes back into .NET. The export fails when the component has no `Value`.

```razor
<IgbQrCode @ref="Qr" Value="https://www.infragistics.com" Size="192" />
<IgbButton @onclick='() => Qr!.ToImageAsync(new IgbQrCodeExportOptions { Format = QrCodeExportFormat.Png, Scale = 2 })'>Download</IgbButton>

@code { IgbQrCode? Qr; }
```

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
