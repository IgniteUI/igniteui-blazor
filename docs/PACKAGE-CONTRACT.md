# Contract with the full IgniteUI.Blazor package

The licensed/Trial `IgniteUI.Blazor` package depends on this package: it gets the components listed in the README
from here, and builds its charts, grids and Dock Manager on the same base classes and next to the same client code.
Everything below is what it relies on. None of it is application API. Changing any item is a breaking change for
that package, so coordinate it with the package's maintainers.

## Assemblies

- `InternalsVisibleTo("IgniteUI.Blazor")`, pinned to the Infragistics public key (`eng/IG.publickey.hex`). The full
  package overrides and calls the base classes' `internal` / `private protected` infrastructure members.
- Public, but `[EditorBrowsable(Never)]`, because public types of the full package derive from them:
  `BaseCollection<T>`, `JsonSerializable`, `SerializationContext`, `SerializationFilter`, `IDataIntentAttribute`.
- `IContentChildHost`: components that implement it cascade themselves to their child content, and elements of
  this package that the full package's components hold as content children (`IgbFormatSpecifier` and derived)
  attach to the nearest host.
- `BaseRendererControl.InteropModulePath`: each package's components use their own client interop module. Client
  module load requests are tracked per interop module (`ModuleLoader.Load(runtime, name, interopModulePath)`).
- `RendererSerializer.Context` / `ShouldWrite`: used to serialize the full package's drawing types (points, rects,
  brushes) as extension methods.
- `MarshalByValueFactory.AddProvider`: the full package registers its by-value types (chart and grid value objects).
- `FindByName` is `internal virtual` on both base classes, so the full package's generated types override it the same way.

## Static web assets (`_content/IgniteUI.Blazor/`)

Both packages serve from the same base path, so the file routes must not overlap.

| File | Contract |
|---|---|
| `interop.js` | The client interop module the components import (JS isolation). Exports: `sendMessage`, `checkReady`, `waitForLoaded`, `requestLoad`, `setResourceString`, `unmarshalledDataSource{Create,CreateDataIntents,Insert,Update,Remove,Clear}`. The full package ships its own module under another path. |
| `api.js` | Public script API (`registerScript`, `removeScript`, `html`). Its `getRegisteredScript` export is internal but stable: the full package's runtime reads the same registry through it, so scripts registered once serve both packages. |
| `lit-html.js` | The lit-html instance both runtimes render templates with. |
| `themes/**` | The web components themes. The full package ships only `themes/grid/**`. |
| `IgniteUI.Blazor.Lite.lib.module.js` | This package's JS initializer. The full package has its own (`IgniteUI.Blazor[.Trial].lib.module.js`). |

This package defines no interop functions or state on `window`. Custom elements shared by both client runtimes (`igc-portal-*`,
`igc-template-container`, `igc-template-content`) are defined only if not defined yet, whichever runtime loads first. The legacy script globals
(`igRegisterScript` / `igRemoveScript` / `igTemplating`) and `app.bundle.js`, where still present, are being
removed. The full package takes them over for its own users.

`tests/js/static-web-assets.test.mjs` pins the file names and the `interop.js` / `api.js` exports.
