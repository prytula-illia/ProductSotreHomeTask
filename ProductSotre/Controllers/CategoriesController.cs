using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSotre.Filters;

namespace ProductSotre.Controllers
{
    [Authorize]
    [ActionLogging(logParameters: true)]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _repository;

        public CategoriesController(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var categories = _repository.GetAll();
            return View(categories);
        }

        [HttpGet]
        [Route("images/{id?}")]
        [Route("[controller]/[action]")]
        public IActionResult GetImage(int id)
        {
            var image = _repository.GetImage(id);
            
            if (image == null)
                return NotFound();
            
            return File(image, "image/jpeg");
        }
        
        public async Task<IActionResult> UpdateImage(int? id)
        {
            if (id == null || _repository.AreNoCategories())
            {
                return NotFound();
            }

            var category = await _repository.FindCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(new FileUpload()
            {
                Id = id.Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateImage(int id, [Bind("Id, FormFile")] FileUpload file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            await _repository.UpdateImage(file);
            return RedirectToAction(nameof(Index));
        }
    }
}