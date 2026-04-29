# Layout — Ignite UI for Blazor

This reference covers layout and navigation container components: Tabs, Stepper, Accordion, Expansion Panel, and Navigation Drawer.

---

## IgbTabs

A tabbed interface with tab headers and associated content panels.

### Basic usage

```razor
<IgbTabs @bind-SelectedIndex="activeTab">
    <IgbTab Panel="panel-profile">Profile</IgbTab>
    <IgbTab Panel="panel-settings">Settings</IgbTab>
    <IgbTab Panel="panel-billing" Disabled="true">Billing</IgbTab>

    <IgbTabPanel Id="panel-profile">
        <p>Profile content goes here.</p>
    </IgbTabPanel>
    <IgbTabPanel Id="panel-settings">
        <p>Settings content goes here.</p>
    </IgbTabPanel>
    <IgbTabPanel Id="panel-billing">
        <p>Billing content goes here.</p>
    </IgbTabPanel>
</IgbTabs>

@code {
    private int activeTab = 0;
}
```

### IgbTabs parameters

| Parameter | Type | Description |
|---|---|---|
| `SelectedIndex` | `int` | Zero-based index of the active tab. Supports `@bind-SelectedIndex`. |
| `Activation` | `TabsActivation` | `Auto` (default) — activates tab on focus; `Manual` — requires click/Enter |
| `Alignment` | `TabsAlignment` | `Start`, `End`, `Center`, `Justify` |

### IgbTab parameters

| Parameter | Type | Description |
|---|---|---|
| `Panel` | `string` | The `Id` of the associated `IgbTabPanel` |
| `Disabled` | `bool` | Disables the tab |
| `Selected` | `bool` | Selects the tab |

### IgbTabPanel parameters

| Parameter | Type | Description |
|---|---|---|
| `Id` | `string` | Unique identifier for the panel |

### Events

| Event | Type | Description |
|---|---|---|
| `SelectedIndexChanged` | `EventCallback<int>` | Fires when the active tab changes |

### Closable tabs

```razor
<IgbTabs>
    @foreach (var tab in tabs)
    {
        <IgbTab Panel="@tab.PanelId" Closable="true" Close="() => RemoveTab(tab)">
            @tab.Title
        </IgbTab>
    }
    @foreach (var tab in tabs)
    {
        <IgbTabPanel Id="@tab.PanelId">
            <p>@tab.Content</p>
        </IgbTabPanel>
    }
</IgbTabs>

@code {
    private List<TabData> tabs = new()
    {
        new("tab-1", "Tab 1", "Content 1"),
        new("tab-2", "Tab 2", "Content 2"),
        new("tab-3", "Tab 3", "Content 3")
    };

    private void RemoveTab(TabData tab) => tabs.Remove(tab);

    record TabData(string PanelId, string Title, string Content);
}
```

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbTabsModule),
    typeof(IgbTabModule)
);
```

---

## IgbStepper

A wizard-like stepper with horizontal or vertical orientation, linear/non-linear navigation, and programmatic step control.

### Basic usage

```razor
<IgbStepper Orientation="StepperOrientation.Horizontal"
             Linear="true"
             ActiveStepChanged="OnStepChanged">
    <IgbStep>
        <span slot="title">Personal Info</span>
        <div>
            <IgbInput @bind-Value="name" Label="Name" />
            <IgbInput @bind-Value="email" Type="InputType.Email" Label="Email" />
        </div>
    </IgbStep>
    <IgbStep>
        <span slot="title">Address</span>
        <div>
            <IgbInput @bind-Value="address" Label="Address" />
        </div>
    </IgbStep>
    <IgbStep>
        <span slot="title">Confirmation</span>
        <div>
            <p>Name: @name</p>
            <p>Email: @email</p>
            <p>Address: @address</p>
        </div>
    </IgbStep>
</IgbStepper>

