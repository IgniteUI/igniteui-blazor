using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;
using Microsoft.Playwright;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// Razor <c>RenderFragment</c> templates that the web components invoke directly with a plain
    /// context (Combo item / group header, Chat renderers), on both render modes: Server
    /// (<c>/templates</c>, data over the JSON channel) and WebAssembly (<c>/templates-wasm</c>,
    /// the in-process channel). The hidden content holder renders every template first, so a
    /// visible match is one the client moved into the component's own UI.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    [TestFixture("templates")]
    [TestFixture("templates-wasm")]
    public class TemplatesTest(string page) : BlazorPageTest<Program>
    {
        [Test]
        public async Task ComboItemTemplate_RendersDataItems()
        {
            await OpenComboAsync("cities", itemCount: 4);

            // The country proves the template got the whole data item, not just its display text.
            await Expect(Template("#cities .tmpl-city", "City: Berlin (Germany)")).ToBeVisibleAsync();
            await Expect(Template("#cities .tmpl-city", "City: Plovdiv (Bulgaria)")).ToBeVisibleAsync();
        }

        [Test]
        public async Task ComboGroupHeaderTemplate_RendersGroupKey()
        {
            await OpenComboAsync("cities", itemCount: 4);

            await Expect(Template("#cities .tmpl-group", "Country: Germany")).ToBeVisibleAsync();
            await Expect(Template("#cities .tmpl-group", "Country: Bulgaria")).ToBeVisibleAsync();
        }

        [Test]
        public async Task ComboItemTemplate_RendersValueTypeItems()
        {
            await OpenComboAsync("numbers", itemCount: 3);

            await Expect(Template("#numbers .tmpl-number", "Number: 2")).ToBeVisibleAsync();
        }

        [Test]
        public async Task ChatMessageContentTemplate_RendersMessage()
        {
            await Page.GotoAsync($"http://localhost:5249/{page}");

            await Expect(Template("#chat .tmpl-message", "Message: Hello from Blazor")).ToBeVisibleAsync();
        }

        private async Task OpenComboAsync(string id, int itemCount)
        {
            await Page.GotoAsync($"http://localhost:5249/{page}");
            // Holding its data means the runtime is live and the transfer completed.
            await Page.WaitForFunctionAsync($"() => document.getElementById('{id}')?.data?.length === {itemCount}");
            await Page.EvaluateAsync($"() => document.getElementById('{id}').show()");
        }

        // Playwright's CSS engine pierces the open shadow roots the templates render into.
        private ILocator Template(string selector, string text)
            => Page.Locator(selector, new() { HasTextString = text });
    }
}
