using Demo_Polly.Models;
using Demo_Polly.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo_Polly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiConsumerService apiConsumerService;
        private readonly FileReaderService fileReaderService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            apiConsumerService = new();
            fileReaderService = new();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ConsumeAPI()
        {
            return Json(apiConsumerService.ConsumeAPI());
        }

        public IActionResult ReadFile()
        {
            return Json(fileReaderService.ReadFile());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}