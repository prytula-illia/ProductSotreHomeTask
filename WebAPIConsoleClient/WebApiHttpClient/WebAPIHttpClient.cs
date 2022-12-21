namespace WebAPIConsoleClient.WebAPIHttpClient
{
    internal static class WebAPIHttpClient
    {
        private static HttpClient client = new HttpClient();
        private const string _httpHost = "https://localhost:7122";
        private const string _categoryPath = "api/Categories";
        private const string _productPath = "api/Products";

        public static async Task GetAllProductsAsync()
        {
            Console.WriteLine("Get all products:");
            var response = await GetAllAsync($"{_httpHost}/{_categoryPath}");
            Console.WriteLine(response);
        }

        public static async Task GetAllCategoriesAsync()
        {
            Console.WriteLine("Get all categories:");
            var response = await GetAllAsync($"{_httpHost}/{_productPath}");
            Console.WriteLine(response);
        }

        private static async Task<string> GetAllAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return $"Something went wrong. Response code: {response.StatusCode}";
        }
    }
}