namespace DAL.Entities
{
    /// <summary>
    /// View model of the product
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Product id
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Supplier name
        /// </summary>
        public string? SupplierName { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string? CategoryName { get; set; }
        
        /// <summary>
        /// Quantity per unit
        /// </summary>
        public string? QuantityPerUnit { get; set; }
        
        /// <summary>
        /// Price of the unit
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// How much unit in the stock
        /// </summary>
        public short? UnitsInStock { get; set; }

        /// <summary>
        /// Units on order
        /// </summary>
        public short? UnitsOnOrder { get; set; }

        /// <summary>
        /// Reorder level
        /// </summary>
        public short? ReorderLevel { get; set; }

        /// <summary>
        /// Does product have discount
        /// </summary>
        public bool Discontinued { get; set; }
    }
}
