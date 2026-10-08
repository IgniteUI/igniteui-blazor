using System.Text.Json;
using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// The form-field names of form-associated components, read from the live elements and from
    /// the form data the browser would submit (<c>/form-names</c>).
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class FormNamesTest : BlazorPageTest<Program>
    {
        private static readonly string[] WrappedTags =
        [
            "igc-color-picker", "igc-combo", "igc-date-picker",
            "igc-date-range-picker", "igc-date-time-input", "igc-mask-input",
        ];

        // Tag -> selector of that component in the named form. "#named > igc-radio" is the standalone
        // radio, a direct child of the form; the grouped one sits inside igc-radio-group.
        private static readonly Dictionary<string, string> NamedSelectors = new[]
        {
            "igc-checkbox", "igc-switch", "igc-input", "igc-radio", "igc-radio-group",
            "igc-rating", "igc-select", "igc-slider", "igc-textarea",
            "igc-color-picker", "igc-combo", "igc-date-picker", "igc-date-range-picker",
            "igc-date-time-input", "igc-mask-input",
        }.ToDictionary(tag => tag, tag => tag == "igc-radio" ? "#named > igc-radio" : "#named " + tag);

        /// <summary>
        /// Without it, components rendered through the client renderer submit under the
        /// renderer's own id for them, "mainControl", so every such field in a form collides.
        /// </summary>
        [Test]
        public async Task UnnamedComponents_SubmitNoName()
        {
            await Page.GotoAsync("http://localhost:5249/form-names");
            await Page.WaitForFunctionAsync(
                "tags => tags.every(t => document.querySelector('#unnamed ' + t)?.label)", WrappedTags);

            var observed = await Page.EvaluateAsync<JsonElement>(
                "tags => ({ names: Object.fromEntries(tags.map(t => [t, document.querySelector('#unnamed ' + t).getAttribute('name')])),"
                + " submitted: [...new FormData(document.getElementById('unnamed')).keys()] })",
                WrappedTags);

            var report = observed.GetRawText();
            Assert.Multiple(() =>
            {
                foreach (var tag in WrappedTags)
                {
                    Assert.That(observed.GetProperty("names").GetProperty(tag).ValueKind, Is.EqualTo(JsonValueKind.Null),
                        $"{tag} should have no name attribute. Observed: {report}");
                }
                Assert.That(observed.GetProperty("submitted").EnumerateArray().Select(k => k.GetString()),
                    Does.Not.Contain("mainControl"), $"Observed: {report}");
            });
        }

        /// <summary>
        /// Without it, a component's Name would not reach its element, so the field would not be
        /// submitted under that name.
        /// </summary>
        [Test]
        public async Task Name_ReachesTheFormField()
        {
            await Page.GotoAsync("http://localhost:5249/form-names");
            try
            {
                await Page.WaitForFunctionAsync(
                    "selectors => Object.entries(selectors).every(([tag, s]) => document.querySelector(s)?.getAttribute('name') === 'f-' + tag)",
                    NamedSelectors);
            }
            catch (TimeoutException)
            {
                var names = await Page.EvaluateAsync<JsonElement>(
                    "selectors => Object.fromEntries(Object.entries(selectors).map(([tag, s]) => [tag, document.querySelector(s)?.getAttribute('name') ?? null]))",
                    NamedSelectors);
                Assert.Fail($"Not every element carries the name \"f-\" + its tag. Observed: {names.GetRawText()}");
            }

            var observed = await Page.EvaluateAsync<JsonElement>(
                "() => ({ submitted: [...new FormData(document.getElementById('named')).keys()],"
                + " groupedRadio: document.querySelector('#named igc-radio-group igc-radio').getAttribute('name') })");

            var report = observed.GetRawText();
            var submitted = observed.GetProperty("submitted").EnumerateArray().Select(k => k.GetString()).ToList();
            Assert.Multiple(() =>
            {
                Assert.That(submitted, Does.Contain("f-igc-input"), $"Observed: {report}");
                Assert.That(submitted, Does.Contain("f-igc-date-time-input"), $"Observed: {report}");
                Assert.That(submitted, Does.Not.Contain("mainControl"), $"Observed: {report}");
                Assert.That(observed.GetProperty("groupedRadio").GetString(), Is.EqualTo("f-igc-radio-group"), $"Observed: {report}");
            });
        }
    }
}
