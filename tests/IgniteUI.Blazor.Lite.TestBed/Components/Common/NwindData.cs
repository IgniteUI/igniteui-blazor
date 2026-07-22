public class NwindDataItem
{
    public string? ProductName { get; set; } = string.Empty;
    //public double SupplierID { get; set; }
    //public double CategoryID { get; set; }
    public string QuantityPerUnit { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
    //public double UnitsInStock { get; set; }
    //public double UnitsOnOrder { get; set; }
    //public double ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public string OrderDate { get; set; } = string.Empty;
    //public double Rating { get; set; }
}
public class NwindDataItem_LocationsItem
{
    public string Shop { get; set; } = string.Empty;
    public string LastInventory { get; set; } = string.Empty;
}

public class NwindData
    : List<NwindDataItem>
{
    public NwindData()
    {
        this.Add(new NwindDataItem()
        {
            ProductName = @"Chai",
            QuantityPerUnit = @"10 boxes x 20 bags",
            UnitPrice = 18,
            Discontinued = false,
            OrderDate = @"2012-02-12"

        }
        );
        this.Add(new NwindDataItem()
        {
            ProductName = @"Chang",
            QuantityPerUnit = @"24 - 12 oz bottles",
            UnitPrice = 19,
            Discontinued = true,
            OrderDate = @"2003-03-17",

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Aniseed Syrup",
            QuantityPerUnit = @"12 - 550 ml bottles",
            UnitPrice = 10,
            Discontinued = false,
            OrderDate = @"2006-03-17"
        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Chef Antons Cajun Seasoning",
            QuantityPerUnit = @"48 - 6 oz jars",
            UnitPrice = 22,
            Discontinued = false,
            OrderDate = @"2016-03-17"

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Chef Antons Gumbo Mix",
            QuantityPerUnit = @"36 boxes",
            UnitPrice = 21.35,
            Discontinued = true,
            OrderDate = @"2011-11-11"

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Grandmas Boysenberry Spread",
            QuantityPerUnit = @"12 - 8 oz jars",
            UnitPrice = 25,
            Discontinued = false,
            OrderDate = @"2017-12-17"

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Uncle Bobs Organic Dried Pears",
            QuantityPerUnit = @"12 - 1 lb pkgs.",
            UnitPrice = 30,
            Discontinued = false,
            OrderDate = @"2016-07-17"

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Northwoods Cranberry Sauce",
            QuantityPerUnit = @"12 - 12 oz jars",
            UnitPrice = 40,
            Discontinued = false,
            OrderDate = @"2018-01-17"

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Mishi Kobe Niku",
            QuantityPerUnit = @"18 - 500 g pkgs.",
            UnitPrice = 97,
            Discontinued = true,
            OrderDate = @"2010-02-17"

        });
        this.Add(new NwindDataItem()
        {
            ProductName = @"Ikura",
            QuantityPerUnit = @"12 - 200 ml jars",
            UnitPrice = 31,
            Discontinued = false,
            OrderDate = @"2008-05-17"

        });
    }
}
