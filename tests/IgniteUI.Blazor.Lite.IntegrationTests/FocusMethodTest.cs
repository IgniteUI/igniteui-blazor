using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;
using Microsoft.Playwright;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    /// <summary>
    /// The FocusComponent / BlurComponent API against the live components on the TestBed's
    /// <c>/focus-method</c> page - GitHub #297, where those methods threw instead of moving
    /// focus. Two client-side defects sat behind it, and neither is reachable from a unit
    /// contract, which stops at the serialized message:
    /// <list type="bullet">
    /// <item>the <c>IgbFocusOptions</c> argument crosses by value and is rebuilt from its wire
    /// type name, so the name has to be one the client registers a description for;</item>
    /// <item>the method dispatch resolves the target's members through a prototype walk that
    /// stops below <c>HTMLElement</c>, so <c>focus</c>/<c>blur</c> inherited from it - the
    /// buttons, which rely on <c>delegatesFocus</c> rather than overriding them - were not
    /// recognized as invokable.</item>
    /// </list>
    /// Either one makes the client hand back a plain error string, which the wrapper then
    /// fails to parse as the JSON return value; the assertions below read both what the
    /// wrapper reported and which element the browser actually focused.
    /// </summary>
    [Parallelizable(ParallelScope.Self)]
    public class FocusMethodTest : BlazorPageTest<Program>
    {
        /// <summary>
        /// Page key and client tag for every component exposing the methods, which between them
        /// cover all seven TS metadata classes that have to register the focus options
        /// (ButtonBase, CheckboxBase, InputBase, DateTimeInputBase, BaseComboBox, Radio,
        /// ToggleButton). Registration is per module, so a component whose module misses it
        /// breaks on its own while the rest keep working.
        /// </summary>
        private static readonly (string Key, string Selector)[] Components =
        [
            ("button", "igc-button"),
            ("icon-button", "igc-icon-button"),
            ("toggle-button", "igc-toggle-button"),
            ("checkbox", "igc-checkbox"),
            ("switch", "igc-switch"),
            ("radio", "igc-radio"),
            ("input", "igc-input"),
            ("mask-input", "igc-mask-input"),
            ("date-time-input", "igc-date-time-input"),
            ("select", "igc-select"),
            ("combo", "igc-combo"),
        ];

        /// <summary>Components whose focus/blur come from <c>HTMLElement</c> rather than an override.</summary>
        private static readonly (string Key, string Selector)[] NativelyInherited =
        [
            ("button", "igc-button"),
            ("icon-button", "igc-icon-button"),
        ];

        private readonly List<string> consoleErrors = [];

        [Test]
        public async Task FocusComponent_MovesFocusToTheComponent()
        {
            await OpenPageAsync();

            var reported = new Dictionary<string, string>();
            var focused = new Dictionary<string, string>();
            foreach (var (key, selector) in Components)
            {
                reported[key] = await InvokeAsync("FocusComponent", key);
                focused[key] = await ActiveElementStateAsync(selector);
            }

            Assert.Multiple(() =>
            {
                foreach (var (key, _) in Components)
                {
                    Assert.That(reported[key], Is.EqualTo("ok"), $"FocusComponentAsync on {key} should not fail");
                    Assert.That(focused[key], Is.EqualTo("focused"), $"FocusComponentAsync on {key} should leave it focused");
                }

                Assert.That(consoleErrors, Is.Empty, "the client reported: " + string.Join("; ", consoleErrors));
            });
        }

        /// <summary>
        /// Blur takes no arguments, so it turns on the method dispatch alone - the half of the
        /// issue the options argument would otherwise mask. Focus is set natively here for the
        /// same reason: the setup must not depend on the path under test.
        /// </summary>
        [Test]
        public async Task BlurComponent_RemovesFocusFromNativelyInheritedComponents()
        {
            await OpenPageAsync();

            var reported = new Dictionary<string, string>();
            var afterBlur = new Dictionary<string, string>();
            foreach (var (key, selector) in NativelyInherited)
            {
                await Page.EvaluateAsync("(s) => document.querySelector(s).focus()", selector);
                Assert.That(await ActiveElementStateAsync(selector), Is.EqualTo("focused"),
                    $"{key} should start out focused, otherwise the blur proves nothing");

                reported[key] = await InvokeAsync("BlurComponent", key);
                afterBlur[key] = await ActiveElementStateAsync(selector);
            }

            Assert.Multiple(() =>
            {
                foreach (var (key, _) in NativelyInherited)
                {
                    Assert.That(reported[key], Is.EqualTo("ok"), $"BlurComponentAsync on {key} should not fail");
                    Assert.That(afterBlur[key], Is.Not.EqualTo("focused"), $"BlurComponentAsync on {key} should take focus off it");
                }

                Assert.That(consoleErrors, Is.Empty, "the client reported: " + string.Join("; ", consoleErrors));
            });
        }

        private async Task OpenPageAsync()
        {
            // NUnit reuses one fixture instance across the tests in the class, so the
            // collected errors have to be dropped per test rather than per instance.
            consoleErrors.Clear();
            Page.Console += OnConsoleMessage;
            await Page.GotoAsync("http://localhost:5249/focus-method");
            // The object reference is registered after the first interactive render, and every
            // component element exists by the time the client has built them all.
            await Page.WaitForFunctionAsync("() => !!window.clientPageRef");
            var selectors = string.Join(",", Components.Select(c => $"'{c.Selector}'"));
            await Page.WaitForFunctionAsync($"() => [{selectors}].every(s => document.querySelector(s) !== null)");
        }

        /// <summary>
        /// Collects script errors the client logs - the failing dispatch reports itself through
        /// console.error. Asset loads are left out: the TestBed page asks for a grid theme the
        /// package does not carry, and that 404 says nothing about the API under test.
        /// </summary>
        private void OnConsoleMessage(object? sender, IConsoleMessage message)
        {
            if (message.Type == "error" && !message.Text.StartsWith("Failed to load resource"))
            {
                consoleErrors.Add(message.Text);
            }
        }

        /// <summary>Runs one of the page's scenario methods; "ok", or what the wrapper raised.</summary>
        private Task<string> InvokeAsync(string method, string key)
            => Page.EvaluateAsync<string>($"(k) => window.clientPageRef.invokeMethodAsync('{method}', k)", key);

        /// <summary>
        /// "focused" when the component holds focus, otherwise what does hold it. Focus landing
        /// inside a shadow root reports the host, so the component element is the one to compare.
        /// </summary>
        private Task<string> ActiveElementStateAsync(string selector)
            => Page.EvaluateAsync<string>(
                @"(s) => {
                    const el = document.querySelector(s);
                    if (!el) return 'missing: ' + s;
                    if (document.activeElement === el) return 'focused';
                    return 'focus is on: ' + (document.activeElement ? document.activeElement.tagName.toLowerCase() : 'nothing');
                  }",
                selector);
    }
}
