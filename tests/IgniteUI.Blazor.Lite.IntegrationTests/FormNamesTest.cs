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
    }
}
