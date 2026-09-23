using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// <c>*Script</c> parameters whose functions are registered only after the component rendered
    /// (<c>/late-scripts</c>): one client template and one client event handler on a Combo.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class LateScriptRegistrationTest : BlazorPageTest<Program>
    {
        [Test]
        public async Task ScriptsRegisteredAfterRender_AreApplied()
        {
            // Both refs must reach the client and miss before anything is registered, otherwise this
            // exercises the ordinary early path. Armed before navigation so no message can slip past.
            var templateMissed = WaitForUnregisteredAsync("lateItemTemplate");
            var handlerMissed = WaitForUnregisteredAsync("lateOpening");

            await Page.GotoAsync("http://localhost:5249/late-scripts");
            await Page.WaitForFunctionAsync("() => window.appLoaded === true");
            await Page.EvaluateAsync("setSelector('igc-combo')");
            await Task.WhenAll(templateMissed, handlerMissed);
            Assert.That(await Page.EvaluateAsync<bool>("checkClientTemplate('itemTemplate')"), Is.False, "template applied before its script was registered");

            await Page.EvaluateAsync("generateClientTmpl('lateItemTemplate')");
            await Page.EvaluateAsync("generateClientHandler('lateOpening')");

            await Page.WaitForFunctionAsync("() => checkClientTemplate('itemTemplate')");
            await Page.RunAndWaitForConsoleMessageAsync(
                () => Page.EvaluateAsync("triggerEvent('igcOpening')"),
                new() { Predicate = message => message.Text == "[TestBed] script 'lateOpening' handled igcOpening" });
        }

        private Task WaitForUnregisteredAsync(string scriptName)
            => Page.WaitForConsoleMessageAsync(new() { Predicate = message => message.Text.Contains($"script '{scriptName}' is not registered yet") });
    }
}