@code {
    private string name = "";
    private string email = "";
    private string address = "";

    private void OnStepChanged(ActiveStepChangedEventArgs args)
    {
        // Handle step change
    }
}
```

### IgbStepper parameters

| Parameter | Type | Description |
|---|---|---|
| `Orientation` | `StepperOrientation` | `Horizontal` (default) or `Vertical` |
| `Linear` | `bool` | When `true`, steps must be completed sequentially |
| `StepType` | `StepperStepType` | `Full`, `Indicator`, `Title` |
| `TitlePosition` | `StepperTitlePosition` | `Bottom`, `Top`, `Start`, `End` |
| `VerticalAnimation` | `StepperVerticalAnimation` | `Grow` (default) or `None` |

### IgbStep parameters

| Parameter | Type | Description |
|---|---|---|
| `Active` | `bool` | Whether the step is currently active |
| `Disabled` | `bool` | Disables the step |
| `Optional` | `bool` | Marks the step as optional |
| `Complete` | `bool` | Marks the step as completed |
| `Invalid` | `bool` | Marks the step as having validation errors |

### IgbStep slots

| Slot | Description |
|---|---|
| `title` | Step title text |
| `subtitle` | Step subtitle text |
| `indicator` | Custom step indicator (icon/number) |
| (default) | Step body content |

### Events

| Event | Type | Description |
|---|---|---|
| `ActiveStepChanged` | `EventCallback<ActiveStepChangedEventArgs>` | Fires when the active step changes |
| `ActiveStepChanging` | `EventCallback<ActiveStepChangingEventArgs>` | Fires before a step change; can be cancelled |

### Programmatic navigation

```razor
<IgbStepper @ref="stepper">
    <!-- steps... -->
</IgbStepper>

<IgbButton Click="() => stepper.PrevAsync()">Previous</IgbButton>
<IgbButton Click="() => stepper.NextAsync()">Next</IgbButton>
<IgbButton Click="() => stepper.ResetAsync()">Reset</IgbButton>

@code {
    private IgbStepper stepper = default!;
}
```

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbStepperModule),
    typeof(IgbStepModule)
);
```

---

## IgbAccordion & IgbExpansionPanel

### IgbAccordion

A container that manages multiple expansion panels, optionally allowing only one to be open.

```razor
<IgbAccordion SingleExpand="true">
    <IgbExpansionPanel>
        <span slot="title">Section 1</span>
        <p>Content for section 1.</p>
    </IgbExpansionPanel>
    <IgbExpansionPanel>
        <span slot="title">Section 2</span>
        <p>Content for section 2.</p>
    </IgbExpansionPanel>
    <IgbExpansionPanel>
        <span slot="title">Section 3</span>
        <p>Content for section 3.</p>
    </IgbExpansionPanel>
</IgbAccordion>
```

### IgbAccordion parameters

| Parameter | Type | Description |
|---|---|---|
| `SingleExpand` | `bool` | When `true`, only one panel can be expanded at a time |

### IgbExpansionPanel (standalone)

Can also be used outside of an accordion:

```razor
<IgbExpansionPanel @bind-Open="isOpen">
    <span slot="title">Click to expand</span>
    <span slot="subtitle">Additional info</span>
    <p>Panel content goes here.</p>
</IgbExpansionPanel>

@code {
    private bool isOpen = false;
}
```

### IgbExpansionPanel parameters

| Parameter | Type | Description |
|---|---|---|
| `Open` | `bool` | Whether the panel is expanded. Supports `@bind-Open`. |
| `IndicatorPosition` | `ExpansionPanelIndicatorPosition` | `Start` or `End` (default) |
| `Disabled` | `bool` | Disables the panel |

### IgbExpansionPanel slots

| Slot | Description |
|---|---|
| `title` | Header title text |
| `subtitle` | Header subtitle text |
| `indicator` | Custom expand/collapse indicator |
| (default) | Body content |

### Events

