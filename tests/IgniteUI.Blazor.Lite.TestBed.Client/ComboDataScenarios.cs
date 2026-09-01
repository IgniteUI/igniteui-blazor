using System.Collections.ObjectModel;

namespace IgniteUI.Blazor.Lite.TestBed.Client;

/// <summary>
/// Combo data-binding scenarios shared between the WASM e2e page (<c>ComboDataPage</c>,
/// compiled here) and the in-process unit suite (<c>UnmarshalledDataChannelTests</c>,
/// which links this file), so both exercise the same data shapes.
/// </summary>
public static class ComboDataScenarios
{
    public sealed record Scenario(object Data, string? ValueKey, string? DisplayKey);

    public static Scenario Get(string name) => name switch
    {
        "typed-products" => new(Products(), "Id", "Name"),
        "objects-as-values" => new(Products(), null, "Name"),
        "observable-products" => new(new ObservableCollection<Product>(Products()), "Id", "Name"),
        "nullable-readings" => new(new List<Reading>
        {
            new() { Id = 1, Label = "full", Quantity = 5, Value = 2.5, Flagged = true, MeasuredOn = new DateTime(2026, 3, 3) },
            new() { Id = 2, Label = "empty", Quantity = null, Value = null, Flagged = null, MeasuredOn = null },
            new() { Id = 3, Label = "mixed", Quantity = 7, Value = null, Flagged = false, MeasuredOn = null },
        }, "Id", "Label"),
        "nested-orders" => new(new List<Order>
        {
            new() { Id = 1, Reference = "ORD-1", Customer = new Customer { Name = "Maria", City = "Berlin" } },
            new() { Id = 2, Reference = "ORD-2", Customer = new Customer { Name = "Ana", City = "Madrid" } },
        }, "Id", "Reference"),
        "nested-field-shipments" => new(new List<Shipment>
        {
            new() { Id = 1, Code = "SHP-1", Box = new BoxSize { Width = 2.5, Height = 1.5 } },
            new() { Id = 2, Code = "SHP-2", Box = new BoxSize { Width = 4.0, Height = 3.0 } },
        }, "Id", "Code"),
        "primitive-strings" => new(new List<string> { "alpha", "beta", "gamma" }, null, null),
        _ => throw new ArgumentException($"Unknown combo data scenario \"{name}\".", nameof(name)),
    };

    public static List<Product> Products() =>
    [
        new() { Id = 1, Name = "Chai", Price = 18.5, Discontinued = false, UnitsSold = 42_000_000_000, Restocked = new DateTime(2026, 1, 15) },
        new() { Id = 2, Name = "Chang", Price = 19.0, Discontinued = true, UnitsSold = 7, Restocked = new DateTime(2025, 6, 30) },
        new() { Id = 3, Name = "Aniseed Syrup", Price = 10.0, Discontinued = false, UnitsSold = 1300, Restocked = new DateTime(2024, 12, 1) },
    ];

    public static Product AddedProduct() =>
        new() { Id = 4, Name = "Added", Price = 4.4, UnitsSold = 4, Restocked = new DateTime(2026, 4, 4) };

    public static Product ReplacementProduct() =>
        new() { Id = 99, Name = "Replaced", Price = 9.9, UnitsSold = 9, Restocked = new DateTime(2026, 9, 9) };

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public bool Discontinued { get; set; }
        public long UnitsSold { get; set; }
        public DateTime Restocked { get; set; }
    }

    public class Reading
    {
        public int Id { get; set; }
        public string Label { get; set; } = "";
        public int? Quantity { get; set; }
        public double? Value { get; set; }
        public bool? Flagged { get; set; }
        public DateTime? MeasuredOn { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Reference { get; set; } = "";
        public Customer Customer { get; set; } = new();
    }

    public class Customer
    {
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
    }

    public class Shipment
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public BoxSize Box { get; set; } = new();
    }

    public class BoxSize
    {
        public double Width;
        public double Height;
    }
}
