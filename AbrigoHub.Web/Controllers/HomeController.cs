using AbrigoHub.Web.Models;
using AbrigoHub.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbrigoHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Por padrão, vamos mostrar o clima de São Paulo
                var weather = await _weatherService.GetWeatherAsync("São Paulo");
                return View(weather);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados do clima");
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
} 