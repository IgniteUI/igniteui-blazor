using Bunit;
using IgniteUI.Blazor.Controls;
using Microsoft.AspNetCore.Components;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// Covers the content-child host hook the full IgniteUI.Blazor package uses to collect elements of this package
/// (format specifiers) declared inside its components, e.g. an <see cref="IgbNumberFormatSpecifier"/> inside a chart axis.
/// </summary>
public class ContentChildHostTests : BlazorComponentTestBase
{
    [Fact]
    public void FormatSpecifier_InsideHost_AttachesOnInitializeAndDetachesOnDispose()
    {
        var cut = Render<TestHost>(p => p
            .AddChildContent<IgbNumberFormatSpecifier>(s => s.Add(x => x.MinimumFractionDigits, 2)));

        var host = cut.Instance;
        var specifier = Assert.Single(host.Attached);
        Assert.IsType<IgbNumberFormatSpecifier>(specifier);
        Assert.Same(cut.FindComponent<IgbNumberFormatSpecifier>().Instance, specifier);

        cut.Render(p => p.AddChildContent(builder => { }));

        Assert.Empty(host.Attached);
        Assert.Equal(1, host.DetachCount);
    }

    [Fact]
    public void FormatSpecifier_InsideHostThatDeclinesChild_IsNotDetachedFromIt()
    {
        var cut = Render<TestHost>(p => p
            .Add(x => x.Accept, false)
            .AddChildContent<IgbNumberFormatSpecifier>());

        var host = cut.Instance;
        Assert.Empty(host.Attached);

        cut.Render(p => p.Add(x => x.Accept, false).AddChildContent(builder => { }));

        Assert.Equal(0, host.DetachCount);
    }

    [Fact]
    public void FormatSpecifier_InsideNonHostElement_RendersWithoutAttaching()
    {
        var cut = Render<NonHost>(p => p.AddChildContent<IgbNumberFormatSpecifier>());

        Assert.NotNull(cut.FindComponent<IgbNumberFormatSpecifier>().Instance);
        cut.Render(p => p.AddChildContent(builder => { }));
        Assert.Empty(cut.FindComponents<IgbNumberFormatSpecifier>());
    }

    [Fact]
    public void NestedHosts_ChildAttachesToNearestHost()
    {
        var cut = Render<TestHost>(p => p
            .AddChildContent<TestHost>(inner => inner
                .AddChildContent<IgbNumberFormatSpecifier>()));

        var outer = cut.Instance;
        var inner = cut.FindComponents<TestHost>().Single(h => !ReferenceEquals(h.Instance, outer)).Instance;

        Assert.Empty(outer.Attached);
        Assert.Single(inner.Attached);
    }

    private sealed class TestHost : BaseRendererElement, IContentChildHost
    {
        [Parameter] public bool Accept { get; set; } = true;

        public List<BaseRendererElement> Attached { get; } = new();

        public int DetachCount { get; private set; }

        private protected override string? ParentTypeName => "TestHostParent";

        bool IContentChildHost.AttachContentChild(BaseRendererElement child)
        {
            if (!Accept)
            {
                return false;
            }
            Attached.Add(child);
            return true;
        }

        void IContentChildHost.DetachContentChild(BaseRendererElement child)
        {
            DetachCount++;
            Attached.Remove(child);
        }
    }

    private sealed class NonHost : BaseRendererElement
    {
        private protected override string? ParentTypeName => "NonHostParent";
    }
}
