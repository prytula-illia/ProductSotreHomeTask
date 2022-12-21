using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using ProductSotre.Controllers;

namespace ProductStore.Tests.Controllers
{
    internal class ProductTests
    {
        private ProductsController _productsController;
        private IProductsRepository _productsRepository;

        [SetUp]
        public void SetUp()
        {
            _productsRepository = Substitute.For<IProductsRepository>();
            _productsRepository.GetAll().Returns(new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    ProductName = "Product1"
                }
            });

            _productsController = new ProductsController(Substitute.For<IConfiguration>(),
                _productsRepository,
                Substitute.For<ICategoriesRepository>(),
                Substitute.For<IRepository<Supplier>>());
        }

        [Test]
        public void Index_Should_Return_Page_With_All_Products()
        {
            var actual = _productsController.Index();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<ViewResult>(actual);
            var actualModel = (actual as ViewResult)?.Model;
            var products = actualModel as IEnumerable<ProductViewModel>;
            Assert.IsNotNull(actualModel);
            Assert.IsNotEmpty(products);
            Assert.AreEqual(products.First().ProductName, "Product1");
        }

        [Test]
        public async Task Create_When_Model_Is_Valid_Redirects_To_Action()
        {
            var result = await _productsController.Create(new Product());

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public async Task Edit_When_Id_Is_Null_Returns_Not_Found()
        {
            var result = await _productsController.Edit(null);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_When_No_Product_With_Id_Returns_Not_Found()
        {
            _productsRepository.FindProduct(1).ReturnsNullForAnyArgs();
            var result = await _productsController.Edit(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_When_Id_Is_Not_The_Same_As_ProductId_Returns_Not_Found()
        {
            var result = await _productsController.Edit(1, new Product() { ProductID = 2});

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}