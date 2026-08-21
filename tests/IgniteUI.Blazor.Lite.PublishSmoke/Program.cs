using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Lite.PublishSmoke;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Exercise the module registration paths under trimming — see TRIMMING.md:
// - Add<T>: reflection-free static-abstract dispatch;
// - Add(typeof(...)): the IgbModuleRef implicit conversion, whose annotated parameter lets the
//   trimmer preserve Register per call site. IgbChat is never statically used by this app, so
//   its preload works ONLY through that tracing (verified in isolation: True with IgbModuleRef,
//   False with the former raw Type[] overload).
builder.Services.AddIgniteUIBlazor(new IgniteUIBlazorSettings(), m => m
    .Add<IgbButtonModule>()
    .Add<IgbAvatarModule>()
    .Add<IgbComboModule>()
    .Add<IgbDateRangePickerModule>()
    .Add(typeof(IgbChatModule)));

await builder.Build().RunAsync();
