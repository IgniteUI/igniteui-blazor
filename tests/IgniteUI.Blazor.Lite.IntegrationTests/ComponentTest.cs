using IgniteUI.Blazor.Lite.IntegrationTests.Infrastructure;
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

            await Page.EvaluateAsync(@"renderComponent('" + this.componentName + "');");
            string[] error = await Page.EvaluateAsync<string[]>(@"getErrors();");
            Assert.That(error.Length == 0, "There were errors : " + string.Join(", \n", error));
        }
    }
}
