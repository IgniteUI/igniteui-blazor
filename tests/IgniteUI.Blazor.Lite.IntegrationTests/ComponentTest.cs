using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;
using IgniteUI.Blazor.Lite.TestBed.Components.Common;
using Microsoft.Playwright;
using NUnit.Framework.Internal;

namespace IgniteUI.Blazor.Lite.IntegrationTests
{
    class ComponentData
    {
        public static List<string> ComponentNames = TestUtil.GetComponentsForTesting();
    }

    [Parallelizable(ParallelScope.Self)]
    [TestFixtureSource(typeof(ComponentData), nameof(ComponentData.ComponentNames))]
    public class ComponentTest : BlazorPageTest<Program>
    {

        private string componentName;
        public ComponentTest(string componentName)
        {
            this.componentName = componentName;
        }

        [Test]
        public async Task GenericComponentTest()
        {
            TestContext.Out.WriteLine("Test started for " + this.componentName);

            await Page.GotoAsync("http://localhost:5249/");
            // wait for blazor to load
            await Page.WaitForConsoleMessageAsync(new PageWaitForConsoleMessageOptions
            {
                Predicate = msg => msg.Text.Contains("App Loaded.")
            });
            //await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var summary = await Page.EvaluateAsync<ComponentRunSummary>(
                @"renderComponent('" + this.componentName + "')");
            string[] error = await Page.EvaluateAsync<string[]>(@"getErrors();");

            Assert.That(summary, Is.Not.Null, "The run returned no summary.");
            TestContext.Out.WriteLine(summary.ToString());

            Assert.Multiple(() =>
            {
                Assert.That(error, Is.Empty, "There were errors : " + string.Join(", \n", error));
                Assert.That(summary.Failure, Is.Null);
            });
        }
    }
}
