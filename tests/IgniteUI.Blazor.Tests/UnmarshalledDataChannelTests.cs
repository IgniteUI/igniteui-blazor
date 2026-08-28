using System.Collections.ObjectModel;
using Bunit;
using IgniteUI.Blazor.Controls;
using IgniteUI.Blazor.Lite.TestBed.Client;
using IgniteUI.Blazor.Tests.Interop;
using static IgniteUI.Blazor.Tests.Interop.RendererMessageInteropHarness;

namespace IgniteUI.Blazor.Tests;

/// <summary>
/// The in-process (unmarshalled) data channel, driven through <see cref="IgbCombo{T}"/> —
/// the unit twin of the browser-based <c>ComboDataTest</c> integration suite, sharing its
/// scenarios (<see cref="ComboDataScenarios"/>). Assertions read the
/// <see cref="UnmarshalledColumn"/> messages the channel emits; the pointer transport and
/// the JS-side reader remain integration-only.
/// </summary>
public class UnmarshalledDataChannelTests : BunitContext
{
    private readonly RendererMessageInteropHarness _interop;

    public UnmarshalledDataChannelTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        _interop = new RendererMessageInteropHarness(JSInterop, forceJsonDataMarshalling: false);
        _interop.ConfigureServices(Services);
    }

    [Fact]
    public void TypedFields_TransferAsTypedColumns()
    {
        var create = RenderScenario("typed-products");

        Assert.Equal(3, Column(create, "Name").ActualCount);
        Assert.Equal(["Chai", "Chang", "Aniseed Syrup"], Column(create, "Name").StringValues.Take(3));
        Assert.Equal([18.5, 19.0, 10.0], Column(create, "Price").DoubleValues.Take(3));
        Assert.Equal([0, 1, 0], Column(create, "Discontinued").IntValues.Take(3));
        Assert.Equal([42_000_000_000, 7, 1300], Column(create, "UnitsSold").LongValues.Take(3));
        Assert.StartsWith("2026-01-15T00:00:00", Column(create, "Restocked").StringValues[0]);
        Assert.True(Guid.TryParse(Column(create, "___id").StringValues[0], out _), "items should carry a uuid ___id column");
    }

    [Fact]
    public void NullableFields_TransferValuesWithNullFlags()
    {
        var create = RenderScenario("nullable-readings");

        var quantity = Column(create, "Quantity");
        Assert.Equal([false, true, false], quantity.NullValues.Take(3));
        Assert.Equal(5, quantity.IntValues[0]);
        Assert.Equal(7, quantity.IntValues[2]);

        var value = Column(create, "Value");
        Assert.Equal([false, true, true], value.NullValues.Take(3));
        Assert.Equal(2.5, value.DoubleValues[0]);

        var flagged = Column(create, "Flagged");
        Assert.Equal([false, true, false], flagged.NullValues.Take(3));
        Assert.Equal(1, flagged.IntValues[0]);
        Assert.Equal(0, flagged.IntValues[2]);

        var measuredOn = Column(create, "MeasuredOn");
        Assert.StartsWith("2026-03-03T00:00:00", measuredOn.StringValues[0]);
        Assert.Null(measuredOn.StringValues[1]);
    }

    [Fact]
    public void NestedObjects_TransferAsDottedPathColumns()
    {
        var create = RenderScenario("nested-orders");

        Assert.Equal(["ORD-1", "ORD-2"], Column(create, "Reference").StringValues.Take(2));
        Assert.Equal(["Maria", "Ana"], Column(create, "Customer.Name").StringValues.Take(2));
        Assert.Equal(["Berlin", "Madrid"], Column(create, "Customer.City").StringValues.Take(2));
    }

    [Fact]
    public void PrimitiveStrings_TransferAsPrimitiveColumn()
    {
        var create = RenderScenario("primitive-strings");

        var values = Column(create, "___primitiveValueCollection");
        Assert.Equal(3, values.ActualCount);
        Assert.Equal(["alpha", "beta", "gamma"], values.StringValues.Take(3));
    }

    [Fact(Skip = "Item types with public primitive-typed fields crash schema creation: JsonDataSourceSchema.Commit " +
        "stores the typed field getters in a Func<object, object>[] and throws ArrayTypeMismatchException " +
        "(TypedPropertyGetters uses Delegate[]). Enable once the field getter array type is fixed.")]
    public void NestedPublicFields_TransferAsColumns()
    {
        var create = RenderScenario("nested-field-shipments");

        Assert.Equal([2.5, 4.0], Column(create, "Box.Width").DoubleValues.Take(2));
        Assert.Equal([1.5, 3.0], Column(create, "Box.Height").DoubleValues.Take(2));
    }

    [Fact]
    public void ObservableCollectionMutations_EmitChannelMessages()
    {
        var scenario = ComboDataScenarios.Get("observable-products");
        var items = (ObservableCollection<ComboDataScenarios.Product>)scenario.Data;
        RenderScenario(scenario);

        items.Add(ComboDataScenarios.AddedProduct());
        var insert = WaitFor("igUnmarshalledDataSourceInsert");
        Assert.Equal(3, insert.Index);
        Assert.Equal(4, Column(insert, "Name").ActualCount);
        Assert.Equal("Added", Column(insert, "Name").StringValues[3]);

        items.RemoveAt(0);
        var remove = WaitFor("igUnmarshalledDataSourceRemove");
        Assert.Equal(0, remove.Index);
        Assert.Equal(3, Column(remove, "Name").ActualCount);
        Assert.Equal("Chang", Column(remove, "Name").StringValues[0]);

        // A replace crosses as remove + insert at the same index, not as an update message.
        items[0] = ComboDataScenarios.ReplacementProduct();
        var replaceInsert = WaitFor("igUnmarshalledDataSourceInsert",
            m => m.Index == 0 && Column(m, "Name").StringValues[0] == "Replaced");
        Assert.Equal(3, Column(replaceInsert, "Name").ActualCount);

        // Clear resets the source's columns; the message itself carries none.
        items.Clear();
        var clear = WaitFor("igUnmarshalledDataSourceClear");
        Assert.Empty(clear.Columns!);
    }

    private UnmarshalledColumnMessage RenderScenario(string scenario) =>
        RenderScenario(ComboDataScenarios.Get(scenario));

    private UnmarshalledColumnMessage RenderScenario(ComboDataScenarios.Scenario scenario)
    {
        _interop.PrimeReady();
        Render<IgbCombo<object>>(ps => ps
            .Add(c => c.Data, scenario.Data)
            .Add(c => c.ValueKey, scenario.ValueKey)
            .Add(c => c.DisplayKey, scenario.DisplayKey));
        _interop.MakeReady();
        return WaitFor("igUnmarshalledDataSourceCreate");
    }

    private UnmarshalledColumnMessage WaitFor(string methodName, Func<UnmarshalledColumnMessage, bool>? match = null) =>
        _interop.WaitForUnmarshalledMessage(m => m.MethodName == methodName && m.Columns is not null && (match?.Invoke(m) ?? true))
        ?? throw new Xunit.Sdk.XunitException($"No \"{methodName}\" column message arrived on the unmarshalled channel.");

    private static UnmarshalledColumn Column(UnmarshalledColumnMessage message, string propertyPath) =>
        Assert.Single(message.Columns!, c => c.PropertyPath == propertyPath);
}
