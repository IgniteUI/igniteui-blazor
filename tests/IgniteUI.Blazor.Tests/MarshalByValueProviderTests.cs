using IgniteUI.Blazor.Controls;

namespace IgniteUI.Blazor.Tests;

/// <summary>The full IgniteUI.Blazor package registers its by-value types next to this package's own.</summary>
public class MarshalByValueProviderTests
{
    [Fact]
    public void AddProvider_ExtendsMustMarshalAndCreate_AfterOwnTypes()
    {
        var created = new object();
        MarshalByValueFactory.AddProvider(
            name => name == "ProviderTestOnlyType",
            name => name == "ProviderTestOnlyType" ? created : null);

        Assert.True(MarshalByValueFactory.MustMarshalByValue("ProviderTestOnlyType"));
        Assert.Same(created, MarshalByValueFactory.CreateInstance("ProviderTestOnlyType"));
        Assert.False(MarshalByValueFactory.MustMarshalByValue("SomeUnknownType"));
        Assert.Null(MarshalByValueFactory.CreateInstance("SomeUnknownType"));
        Assert.True(MarshalByValueFactory.MustMarshalByValue("CalendarFormatOptions"));
    }
}
