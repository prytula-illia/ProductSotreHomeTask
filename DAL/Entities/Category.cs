namespace DAL.Entities
{
    /// <summary>
    /// Category of the products in the store
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Category id
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Picture to represent category formated as byte array
        /// </summary>
        public byte[]? Picture { get; set; }
    }
}