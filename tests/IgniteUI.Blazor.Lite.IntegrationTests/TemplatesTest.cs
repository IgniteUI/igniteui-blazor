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

        [Test]
        public async Task ComboItemTemplate_ReplacedWhileShown_RendersTheNewTemplate()
        {
            await OpenComboAsync("live", itemCount: 4);
            await Expect(Template("#live .tmpl-live", "Live: Berlin")).ToBeVisibleAsync();

            // Replaces the template and refreshes the data, so the combo renders the new template
            // into the item hosts already on screen.
            await LiveActionAsync("swap-template");

            await Expect(Template("#live .tmpl-alt", "Alt: Berlin")).ToBeVisibleAsync();
            await Expect(Page.Locator("#live .tmpl-live")).ToHaveCountAsync(0);
            // The old template's content was released rather than left alongside the new one.
            await Expect(Page.Locator("#live-count")).ToHaveTextAsync("4");
        }

        [Test]
        public async Task ComboItemTemplate_RemovedItems_ReleaseTheirContent()
        {
            await OpenComboAsync("live", itemCount: 4);
            await Expect(Page.Locator("#live .tmpl-live")).ToHaveCountAsync(4);
            await Expect(Page.Locator("#live-count")).ToHaveTextAsync("4");

            await LiveActionAsync("drop-cities");

            await Expect(Page.Locator("#live .tmpl-live")).ToHaveCountAsync(2);
            await Expect(Page.Locator("#live-count")).ToHaveTextAsync("2");

            // Closing drops the rest of the items.
            await Page.EvaluateAsync("() => document.getElementById('live').hide()");
            await Expect(Page.Locator("#live-count")).ToHaveTextAsync("0");
        }

        [Test]
        public async Task ComboItemTemplate_ClosedBeforeContentArrives_ReleasesItsHosts()
        {
            await TrackHolderObserversAsync();
            await Page.GotoAsync($"http://localhost:5249/{page}");
            await Page.WaitForFunctionAsync("() => document.getElementById('live')?.data?.length === 4");

            // Closed as its item hosts are inserted, in the same task: their Add and Remove then reach
            // .NET in one batch, so their content is never rendered.
            await Page.EvaluateAsync("""
                () => new Promise(resolve => {
                    const combo = document.getElementById('live');
                    const watch = new MutationObserver(() => {
                        if (combo.shadowRoot.querySelector('igc-template-content')) {
                            watch.disconnect();
                            combo.hide();
                            resolve();
                        }
                    });
                    watch.observe(combo.shadowRoot, { childList: true, subtree: true });
                    combo.show();
                })
                """);

            // The hosts stopped waiting for their content, which would otherwise keep them alive.
            await Page.WaitForFunctionAsync($"() => {LiveHolderObservers} === 0", null, new() { Timeout = 5000 });
            await Expect(Page.Locator("#live-count")).ToHaveTextAsync("0");
        }

        private async Task OpenComboAsync(string id, int itemCount)
        {
            await Page.GotoAsync($"http://localhost:5249/{page}");
            // Holding its data means the runtime is live and the transfer completed.
            await Page.WaitForFunctionAsync($"() => document.getElementById('{id}')?.data?.length === {itemCount}");
            await Page.EvaluateAsync($"() => document.getElementById('{id}').show()");
        }

        // Counts the mutation observers watching #live's content holder: each template host watches
        // it until its content arrives, so any left over are leaked.
        private const string LiveHolderObservers =
            "[...window.__liveObservers].filter(o => o.__target === document.querySelector('#live > .ig-dynamic-content-holder')).length";

        private Task TrackHolderObserversAsync()
            => Page.AddInitScriptAsync("""
                (() => {
                    const live = new Set();
                    window.__liveObservers = live;
                    window.MutationObserver = class extends window.MutationObserver {
                        observe(target, options) {
                            this.__target = target;
                            live.add(this);
                            return super.observe(target, options);
                        }
                        disconnect() {
                            live.delete(this);
                            return super.disconnect();
                        }
                    };
                })();
                """);

        // A change event rather than a click: the combo closes on any click outside it.
        private Task LiveActionAsync(string action)
            => Page.EvaluateAsync("""
                action => {
                    const input = document.getElementById('live-action');
                    input.value = action;
                    input.dispatchEvent(new Event('change', { bubbles: true }));
                }
                """, action);

        // Playwright's CSS engine pierces the open shadow roots the templates render into.
        private ILocator Template(string selector, string text)
            => Page.Locator(selector, new() { HasTextString = text });
    }
}
