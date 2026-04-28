# Data Display — Ignite UI for Blazor

This reference covers data-display components: List, Tree, Card, Chip, Avatar, Badge, Icon, Carousel, and Progress indicators.

---

## IgbList

A styled list with items, headers, and dividers.

### Basic usage

```razor
<IgbList>
    <IgbListHeader>Contacts</IgbListHeader>
    @foreach (var contact in contacts)
    {
        <IgbListItem>
            <IgbAvatar slot="start" Shape="AvatarShape.Circle" Initials="@contact.Initials" />
            <span slot="title">@contact.Name</span>
            <span slot="subtitle">@contact.Email</span>
            <IgbIcon slot="end" Collection="material" Name="chevron_right" />
        </IgbListItem>
    }
</IgbList>

@code {
    private List<Contact> contacts = new()
    {
        new("John Doe", "john@example.com", "JD"),
        new("Jane Smith", "jane@example.com", "JS"),
        new("Bob Wilson", "bob@example.com", "BW")
    };

    record Contact(string Name, string Email, string Initials);
}
```

### IgbListItem slots

| Slot | Description |
|---|---|
| `start` | Leading content (avatar, icon) |
| `title` | Primary text |
| `subtitle` | Secondary text |
| `end` | Trailing content (icon, badge) |
| (default) | Fallback content |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbListModule),
    typeof(IgbListItemModule),
    typeof(IgbListHeaderModule)
);
```

---

## IgbTree

A tree view with expandable/collapsible nodes and selection.

### Basic usage

```razor
<IgbTree Selection="TreeSelection.Multiple" SelectionChanged="OnSelectionChanged">
    <IgbTreeItem Label="Documents" Expanded="true">
        <IgbTreeItem Label="Work">
            <IgbTreeItem Label="Report.docx" />
            <IgbTreeItem Label="Budget.xlsx" />
        </IgbTreeItem>
        <IgbTreeItem Label="Personal">
            <IgbTreeItem Label="Photo.jpg" />
        </IgbTreeItem>
    </IgbTreeItem>
    <IgbTreeItem Label="Downloads">
        <IgbTreeItem Label="Setup.exe" />
    </IgbTreeItem>
</IgbTree>

@code {
    private void OnSelectionChanged(TreeSelectionEventArgs args)
    {
        var selectedItems = args.Detail.NewSelection;
    }
}
```

### IgbTree parameters

| Parameter | Type | Description |
|---|---|---|
| `Selection` | `TreeSelection` | `None`, `Multiple`, `Cascade` |

### IgbTreeItem parameters

| Parameter | Type | Description |
|---|---|---|
| `Label` | `string` | Display text |
| `Value` | `object` | Associated data value |
| `Expanded` | `bool` | Whether the node is expanded |
| `Selected` | `bool` | Whether the node is selected |
| `Disabled` | `bool` | Disables the node |
| `Active` | `bool` | Marks as active (keyboard focused) |

### IgbTreeItem slots

| Slot | Description |
|---|---|
| `label` | Custom label content |
| `indicator` | Custom expand/collapse indicator |
| (default) | Child `IgbTreeItem` nodes |

### Events

| Event | Type | Description |
|---|---|---|
| `SelectionChanged` | `EventCallback<TreeSelectionEventArgs>` | Fires when selection changes |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbTreeModule),
    typeof(IgbTreeItemModule)
);
```

---

## IgbCard

A material-design card with header, content, media, and action areas.

### Basic usage

```razor
<IgbCard>
    <IgbCardMedia Height="200px">
        <img src="images/landscape.jpg" alt="Landscape" />
    </IgbCardMedia>
    <IgbCardHeader>
        <IgbAvatar slot="thumbnail" Shape="AvatarShape.Circle" Src="images/avatar.png" />
        <h3 slot="title">Card Title</h3>
        <span slot="subtitle">Card subtitle</span>
    </IgbCardHeader>
    <IgbCardContent>
        <p>This is the card body content. Use it for descriptions, summaries, or any text.</p>
    </IgbCardContent>
    <IgbCardActions>
        <IgbButton slot="start" Variant="ButtonVariant.Flat">Read More</IgbButton>
        <IgbIconButton slot="end" Variant="IconButtonVariant.Flat" Collection="material" Name="favorite" />
        <IgbIconButton slot="end" Variant="IconButtonVariant.Flat" Collection="material" Name="share" />
    </IgbCardActions>
</IgbCard>
```

### Card sub-components

