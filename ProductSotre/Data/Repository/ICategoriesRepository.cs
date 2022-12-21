using DAL.Entities;
using System.Drawing;

namespace DAL.Data.Repository
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        bool AreNoCategories();

        Task<Category> FindCategory(int? id);

        byte[] GetImage(int id);

        Task UpdateImage(FileUpload file);
    }
}