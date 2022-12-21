using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSotre.Filters;
using Serilog;

namespace ProductSotre.Controllers
{
    [Authorize]
    [ActionLogging]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration, 
            IProductsRepository productsRepository,
            ICategoriesRepository categoryRepository,
            IRepository<Supplier> supplierRepository)
        {
            _configuration = configuration;
            _productsRepository = productsRepository;
            _categoriesRepository = categoryRepository;
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            var maxProductAmount = _configuration["MaximumProductAmount"];
            Log.Information($"Read MaximumProductAmount from configuration. Current value: {maxProductAmount}");
            ViewData["M"] = maxProductAmount;

            var products = _productsRepository.GetAll();

            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["Categories"] = _categoriesRepository.GetAll();
            ViewData["Suppliers"] = _supplierRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsRepository.Create(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _productsRepository.AreNoProducts())
            {
                return NotFound();
            }

            var product = await _productsRepository.FindProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = _categoriesRepository.GetAll();
            ViewData["Suppliers"] = _supplierRepository.GetAll();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productsRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}