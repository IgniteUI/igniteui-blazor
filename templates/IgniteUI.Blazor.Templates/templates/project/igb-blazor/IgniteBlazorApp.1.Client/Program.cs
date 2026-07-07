#if (IncludeWeatherSample)
using ApexCharts;
#endif
using IgniteBlazorApp._1.Client;
#if (IncludeWeatherSample)
using IgniteBlazorApp._1IGB_NS_SEGMENT.Services;
#endif
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddIgniteUIBlazor();
#if (IncludeWeatherSample)
builder.Services.AddApexCharts();
builder.Services.AddSingleton<WeatherForecastService>();
#endif

await builder.Build().RunAsync();
