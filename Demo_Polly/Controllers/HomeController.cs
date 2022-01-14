using Demo_Polly.Models;
using Demo_Polly.Services;
using Microsoft.AspNetCore.Mvc;
using Polly;
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

        public async Task<IActionResult> ConsumeAPI()
        {
            try
            {
                int retries = 1;
                string response = "";

                var policyResult = await Policy.Handle<Exception>()
                    .WaitAndRetryAsync(
                    retryCount: 6,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
                    onRetry: (exception, timeSpan, context) =>
                    {
                        _logger.LogWarning($"Exception: {exception.Message}");
                        _logger.LogInformation($"Retries: {retries}");
                        retries++;
                    })
                    .ExecuteAndCaptureAsync(async () =>
                    {
                        response = apiConsumerService.ConsumeAPI();
                    });

                _logger.LogInformation($"Result: {policyResult.Outcome.ToString()}");

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

        public async Task<IActionResult> ReadFile()
        {
            try
            {
                int retries = 1;
                string response = "";

                var policyResult = await Policy.Handle<Exception>()
                    .WaitAndRetryAsync(
                    retryCount: 6,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
                    onRetry: (exception, timeSpan, context) =>
                    {
                        _logger.LogWarning($"Exception: {exception.Message}");
                        _logger.LogInformation($"Retries: {retries}");
                        retries++;
                    })
                    .ExecuteAndCaptureAsync(async () =>
                    {
                        response = fileReaderService.ReadFile();
                    });

                _logger.LogInformation($"Result: {policyResult.Outcome.ToString()}");

                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}