using DAL.Entities;

namespace DAL.Data.Repository
{
    public interface IProductsRepository : IRepository<ProductViewModel>
    {
        Task Update(Product product);

        Task Create(Product product);

        Task<Product> FindProduct(int? id);
        bool AreNoProducts();
    }
}