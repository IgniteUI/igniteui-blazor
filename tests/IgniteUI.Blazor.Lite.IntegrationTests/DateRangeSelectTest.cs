using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// <c>SelectAsync</c> on a Date Range Picker with a range created in user code (<c>/date-range-select</c>).
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class DateRangeSelectTest : BlazorPageTest<Program>
    {
        [Test]
        public async Task Select_AppliesTheRangeToThePicker()
        {
            await Page.GotoAsync("http://localhost:5249/date-range-select");
            await Page.WaitForFunctionAsync("() => window.appLoaded === true");

            await Page.ClickAsync("#select");

            await Page.WaitForFunctionAsync("() => document.querySelector('igc-date-range-picker').value?.start != null");
            var range = await Page.EvaluateAsync<string[]>(
                """
                () => {
                    const format = date => [date.getFullYear(), date.getMonth() + 1, date.getDate()].join('-');
                    const value = document.querySelector('igc-date-range-picker').value;
                    return [format(value.start), format(value.end)];
                }
                """);

            Assert.That(range, Is.EqualTo(new[] { "2026-3-1", "2026-3-10" }));
        }
    }
}
