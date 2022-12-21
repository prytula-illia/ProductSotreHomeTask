using NUnit.Framework;
using RichardSzalay.MockHttp;
using System.Net;

namespace ProductStore.Tests
{
    internal class TestProxy
    {
        private HttpClient _httpClient;
        private string _httpHost = "http://localhost/api";
        private const string _categoryPath = "api/Categories";
        private const string _productPath = "api/Products";

        [SetUp]
        public void SetUp()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When($"{_httpHost}/{_categoryPath}")
                    .Respond("application/json", "{}");

            mockHttp.When($"{_httpHost}/{_productPath}")
                    .Respond("application/json", "{}");

            _httpClient = new HttpClient(mockHttp);
        }

        [Test]
        public async Task GetCategories_Returns_OK_Response()
        {
            var response = await GetCategoriesAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetProducts_Returns_OK_Response()
        {
            var response = await GetProductsAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<HttpResponseMessage> GetCategoriesAsync()
        {
            return await _httpClient.GetAsync($"{_httpHost}/{_categoryPath}");
        }

        private async Task<HttpResponseMessage> GetProductsAsync()
        {
            return await _httpClient.GetAsync($"{_httpHost}/{_productPath}");
        }
    }
}