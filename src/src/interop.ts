/**
 * The client side of the .NET interop, served at `_content/IgniteUI.Blazor/interop.js`.
 *
 * The components import this module through `IJSRuntime` (JS isolation) instead of calling functions on `window`,
 * so the library defines no globals and a second Ignite UI package can bring its own interop module alongside.
 * The functions live in the interop bundle (index.ts); this entry only gives them a fixed, importable file name, so
 * importing it reuses the instance the JS initializer already loaded.
 *
 * Internal to the Ignite UI packages; not an application API.
 */
export {
  checkReady,
  requestLoad,
  sendMessage,
  setResourceString,
  unmarshalledDataSourceClear,
  unmarshalledDataSourceCreate,
  unmarshalledDataSourceCreateDataIntents,
  unmarshalledDataSourceInsert,
  unmarshalledDataSourceRemove,
  unmarshalledDataSourceUpdate,
  waitForLoaded,
} from './index';
