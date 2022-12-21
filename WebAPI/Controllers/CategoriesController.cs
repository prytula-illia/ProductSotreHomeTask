using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _repository;

        public CategoriesController(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>
        /// List of categories
        /// </returns>
        [HttpGet(Name = "GetCategories")]
        public IEnumerable<Category> GetCategories()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<Category> GetCategoryById(int id)
        {
            return await _repository.FindCategory(id);
        }

        /// <summary>
        /// Gets category image by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Image in format of byte array</returns>
        [HttpGet("/image/{id:int}", Name = "GetImage")]
        public byte[] GetImage(int id)
        {
            return _repository.GetImage(id);
        }

        /// <summary>
        /// Update category image
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("/image", Name = "UpdateImage")]
        public async Task<IActionResult> UpdateImage(FileUpload file)
        {
            await _repository.UpdateImage(file);
            return new NoContentResult();
        }
    }
}