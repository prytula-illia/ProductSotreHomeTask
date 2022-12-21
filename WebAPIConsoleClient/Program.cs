// See https://aka.ms/new-console-template for more information
using WebAPIConsoleClient.WebAPIHttpClient;

await WebAPIHttpClient.GetAllProductsAsync();
await WebAPIHttpClient.GetAllCategoriesAsync();

Console.WriteLine("End of execution");