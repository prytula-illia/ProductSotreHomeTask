using DAL.Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductSotre.Models;
using Serilog;

namespace ProductSotre.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if(exception is EntityNotFoundException)
            {
                return NotFound();
            }

            var errorMessage = exception?.Message;
            Log.Error($"ERROR! Exception message: {errorMessage}");
            var error = new Error()
            {
                ExceptionMessage = $"Please, check log files for more information following this path: {AppDomain.CurrentDomain.BaseDirectory}logs\\log.txt"
            };
            return View(error);
        }
    }
}