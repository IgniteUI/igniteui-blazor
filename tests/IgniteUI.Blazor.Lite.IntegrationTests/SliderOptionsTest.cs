using System.Text.Json;
using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// Slider options that cross to the element as objects (<c>/slider-options</c>).
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class SliderOptionsTest : BlazorPageTest<Program>
    {
        // The thumb's accessible text is the value as the element formats it.
        private const string ThumbTexts =
            "s => [...(document.querySelector(s)?.shadowRoot?.querySelectorAll('[part~=\"thumb\"]') ?? [])].map(t => t.getAttribute('aria-valuetext'))";

        /// <summary>
        /// Without it, the options never reach the element, so the values show unformatted.
        /// </summary>
        [Test]
        public async Task ValueFormatOptions_FormatTheValues()
        {
            await Page.GotoAsync("http://localhost:5249/slider-options");
            try
            {
                await Page.WaitForFunctionAsync(
                    "() => { const texts = s => (" + ThumbTexts + ")(s).join('|');"
                    + " return texts('#percent') === '50%' && texts('#currency') === 'from €20.00|from €80.00'; }");
            }
            catch (TimeoutException)
            {
                var observed = await Page.EvaluateAsync<JsonElement>(
                    "() => Object.fromEntries(['#percent', '#currency'].map(s => [s, {"
                    + " thumbs: (" + ThumbTexts + ")(s),"
                    + " options: document.querySelector(s)?.valueFormatOptions ?? null }]))");
                Assert.Fail($"The thumbs do not show the formatted values. Observed: {observed.GetRawText()}");
            }
        }

        /// <summary>
        /// Without it, the tick labels would not rotate: the element takes the rotation in degrees, which the enum names stand for.
        /// </summary>
        [Test]
        public async Task TickLabelRotation_ReachesTheElementAsDegrees()
        {
            await Page.GotoAsync("http://localhost:5249/slider-options");
            try
            {
                await Page.WaitForFunctionAsync("() => document.querySelector('#percent')?.getAttribute('tick-label-rotation') === '90'");
            }
            catch (TimeoutException)
            {
                var observed = await Page.EvaluateAsync<JsonElement>(
                    "() => { const e = document.querySelector('#percent');"
                    + " return { property: e?.tickLabelRotation ?? null, attribute: e?.getAttribute('tick-label-rotation') ?? null }; }");
                Assert.Fail($"The tick labels are not rotated by 90 degrees. Observed: {observed.GetRawText()}");
            }
        }
    }
}
