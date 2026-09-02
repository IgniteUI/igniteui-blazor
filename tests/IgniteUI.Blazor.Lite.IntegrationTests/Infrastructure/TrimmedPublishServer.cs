using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Serves the PublishSmoke app's trimmed publish output as static files on a dynamic port
    /// for the TrimmedPublish browser checks, publishing it first when the output is missing
    /// (CI publishes ahead of the test run, so it hits the fast path).
    /// </summary>
    internal sealed class TrimmedPublishServer : IAsyncDisposable
    {
        private const string Tfm = "net10.0";

        private readonly WebApplication app;

        public string BaseUrl { get; }

        private TrimmedPublishServer(WebApplication app, string baseUrl)
        {
            this.app = app;
            BaseUrl = baseUrl;
        }

        public static async Task<TrimmedPublishServer> StartAsync()
        {
            var files = new PhysicalFileProvider(EnsurePublished());

            var builder = WebApplication.CreateBuilder();
            builder.Logging.ClearProviders();
            builder.WebHost.UseUrls("http://127.0.0.1:0");

            var app = builder.Build();
            app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = files });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = files,
                // Blazor publish output contains fingerprinted asset names with no mapped extension.
                ServeUnknownFileTypes = true,
            });

            await app.StartAsync().ConfigureAwait(false);
            return new TrimmedPublishServer(app, app.Urls.First());
        }

        public async ValueTask DisposeAsync()
        {
            await app.StopAsync().ConfigureAwait(false);
            await app.DisposeAsync().ConfigureAwait(false);
        }

        private static string EnsurePublished()
        {
            var repoRoot = FindRepoRoot();
            var wwwroot = Path.Combine(repoRoot, "tests", "IgniteUI.Blazor.Lite.PublishSmoke",
                "bin", "Release", Tfm, "publish", "wwwroot");

            if (!File.Exists(Path.Combine(wwwroot, "index.html")))
            {
                var publish = new ProcessStartInfo("dotnet",
                    $"publish tests/IgniteUI.Blazor.Lite.PublishSmoke -c Release -f {Tfm} --nologo")
                {
                    WorkingDirectory = repoRoot,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };
                using var process = Process.Start(publish)!;
                var stderr = process.StandardError.ReadToEndAsync();
                var output = process.StandardOutput.ReadToEnd() + stderr.GetAwaiter().GetResult();
                process.WaitForExit();
                if (process.ExitCode != 0 || !File.Exists(Path.Combine(wwwroot, "index.html")))
                {
                    throw new InvalidOperationException($"Publishing the smoke app failed ({process.ExitCode}):\n{output}");
                }
            }

            return wwwroot;
        }

        private static string FindRepoRoot()
        {
            for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir != null; dir = dir.Parent)
            {
                if (dir.EnumerateFiles("*.slnx").Any())
                {
                    return dir.FullName;
                }
            }

            throw new InvalidOperationException("Could not locate the repository root (no .slnx found above " + AppContext.BaseDirectory + ").");
        }
    }
}
