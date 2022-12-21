using DAL.Entities;

namespace DAL.Data.Repository
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private readonly ProductSotreContext _context;

        public SupplierRepository(ProductSotreContext context)
        {
            _context = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;
        }
    }
}
