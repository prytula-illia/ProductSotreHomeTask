using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    /// <summary>
    /// Product in the store
    /// </summary>
    public class Product : IValidatableObject
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [Required]
        [StringLength(20)]
        public string ProductName { get; set; }

        /// <summary>
        /// Supplier id
        /// </summary>
        public int? SupplierID { get; set; }

        /// <summary>
        /// Category id
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// Quantity per unit
        /// </summary>
        public string? QuantityPerUnit { get; set; }

        /// <summary>
        /// Price per unit
        /// </summary>
        [Range(0, 99999.99, ErrorMessage = "Unit price should be in range 0 - 99999.99.")]
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Avaliable units in stock
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
        [Required]
        public bool Discontinued { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(UnitsInStock < 0)
            {
                errors.Add(new ValidationResult("Cannot be less than 0 units in stock"));
            }

            return errors;
        }
    }
}