| Event | Type | Description |
|---|---|---|
| `OpenChanged` | `EventCallback<bool>` | Fires when the panel open state changes |

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbAccordionModule),
    typeof(IgbExpansionPanelModule)
);
```

---

## IgbNavDrawer

A side navigation drawer providing quick access between views.

### Basic usage

```razor
<IgbNavDrawer Open="true" Position="NavDrawerPosition.Start">
    <IgbNavDrawerHeaderItem>
        My Application
    </IgbNavDrawerHeaderItem>
    <IgbNavDrawerItem @onclick="() => Navigate('/')">
        <IgbIcon slot="icon" Collection="material" Name="home" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>
    <IgbNavDrawerItem @onclick="() => Navigate('/dashboard')" Active="true">
        <IgbIcon slot="icon" Collection="material" Name="dashboard" />
        <span slot="content">Dashboard</span>
    </IgbNavDrawerItem>
    <IgbNavDrawerItem @onclick="() => Navigate('/settings')">
        <IgbIcon slot="icon" Collection="material" Name="settings" />
        <span slot="content">Settings</span>
    </IgbNavDrawerItem>
</IgbNavDrawer>

@code {
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    private void Navigate(string url) => NavManager.NavigateTo(url);
}
```

### IgbNavDrawer parameters

| Parameter | Type | Description |
|---|---|---|
| `Open` | `bool` | Whether the drawer is open. Supports `@bind-Open`. |
| `Position` | `NavDrawerPosition` | `Start` (default), `End`, `Top`, `Bottom`, `Relative` |

### IgbNavDrawerItem parameters

| Parameter | Type | Description |
|---|---|---|
| `Active` | `bool` | Highlights the item as active |
| `Disabled` | `bool` | Disables the item |

### IgbNavDrawerItem slots

| Slot | Description |
|---|---|
| `icon` | Icon area (typically `IgbIcon`) |
| `content` | Text content of the item |

### IgbNavDrawerHeaderItem

A non-interactive header element at the top of the drawer.

### Mini variant

```razor
<IgbNavDrawer Open="true" Position="NavDrawerPosition.Relative">
    <IgbNavDrawerItem>
        <IgbIcon slot="icon" Collection="material" Name="home" />
        <span slot="content">Home</span>
    </IgbNavDrawerItem>
</IgbNavDrawer>
```

The mini variant collapses to show only icons when in `Relative` position and the drawer has a smaller width applied via CSS.

### Registration

```csharp
builder.Services.AddIgniteUIBlazor(
    typeof(IgbNavDrawerModule),
    typeof(IgbNavDrawerItemModule),
    typeof(IgbNavDrawerHeaderItemModule),
    typeof(IgbIconModule)
);
```

---

## Key Rules

1. **Slots** — Blazor Ignite UI components use `slot` attributes on child elements to project content into named slots (e.g., `<span slot="title">`). This maps to web component slots.
2. **Tab linking** — `IgbTab` and `IgbTabPanel` are linked via the `Panel` and `Id` properties. They must match.
3. **Stepper linear mode** — when `Linear="true"`, the user cannot skip ahead. Mark completed steps via `Complete="true"` or use the `NextAsync()` method.
4. **NavDrawer routing** — use `@onclick` with `NavigationManager.NavigateTo()` for Blazor routing; set `Active="true"` on the item matching the current URL.
5. **Accordion vs standalone** — `IgbExpansionPanel` works both inside and outside of `IgbAccordion`. Inside an accordion with `SingleExpand="true"`, only one panel opens at a time.

---

## See Also

- [setup.md](setup.md) — Project setup and registration
- [form-controls.md](form-controls.md) — Form inputs for stepper step content
- [data-display.md](data-display.md) — Lists and trees for drawer content
- [feedback.md](feedback.md) — Dialog, Snackbar, Toast
- [directives.md](directives.md) — Buttons and icons used in layouts
- [layout-manager.md](layout-manager.md) — TileManager and DockManager
