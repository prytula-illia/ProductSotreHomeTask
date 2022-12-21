using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _repository;

        public ProductsController(IProductsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all products in the store
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductViewModel> GetProducts()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Create new product
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.Create(product);
            return Created($"{Request.Path}/{product.ProductID}", product);
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            await _repository.Update(product);
            return new NoContentResult();
        }
    }
}