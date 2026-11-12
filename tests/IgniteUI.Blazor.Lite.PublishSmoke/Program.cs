using System.Collections.ObjectModel;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Lite.PublishSmoke;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Type-based module preload under trimming (settings-carried + params, merged). IgbRating is never
// statically used by this app, so its preload only survives via [IgbModule<IgbRatingModule>].
var settings = new IgniteUIBlazorSettings()
    .WithModulesToLoad(new ReadOnlyCollection<Type>([typeof(IgbRatingModule)]));

builder.Services.AddIgniteUIBlazor(settings,
    typeof(IgbButtonModule),
    typeof(IgbAvatarModule),
    typeof(IgbComboModule),
    typeof(IgbDateRangePickerModule),
    typeof(IgbChatModule));

await builder.Build().RunAsync();
