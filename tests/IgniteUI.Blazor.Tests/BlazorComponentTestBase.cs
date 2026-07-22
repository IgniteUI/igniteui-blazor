using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Base class for component tests providing shared test context setup.
/// </summary>
public abstract class BlazorComponentTestBase : TestContext
{
    protected Mock<IJSRuntime> JsRuntimeMock { get; }
    protected IIgniteUIBlazor IgniteUIBlazor { get; }

    protected BlazorComponentTestBase()
    {
        JsRuntimeMock = new Mock<IJSRuntime>();
        JsRuntimeMock
            .Setup(x => x.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<object[]>()))
            .ReturnsAsync((object)null!);

        IgniteUIBlazor = new IgniteUIBlazor(JsRuntimeMock.Object);
        Services.AddSingleton<IIgniteUIBlazor>(IgniteUIBlazor);
    }
}
