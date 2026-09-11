using System.Collections.Concurrent;
using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// Browser checks over the PublishSmoke app's trimmed publish output — the runtime gate for
    /// behavior the trim analyzer cannot verify: suppression justifications and retention-based
    /// mechanisms, which trim silently when they stop holding. Mirrors the checklist in
    /// <c>tests/IgniteUI.Blazor.Lite.PublishSmoke/README.md</c>.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    [Category("TrimmedPublish")]
    public class TrimmedPublishSmokeTest : BrowserTest
    {
        private static TrimmedPublishServer server = null!;

        // Filled from Playwright's dispatcher thread while the test thread may be reading.
        private IBrowserContext context = null!;
        private IPage page = null!;
        private ConcurrentQueue<string> pageErrors = null!;
        private ConcurrentQueue<string> failedRequests = null!;

        [OneTimeSetUp]
        public static async Task StartServer() => server = await TrimmedPublishServer.StartAsync();

        [OneTimeTearDown]
        public static async Task StopServer()
        {
            if (server is not null)
            {
                await server.DisposeAsync();
            }
        }

        [SetUp]
        public async Task LoadApp()
        {
            pageErrors = new();
            failedRequests = new();

            context = await NewContext(new BrowserNewContextOptions { BaseURL = server.BaseUrl });
            page = await context.NewPageAsync();
            page.Console += (_, message) =>
            {
                // Resource-load failures land in failedRequests with the URL (and the favicon
                // exclusion); the console duplicate carries neither.
                if (message.Type == "error" && !message.Text.StartsWith("Failed to load resource"))
                {
                    pageErrors.Enqueue(message.Text);
                }
            };
            page.PageError += (_, error) => pageErrors.Enqueue(error);
            page.Response += (_, response) =>
            {
                if (response.Status >= 400 && !response.Url.EndsWith("favicon.ico"))
                {
                    failedRequests.Enqueue($"{response.Status} {response.Url}");
                }
            };

            await page.GotoAsync("/", new() { WaitUntil = WaitUntilState.NetworkIdle });
            await page.WaitForSelectorAsync("#module-preload-result", new() { Timeout = 30000 });
            // Components ready: child components commit in later render batches than the App
            // markup above, and a non-empty bounding box on the last one means its web-component
            // module loaded and the element upgraded.
            await page.WaitForSelectorAsync("igc-date-range-picker", new() { State = WaitForSelectorState.Visible, Timeout = 30000 });
        }

        [TearDown]
        public async Task CloseContext()
        {
            if (context is not null)
            {
                await context.CloseAsync();
            }
        }

        /// <summary>
        /// IgbChat's component is never referenced by the app, so its Type-based preload survives
        /// trimming only through <c>[IgbModule&lt;IgbChatModule&gt;]</c> — the check that catches
        /// both attribute regressions and linker-behavior changes on SDK updates.
        /// </summary>
        [Test]
        public async Task ModulePreload_RegisterSurvivesTrimming()
        {
            var text = await page.Locator("#module-preload-result").TextContentAsync();

            Assert.That(text, Does.Contain("ChatModule preloaded: True"));
        }

        /// <summary>
        /// Enum parameters must serialize as web-component tokens — camelCase by convention,
        /// or via [WCEnumName] where present ("single-required"). Numbers mean the enum fields
        /// were trimmed; a camelCase "singleRequired" means the field attributes were.
        /// </summary>
        [Test]
        public async Task EnumParameters_SerializeAsWebComponentTokens()
        {
            var tokens = await page.EvaluateAsync<string[]>(
                @"[document.querySelector('igc-button').variant,
                   document.querySelector('igc-avatar').shape,
                   document.querySelector('igc-button-group').selection]");

            Assert.That(tokens, Is.EqualTo(new[] { "outlined", "circle", "single-required" }));
        }

        /// <summary>
        /// The combo's schema is built by reflecting over the app's item type, preserved via the
        /// documented DynamicallyAccessedMembers pattern from docs/TRIMMING.md.
        /// </summary>
        [Test]
        public async Task DataSource_ReflectsPreservedItemType()
        {
            // The data lands through interop after the element upgrades.
            await page.WaitForFunctionAsync("() => document.querySelector('igc-combo')?.data?.length === 3");
            var firstItemNames = await page.EvaluateAsync<string[]>(
                @"Object.keys(document.querySelector('igc-combo').data[0])");

            Assert.That(firstItemNames, Does.Contain("Id").And.Contain("Name"));
        }

        [Test]
        public async Task EventPayload_MaterializesDateRangeValue()
        {
            await page.EvaluateAsync(
                @"document.querySelector('igc-date-range-picker').dispatchEvent(
                      new CustomEvent('igcChange', { detail: { start: new Date(2026, 0, 5), end: new Date(2026, 0, 9) } }))");

            await page.WaitForFunctionAsync(
                @"() => !document.querySelector('#range-result').textContent.includes('no range selected')");
            var text = await page.Locator("#range-result").TextContentAsync();

            Assert.That(text, Does.Contain("2026"));
        }

        [Test]
        public void CleanLoad_NoErrorsOrFailedRequests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(pageErrors, Is.Empty, "Console/page errors during the trimmed app load.");
                Assert.That(failedRequests, Is.Empty, "Failed requests during the trimmed app load.");
            });
        }
    }
}
