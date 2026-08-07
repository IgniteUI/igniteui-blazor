using Microsoft.AspNetCore.Hosting;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure
{
    public class BlazorPageTest<TProgram> : BrowserTest
        where TProgram : class
    {
        private static int hostDescribed;

        private BlazorApplicationFactory<TProgram>? host;

        public IBrowserContext Context { get; private set; } = null!;

        public IPage Page { get; private set; } = null!;

        public BlazorApplicationFactory<TProgram> Host
        {
            get
            {
                host ??= CreateHostFactory() ?? new BlazorApplicationFactory<TProgram>(ConfigureWebHost);
                return host;
            }
        }

        public virtual BlazorApplicationFactory<TProgram> CreateHostFactory()
            => new BlazorApplicationFactory<TProgram>(ConfigureWebHost);

        public virtual BrowserNewContextOptions ContextOptions() => null!;

        protected virtual void ConfigureWebHost(IWebHostBuilder builder) { }

        [SetUp]
        public async Task PageSetup()
        {
            var options = ContextOptions() ?? new BrowserNewContextOptions();
            options.BaseURL = Host.ServerAddress;
            options.IgnoreHTTPSErrors = true;

            // All the hosts are configured the same, so this is worth reporting once per run.
            if (Interlocked.Exchange(ref hostDescribed, 1) == 0)
            {
                TestContext.Out.WriteLine($"[host] {Host.Describe()}");
            }

            Context = await NewContext(options).ConfigureAwait(false);
            Page = await Context.NewPageAsync().ConfigureAwait(false);
        }

        [TearDown]
        public async Task HostTearDown()
        {
            // The client side of the SignalR connection has to be gone before the server
            // shuts down. BrowserTest closes the contexts too, but only after this TearDown
            // and only when the test passed; CloseAsync is idempotent, so that stays a no-op.
            if (Context != null)
            {
                await Context.CloseAsync().ConfigureAwait(false);
            }

            if (host is { } currentHost)
            {
                host = null;
                await currentHost.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}
