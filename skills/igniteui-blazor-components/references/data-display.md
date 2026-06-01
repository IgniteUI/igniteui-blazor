# Data Display Components

> **Part of the [`igniteui-blazor-components`](../SKILL.md) skill hub.**
> For project setup and module registration - see [`setup.md`](./setup.md).

## Contents

- [Button & Button Group](#button--button-group)
- [Icon & Icon Button](#icon--icon-button)
- [Card](#card)
- [Carousel](#carousel)
- [List](#list)
- [Avatar](#avatar)
- [Badge](#badge)
- [Chip](#chip)
- [Circular Progress](#circular-progress)
- [Linear Progress](#linear-progress)
- [Dropdown](#dropdown)
- [Tooltip](#tooltip)
- [Ripple](#ripple)
- [Key Rules](#key-rules)

---

## Overview
This reference gives high-level guidance on data display and action components, their key features, and common API members. For detailed documentation, call `get_doc` from `igniteui-cli`; use `search_api` and `get_api_reference` for Blazor API details.

## Button & Button Group

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbButtonModule), typeof(IgbButtonGroupModule));
```

```razor
<!-- Button variants -->
<IgbButton Variant="@ButtonVariant.Contained" @onclick="Save">Save</IgbButton>
<IgbButton Variant="@ButtonVariant.Outlined" @onclick="Cancel">Cancel</IgbButton>
<IgbButton Variant="@ButtonVariant.Flat">Flat</IgbButton>

<!-- Button Group (toggle buttons) -->
<IgbButtonGroup Selection="@ButtonGroupSelection.Single">
    <IgbToggleButton Value="left">Left</IgbToggleButton>
    <IgbToggleButton Value="center" Selected="true">Center</IgbToggleButton>
    <IgbToggleButton Value="right">Right</IgbToggleButton>
</IgbButtonGroup>
```

---

## Icon & Icon Button

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbIconModule), typeof(IgbIconButtonModule));
```

```razor
<IgbIcon @ref="MyIcon" IconName="home" Collection="material" />
<IgbIconButton @ref="MenuBtn" IconName="menu" Collection="material" Variant="@IconButtonVariant.Flat" />

@code {
    IgbIcon MyIcon { get; set; }
    IgbIconButton MenuBtn { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && MyIcon != null)
        {
            await MyIcon.EnsureReady();
            string svg = "<svg>...</svg>";
            await MyIcon.RegisterIconFromTextAsync("home", svg, "material");
        }
    }
}
```

> **AGENT INSTRUCTION - Icon Registration:** Icons are registered by name+collection. Registration must happen in `OnAfterRenderAsync(bool firstRender)` after calling `EnsureReady()`. Re-use the same collection name across the app for consistency.

---

## Card

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCardModule));
```

```razor
<IgbCard style="width: 350px;">
    <IgbCardMedia>
        <img src="photo.jpg" alt="Card image" />
    </IgbCardMedia>
    <IgbCardHeader>
        <h3 slot="title">Jane Doe</h3>
        <span slot="subtitle">Photographer</span>
    </IgbCardHeader>
    <IgbCardContent>
        <p>A short description of Jane Doe.</p>
    </IgbCardContent>
    <IgbCardActions>
        <IgbButton slot="start" Variant="@ButtonVariant.Flat">Like</IgbButton>
        <IgbButton slot="start" Variant="@ButtonVariant.Flat">Share</IgbButton>
        <IgbButton slot="end" Variant="@ButtonVariant.Contained">Buy Now</IgbButton>
    </IgbCardActions>
</IgbCard>
```

---

## Carousel

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCarouselModule));
```

```razor
<IgbCarousel>
    <IgbCarouselSlide Active="true">
        <img src="slide-1.jpg" alt="Slide 1" />
    </IgbCarouselSlide>
    <IgbCarouselSlide>
        <img src="slide-2.jpg" alt="Slide 2" />
    </IgbCarouselSlide>
</IgbCarousel>
```

Use Carousel for image/content slides, banners, onboarding panels, or media galleries. Check the `carousel` MCP doc for current slide APIs, navigation controls, indicators, autoplay, animation, and CSS parts.

---

## List

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbListModule), typeof(IgbListItemModule), typeof(IgbListHeaderModule));
```

```razor
<IgbList>
    <IgbListHeader>Contacts</IgbListHeader>
    @foreach (var contact in Contacts)
    {
        <IgbListItem>
            <IgbAvatar slot="start" Shape="@AvatarShape.Circle">@contact.Initials</IgbAvatar>
            <span slot="title">@contact.Name</span>
            <span slot="subtitle">@contact.Phone</span>
            <IgbIconButton slot="end" IconName="delete" Collection="material" />
        </IgbListItem>
    }
</IgbList>
```

---

## Avatar

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbAvatarModule));
```

```razor
<!-- Image avatar -->
<IgbAvatar Src="avatar.png" Alt="User photo" Shape="@AvatarShape.Circle" />

<!-- Initials avatar -->
<IgbAvatar Shape="@AvatarShape.Circle">AB</IgbAvatar>

<!-- Icon avatar -->
<IgbAvatar Shape="@AvatarShape.Square">
    <IgbIcon IconName="person" Collection="material" />
</IgbAvatar>
```

---

## Badge

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbBadgeModule));
```

```razor
<IgbBadge Variant="@StyleVariant.Primary">5</IgbBadge>
<IgbBadge Variant="@StyleVariant.Danger" Shape="@BadgeShape.Square" />
```

---

## Chip

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbChipModule));
```

```razor
<IgbChip Selectable="true" Removable="true" Remove="OnChipRemoved">
    <IgbIcon slot="start" IconName="star" Collection="material" />
    Blazor
</IgbChip>

@code {
    void OnChipRemoved(IgbComponentBoolValueChangedEventArgs e) { /* handle removal */ }
}
```

---

## Circular Progress

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbCircularProgressModule));
```

```razor
<!-- Determinate -->
<IgbCircularProgress Value="65" Max="100">
    <span slot="default">65%</span>
</IgbCircularProgress>

<!-- Indeterminate (spinner) -->
<IgbCircularProgress Indeterminate="true" />
```

---

## Linear Progress

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbLinearProgressModule));
```

```razor
<IgbLinearProgress Value="42" Max="100" Striped="true" />
<IgbLinearProgress Indeterminate="true" />
```

---

## Dropdown

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbDropdownModule));
```

```razor
<div>
    <IgbDropdown>
        <IgbButton slot="target">Options</IgbButton>
        <IgbDropdownHeader>Actions</IgbDropdownHeader>
        <IgbDropdownItem Value="edit">Edit</IgbDropdownItem>
        <IgbDropdownItem Value="delete">Delete</IgbDropdownItem>
        <IgbDropdownItem Value="disabled" Disabled="true">Archive</IgbDropdownItem>
    </IgbDropdown>
</div>

```

---

## Tooltip

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbTooltipModule));
```

```razor
<IgbButton id="hover-button">Hover me</IgbButton>
<IgbTooltip Anchor="hover-button" Placement="@PopoverPlacement.Top">
    This is a tooltip
</IgbTooltip>
```

---

## Ripple

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbRippleModule));
```

Attach to any element with the `igcRipple` attribute or nest `IgbRipple` inside a container:

```razor
<div style="position: relative; padding: 16px; cursor: pointer;">
    Click me
    <IgbRipple />
</div>
```

CSS part: `base`. Customize color via `--color` CSS custom property.

---

## Key Rules

1. **Register each module explicitly.** `IgbButtonModule` and `IgbIconButtonModule` are separate modules.
2. **Icons must be registered before they display.** Use `EnsureReady()` + `RegisterIconFromTextAsync()` in `OnAfterRenderAsync(bool firstRender)`.
3. **`IgbCard` does not set a default width.** Always set a width via inline style or CSS class.
4. **Prefer the Dropdown `target` slot for the trigger.** For an external trigger, follow the current `dropdown` MCP doc and call `Show()`, `Hide()`, or `Toggle()` on the dropdown reference.
5. **`IgbTooltip.Anchor` uses the target element ID string in the documented Blazor pattern.** Give the target an `id` and pass that same value to `Anchor`.
