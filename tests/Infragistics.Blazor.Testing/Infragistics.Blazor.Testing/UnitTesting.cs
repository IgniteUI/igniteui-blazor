using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;
namespace Infragistics.Blazor.Unit.Testing
{
    public class UnitTesting : TestContext
    {
        //These could be simple unit test that check the initial render.
        [Fact]
        public void DialogRender()
        {
            using var ctx = new TestContext();
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            ctx.Services.AddIgniteUIBlazor(
                typeof(IgbDialogModule));
            var componentUnderTest = ctx.RenderComponent<IgbDialog>(parameters => {
                parameters.Add(x => x.Title, "Test");
            });
            Assert.NotNull(componentUnderTest);
        }
    }
}