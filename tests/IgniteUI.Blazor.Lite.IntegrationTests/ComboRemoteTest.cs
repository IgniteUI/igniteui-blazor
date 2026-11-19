using System.Text.Json;
using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// The combo features of igniteui-webcomponents 7.5 against the WebAssembly-rendered
    /// TestBed page (<c>/combo-remote</c>): remote search and paging, the close trigger,
    /// and the confirm selection mode. The page records what its .NET handlers receive,
    /// so each test checks the event details that crossed back from the client.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class ComboRemoteTest : BlazorPageTest<Program>
    {
        private const string Remote = "document.getElementById('remote')";
        private const string Confirm = "document.getElementById('confirm')";

        [Test]
        public async Task RemoteSearch_SetsDataFromTheServer()
        {
            await OpenAsync();

            await Page.EvaluateAsync($"() => {Remote}.show()");
            await Page.Locator("#remote").Locator("[part~='search-input'] input").FillAsync("12");

            // "12" matches 12, 112, 120-129, 212, ... : the page's search, not the combo's filter.
            await Page.WaitForFunctionAsync($"() => {Remote}.data.length > 0 && {Remote}.data.every(p => p.Name.includes('12'))");
            var log = await LogAsync();
            Assert.That(log.GetProperty("searchTexts").EnumerateArray().Select(t => t.GetString()), Does.Contain("12"));
            var total = await Page.EvaluateAsync<int>($"() => {Remote}.totalItemCount");
            Assert.That(total, Is.EqualTo(Enumerable.Range(1, 500).Count(i => i.ToString("000").Contains("12"))));
        }

        [Test]
        public async Task DataRequest_AppendsTheRequestedPage()
        {
            await OpenAsync();

            // 20 of 500 items are loaded, so opening the list is already near the end of the data.
            await Page.EvaluateAsync($"() => {Remote}.show()");
            await Page.WaitForFunctionAsync($"() => {Remote}.data.length > 20");

            var log = await LogAsync();
            var first = log.GetProperty("requests")[0].GetString();
            Assert.That(first, Does.StartWith("20:"), "the request should start after the loaded items");
            var name = await Page.EvaluateAsync<string>($"() => {Remote}.data[20].Name");
            Assert.That(name, Is.EqualTo("Product 021"));

            // The appended items render, not only reach the data array.
            await Page.EvaluateAsync($"() => {Remote}.shadowRoot.querySelector('igc-virtual-scroll').scrollToIndex(30)");
            await Page.WaitForFunctionAsync(
                $"() => [...{Remote}.shadowRoot.querySelectorAll('igc-combo-item')].some(i => i.textContent.includes('Product 031'))");
        }

        [Test]
        public async Task Closing_ReportsTheTrigger()
        {
            await OpenAsync();

            await Page.EvaluateAsync($"() => {Remote}.show()");
            await Page.Locator("#remote").Locator("[part~='search-input'] input").FocusAsync();
            await Page.Keyboard.PressAsync("Escape");

            await WaitForLogAsync("closed", 1);
            var log = await LogAsync();
            Assert.Multiple(() =>
            {
                Assert.That(log.GetProperty("closing")[0].GetString(), Is.EqualTo("Escape"));
                Assert.That(log.GetProperty("closed")[0].GetString(), Is.EqualTo("Escape"));
            });
        }

        [Test]
        public async Task ConfirmMode_AppliesThePendingSelectionWithOneChange()
        {
            await OpenAsync();

            await Page.EvaluateAsync($"() => {Confirm}.show()");
            await Page.WaitForFunctionAsync($"() => {Confirm}.shadowRoot.querySelector('igc-combo-item') !== null");
            // Playwright's own click fails its actionability check on the shadow-DOM items.
            await Page.EvaluateAsync($"() => {Confirm}.shadowRoot.querySelectorAll('igc-combo-item')[1].click()");

            await WaitForLogAsync("pending", 1);
            Assert.That(await Page.EvaluateAsync<int>($"() => {Confirm}.value.length"), Is.EqualTo(0),
                "a pending change should not change the value");

            await Page.EvaluateAsync($"() => {Confirm}.shadowRoot.querySelector('[part~=\"apply-button\"]').click()");

            await WaitForLogAsync("changes", 1);
            var log = await LogAsync();
            Assert.Multiple(() =>
            {
                Assert.That(log.GetProperty("pending")[0].GetString(), Is.EqualTo("Selection:2"));
                Assert.That(log.GetProperty("changes")[0].GetString(), Is.EqualTo("Commit:2"));
            });
        }

        [Test]
        public async Task NewData_KeepsTheSelectedItemsOfTheOldData()
        {
            const string NoKey = "document.getElementById('nokey')";
            await OpenAsync();
            await Page.WaitForFunctionAsync($"() => {NoKey}?.data?.length === 5");

            await Page.EvaluateAsync($"() => {NoKey}.show()");
            await Page.WaitForFunctionAsync($"() => {NoKey}.shadowRoot.querySelector('igc-combo-item') !== null");
            await Page.EvaluateAsync($"() => {NoKey}.shadowRoot.querySelectorAll('igc-combo-item')[0].click()");
            await WaitForLogAsync("noKeyChanges", 1);

            // As after a remote search: new data that does not hold the selected item.
            await Page.EvaluateAsync("() => window.clientPageRef.invokeMethodAsync('ReplaceNoKeyData')");
            await Page.WaitForFunctionAsync($"() => {NoKey}.data[0].Name === 'Product 006'");
            await Page.WaitForFunctionAsync($"() => {NoKey}.shadowRoot.querySelector('igc-combo-item')?.textContent.includes('006')");
            await Page.EvaluateAsync($"() => {NoKey}.shadowRoot.querySelectorAll('igc-combo-item')[0].click()");
            await WaitForLogAsync("noKeyChanges", 2);

            var log = await LogAsync();
            Assert.Multiple(() =>
            {
                Assert.That(log.GetProperty("noKeyChanges")[0].GetString(), Is.EqualTo("Product 001"));
                Assert.That(log.GetProperty("noKeyChanges")[1].GetString(), Is.EqualTo("Product 001,Product 006"));
            });

            // .NET reads the selection back, and names the old item in a method call.
            var selection = await Page.EvaluateAsync<string>("() => window.clientPageRef.invokeMethodAsync('GetNoKeySelection')");
            Assert.That(selection, Is.EqualTo("Product 001,Product 006"));

            await Page.EvaluateAsync("() => window.clientPageRef.invokeMethodAsync('DeselectFirstNoKeyItem')");
            await Page.WaitForFunctionAsync($"() => {NoKey}.value.length === 1 && {NoKey}.value[0].Name === 'Product 006'");
        }

        private async Task OpenAsync()
        {
            await Page.GotoAsync("http://localhost:5249/combo-remote");
            await Page.WaitForFunctionAsync("() => !!window.clientPageRef");
            await Page.WaitForFunctionAsync($"() => {Remote}?.data?.length === 20 && {Confirm}?.data?.length === 5");
        }

        private Task<JsonElement> LogAsync()
            => Page.EvaluateAsync<JsonElement>("() => window.clientPageRef.invokeMethodAsync('GetLog')");

        private Task WaitForLogAsync(string list, int count)
            => Page.WaitForFunctionAsync(
                $"() => window.clientPageRef.invokeMethodAsync('GetLog').then(l => l.{list}.length >= {count})");
    }
}
