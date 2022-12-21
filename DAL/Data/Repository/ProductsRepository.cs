using DAL.Entities;
using DAL.Entities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductSotreContext _context;

        public ProductsRepository(ProductSotreContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return from product in _context.Products
                    join category in _context.Categories
                    on product.CategoryID equals category.CategoryID
                    join supplier in _context.Suppliers
                    on product.SupplierID equals supplier.SupplierID
                    select new ProductViewModel()
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        SupplierName = supplier.CompanyName,
                        CategoryName = category.CategoryName,
                        QuantityPerUnit = product.QuantityPerUnit,
                        UnitPrice = product.UnitPrice,
                        UnitsInStock = product.UnitsInStock,
                        UnitsOnOrder = product.UnitsOnOrder,
                        ReorderLevel = product.ReorderLevel,
                        Discontinued = product.Discontinued
                    };
        }

        public async Task Create(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductID))
                {
                    throw new EntityNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public bool AreNoProducts()
        {
            return _context.Products == null;
        }

        public async Task<Product> FindProduct(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}