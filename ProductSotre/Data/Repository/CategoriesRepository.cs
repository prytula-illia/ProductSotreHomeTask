using DAL.Entities;
using DAL.Entities.Exceptions;

namespace DAL.Data.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ProductSotreContext _context;
        private const int BrokenBytesCount = 78;

        public CategoriesRepository(ProductSotreContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public bool AreNoCategories()
        {
            return _context.Categories == null;
        }

        public async Task<Category> FindCategory(int? id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public byte[] GetImage(int id)
        {
            var category = _context.Categories.Find(id);

            return category.Picture.Skip(78).ToArray();
        }

        public async Task UpdateImage(FileUpload file)
        {
            if (file.FormFile is null)
            {
                throw new BusinessLogicException("FormFile cannot be empty");
            }

            var category = _context.Categories.Find(file.Id);
            if(category is null)
            {
                throw new EntityNotFoundException();
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.FormFile.CopyToAsync(memoryStream);

                var stream = memoryStream.ToArray();

                var result = new byte[BrokenBytesCount + stream.Length];

                stream.CopyTo(result, BrokenBytesCount);

                category.Picture = result;
            }

            await _context.SaveChangesAsync();
        }
    }
}