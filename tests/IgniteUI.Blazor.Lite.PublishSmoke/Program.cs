using System.Collections.ObjectModel;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Lite.PublishSmoke;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Type-based module preload under trimming (settings-carried + params, merged). IgbChat is never
// statically used by this app, so its preload only survives via [IgbModule<IgbChatModule>].
var settings = new IgniteUIBlazorSettings()
    .WithModulesToLoad(new ReadOnlyCollection<Type>([typeof(IgbChatModule)]));

builder.Services.AddIgniteUIBlazor(settings,
    typeof(IgbButtonModule),
    typeof(IgbAvatarModule),
    typeof(IgbComboModule),
    typeof(IgbDateRangePickerModule));

await builder.Build().RunAsync();