| Component | Description |
|---|---|
| `IgbCardHeader` | Header with `thumbnail`, `title`, `subtitle` slots |
| `IgbCardContent` | Body content area |
| `IgbCardMedia` | Image/media area. `Height` sets its height. |
| `IgbCardActions` | Action buttons with `start` and `end` slots |

### IgbCardHeader slots

| Slot | Description |
|---|---|
| `thumbnail` | Small avatar or icon |
| `title` | Card title |
| `subtitle` | Card subtitle |

### IgbCardActions slots

| Slot | Description |
|---|---|
| `start` | Leading action buttons |
| `end` | Trailing action icons |

### Card with outlined style

```razor
<IgbCard Outlined="true">
    <!-- content -->
</IgbCard>
```

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbCardModule),
    typeof(IgbCardHeaderModule),
    typeof(IgbCardContentModule),
    typeof(IgbCardMediaModule),
    typeof(IgbCardActionsModule)
);
```

---

## IgbChip

A compact element representing an attribute, action, or filter.

### Basic usage

```razor
<IgbChip>Default Chip</IgbChip>
<IgbChip Selectable="true" Selected="true">Selectable</IgbChip>
<IgbChip Removable="true" Remove="OnChipRemove">Removable</IgbChip>
<IgbChip Disabled="true">Disabled</IgbChip>
<IgbChip Variant="StyleVariant.Primary">Primary</IgbChip>

@code {
    private void OnChipRemove()
    {
        // Handle chip removal
    }
}
```

### Filter chips example

```razor
@foreach (var filter in filters)
{
    <IgbChip Selectable="true"
              Selected="@filter.IsActive"
              SelectedChanged="(bool val) => filter.IsActive = val">
        @filter.Name
    </IgbChip>
}

@code {
    private List<FilterItem> filters = new()
    {
        new("Active", true),
        new("Pending", false),
        new("Closed", false)
    };

    class FilterItem
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public FilterItem(string name, bool active) { Name = name; IsActive = active; }
    }
}
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Selectable` | `bool` | If true, chip can be toggled |
| `Selected` | `bool` | Selected state. Supports `@bind-Selected`. |
| `Removable` | `bool` | Shows a remove button |
| `Disabled` | `bool` | Disables the chip |
| `Variant` | `StyleVariant` | `Primary`, `Info`, `Success`, `Warning`, `Danger` |

### Events

| Event | Type | Description |
|---|---|---|
| `SelectedChanged` | `EventCallback<bool>` | Fires when selected state changes |
| `Remove` | `EventCallback` | Fires when the remove button is clicked |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbChipModule));
```

---

## IgbAvatar

Displays a user image, initials, or icon.

### Variants

```razor
<!-- Image avatar -->
<IgbAvatar Src="images/avatar.png" Shape="AvatarShape.Circle" Size="large" />

<!-- Initials avatar -->
<IgbAvatar Initials="JD" Shape="AvatarShape.Rounded" Size="medium" />

<!-- Icon avatar -->
<IgbAvatar Shape="AvatarShape.Square" Size="small">
    <IgbIcon Collection="material" Name="person" />
</IgbAvatar>
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Src` | `string` | Image source URL |
| `Initials` | `string` | Text initials (1-2 characters) |
| `Shape` | `AvatarShape` | `Circle`, `Rounded`, `Square` |
| `Size` | `string` | `"small"`, `"medium"`, `"large"` |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbAvatarModule));
```

---

## IgbBadge

A small status indicator, typically overlaid on other elements.

### Basic usage

```razor
<IgbBadge Variant="StyleVariant.Primary">5</IgbBadge>
<IgbBadge Variant="StyleVariant.Success">New</IgbBadge>
<IgbBadge Variant="StyleVariant.Danger">!</IgbBadge>
<IgbBadge Shape="BadgeShape.Rounded">99+</IgbBadge>
<IgbBadge Outlined="true">Info</IgbBadge>
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Variant` | `StyleVariant` | `Primary`, `Info`, `Success`, `Warning`, `Danger` |
| `Shape` | `BadgeShape` | `Rounded`, `Square` |
| `Outlined` | `bool` | Outlined style |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbBadgeModule));
```

---

## IgbIcon

Renders an icon from a registered collection or inline SVG.

### Material icon

```razor
<IgbIcon Collection="default" Name="home" />
<IgbIcon Collection="material" Name="favorite" />
```

### Custom SVG icon

```razor
<IgbIcon>
    <svg viewBox="0 0 24 24"><path d="M12 2L2 22h20L12 2z"/></svg>
</IgbIcon>
```

### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Name` | `string` | Icon name within the collection |
| `Collection` | `string` | Icon collection name (e.g., `"default"`, `"material"`) |
| `Mirrored` | `bool` | Mirrors the icon for RTL |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(typeof(IgbIconModule));
```

---

## IgbCarousel

A slideshow component for cycling through slides.

### Basic usage

```razor
<IgbCarousel>
    <IgbCarouselSlide>
        <img src="images/slide1.jpg" alt="Slide 1" />
    </IgbCarouselSlide>
    <IgbCarouselSlide>
        <img src="images/slide2.jpg" alt="Slide 2" />
    </IgbCarouselSlide>
    <IgbCarouselSlide>
        <img src="images/slide3.jpg" alt="Slide 3" />
    </IgbCarouselSlide>
    <IgbCarouselIndicator slot="indicator" />
</IgbCarousel>
```

### IgbCarousel parameters

| Parameter | Type | Description |
|---|---|---|
| `DisableLoop` | `bool` | Disables looping at the end |
| `DisablePauseOnInteraction` | `bool` | Keeps auto-playing even on interaction |
| `HideNavigation` | `bool` | Hides previous/next buttons |
| `Interval` | `int` | Auto-play interval in milliseconds |
| `Vertical` | `bool` | Vertical slide direction |
| `IndicatorsOrientation` | `CarouselIndicatorsOrientation` | `Start`, `End` |
| `AnimationDirection` | `CarouselAnimationDirection` | `Normal`, `Reverse` |

### IgbCarouselSlide parameters

| Parameter | Type | Description |
|---|---|---|
| `Active` | `bool` | Whether this slide is currently visible |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbCarouselModule),
    typeof(IgbCarouselSlideModule),
    typeof(IgbCarouselIndicatorModule)
);
```

---

## IgbLinearProgress & IgbCircularProgress

Progress indicators for loading states and task completion.

### IgbLinearProgress

```razor
<!-- Determinate -->
<IgbLinearProgress Value="65" Max="100" />

<!-- Indeterminate -->
<IgbLinearProgress Indeterminate="true" />

<!-- With label -->
<IgbLinearProgress Value="@progress" Max="100" LabelAlign="LinearProgressLabelAlign.End">
    @($"{progress}%")
</IgbLinearProgress>

@code {
    private double progress = 65;
}
```

#### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `double` | Current progress value |
| `Max` | `double` | Maximum value (default 100) |
| `Indeterminate` | `bool` | Shows indeterminate animation |
| `LabelAlign` | `LinearProgressLabelAlign` | `Top`, `TopStart`, `TopEnd`, `Bottom`, `BottomStart`, `BottomEnd`, `Start`, `End` |
| `Striped` | `bool` | Striped bar style |

### IgbCircularProgress

```razor
<!-- Determinate -->
<IgbCircularProgress Value="75" Max="100" />

<!-- Indeterminate -->
<IgbCircularProgress Indeterminate="true" />
```

#### Key parameters

| Parameter | Type | Description |
|---|---|---|
| `Value` | `double` | Current progress value |
| `Max` | `double` | Maximum value |
| `Indeterminate` | `bool` | Shows indeterminate animation |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbLinearProgressModule),
    typeof(IgbCircularProgressModule)
);
```

---

## Key Rules

1. **Slots** — Many data-display components use named slots (`slot="start"`, `slot="title"`, etc.) for content projection. These map to web component slots.
2. **Collections in loops** — When iterating with `@foreach` to create dynamic items, always use `@key` on the loop item when items may be added/removed.
3. **Avatar shape** — Use the `AvatarShape` enum: `Circle`, `Rounded`, `Square`.
4. **Badge content** — Badge child content is used as the badge text. For icon-only badges, place an `IgbIcon` as child content.
5. **Tree selection modes** — `None` disables selection, `Multiple` allows independent selection of each node, `Cascade` selects/deselects child nodes when a parent is toggled.
6. **Progress indeterminate** — Set `Indeterminate="true"` when the total is unknown. When `Indeterminate` is `true`, the `Value` is ignored.

---

## See Also

- [setup.md](setup.md) — Project setup and registration
- [form-controls.md](form-controls.md) — Form inputs used within cards and lists
- [layout.md](layout.md) — Tabs, Accordion, NavDrawer for organizing data-display content
- [directives.md](directives.md) — Buttons and icons used in card actions and list items
- [feedback.md](feedback.md) — Dialog and Toast for actions on data-display items
