#if (IncludeWeatherSample)
using ApexCharts;
#endif
using IgniteBlazorApp.Components;
#if (IncludeWeatherSample)
using IgniteBlazorAppIGB_NS_SEGMENT.Services;
#endif
using IgniteUI.Blazor.Controls;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
#if (HostingIsServer)
    .AddInteractiveServerComponents();
#endif
#if (Hosting == "Wasm")
    .AddInteractiveWebAssemblyComponents();
#endif
#if (HostingIsAuto)
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
#endif
builder.Services.AddIgniteUIBlazor(
    typeof(IgbNavbarModule),
    typeof(IgbButton),
    typeof(IgbIconButtonModule),
    typeof(IgbNavDrawerModule)
);
#if (IncludeWeatherSample)
builder.Services.AddApexCharts();
builder.Services.AddSingleton<WeatherForecastService>();
#endif

var app = builder.Build();

#if (HostingHasClient)
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
#else
if (!app.Environment.IsDevelopment())
#endif
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
#if (HostingIsServer)
    .AddInteractiveServerRenderMode();
#endif
#if (Hosting == "Wasm")
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IgniteBlazorApp.Client._Imports).Assembly);
#endif
#if (HostingIsAuto)
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(IgniteBlazorApp.Client._Imports).Assembly);
#endif

app.Run();
