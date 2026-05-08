#if (IncludeWeatherSample)
using ApexCharts;
#endif
using IgniteBlazorApp.Client;
#if (IncludeWeatherSample)
using IgniteBlazorAppIGB_NS_SEGMENT.Services;
#endif
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

await builder.Build().RunAsync();
