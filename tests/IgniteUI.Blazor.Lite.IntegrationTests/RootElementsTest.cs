using System.Text.Json;
using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// Components the client renderer drives through descriptions, rendered by Blazor as the web
    /// component element itself (<c>/root-elements</c>).
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class RootElementsTest : BlazorPageTest<Program>
    {
        private const string Picker = "#flow igc-date-picker";

        /// <summary>
        /// Without it, the component sits inside a wrapper and a renderer container: its attributes
        /// land on the wrapper, an id exists twice, and the chat collapses to no width in normal flow.
        /// </summary>
        [Test]
        public async Task Component_IsTheRenderedElement()
        {
            await Page.GotoAsync("http://localhost:5249/root-elements");
            await Page.WaitForFunctionAsync("s => document.querySelector(s)?.label === 'Picker'", Picker);

            var observed = await Page.EvaluateAsync<JsonElement>(
                "s => { const p = document.querySelector(s), c = document.querySelector('#flow igc-chat');"
                + " return { parent: p.parentElement.id, containers: document.querySelectorAll('igc-component-renderer-container').length,"
                + " ids: document.querySelectorAll('#rooted-picker').length, className: p.className,"
                + " style: p.getAttribute('style'), title: p.title, chatWidth: c ? c.getBoundingClientRect().width : -1 }; }",
                Picker);

            var report = observed.GetRawText();
            Assert.Multiple(() =>
            {
                Assert.That(observed.GetProperty("parent").GetString(), Is.EqualTo("flow"), $"Observed: {report}");
                Assert.That(observed.GetProperty("containers").GetInt32(), Is.Zero, $"Observed: {report}");
                Assert.That(observed.GetProperty("ids").GetInt32(), Is.EqualTo(1), $"Observed: {report}");
                Assert.That(observed.GetProperty("className").GetString(), Is.EqualTo("rooted"), $"Observed: {report}");
                Assert.That(observed.GetProperty("style").GetString(), Is.EqualTo("margin: 2px"), $"Observed: {report}");
                Assert.That(observed.GetProperty("title").GetString(), Is.EqualTo("Picker"), $"Observed: {report}");
                Assert.That(observed.GetProperty("chatWidth").GetDouble(), Is.EqualTo(600), $"Observed: {report}");
            });
        }

        /// <summary>
        /// Without it, an attribute Blazor removes from the markup stays on the element, as the
        /// text "null", so a component rendered hidden="@false" stays hidden.
        /// </summary>
        [Test]
        public async Task RemovedAttribute_LeavesTheElement()
        {
            await Page.GotoAsync("http://localhost:5249/root-elements");
            await Page.WaitForFunctionAsync("s => document.querySelector(s)?.label === 'Picker'", Picker);

            await Page.ClickAsync("#show-picker");
            try
            {
                await Page.WaitForFunctionAsync(
                    "s => { const p = document.querySelector(s); return !p.hasAttribute('hidden') && p.getBoundingClientRect().height > 0; }",
                    Picker);
            }
            catch (TimeoutException)
            {
                var hidden = await Page.EvaluateAsync<string?>("s => document.querySelector(s).getAttribute('hidden')", Picker);
                Assert.Fail($"The picker is still hidden. Observed hidden attribute: {hidden ?? "(none)"}");
            }
        }
    }
}
