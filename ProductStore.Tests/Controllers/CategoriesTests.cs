using DAL.Data.Repository;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using ProductSotre.Controllers;

namespace ProductStore.Tests.Controllers
{
    internal class CategoriesTests
    {
        private CategoriesController _categoriesController;
        
        [SetUp]
        public void SetUp()
        {
            var categoryRepository = Substitute.For<ICategoriesRepository>();
            categoryRepository.GetAll().Returns(new List<Category>()
            {
                new Category()
                {
                    CategoryName = "Name1"
                }
            });

            _categoriesController = new CategoriesController(categoryRepository);
        }

        [Test]
        public void Category_Index_Should_Return_Page_With_All_Categories()
        {
            var actual = _categoriesController.Index();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<ViewResult>(actual);
            var actualModel = (actual as ViewResult)?.Model;
            var categories = actualModel as IEnumerable<Category>;
            Assert.IsNotNull(actualModel);
            Assert.IsNotEmpty(categories);
            Assert.AreEqual(categories.First().CategoryName, "Name1");
        }
    }
}