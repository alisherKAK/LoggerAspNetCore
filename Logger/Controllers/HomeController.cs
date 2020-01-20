using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Logger.Models;

namespace Logger.Controllers
{
    public interface IMyClass{}
    public class MyClass
    {
        private readonly ILogger _logger;
        public MyClass(ILogger logger)
        {
            _logger = logger;
            _logger.LogWarning("MyClass warning");
        }
    }

    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private MyClass _myClass;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _myClass = new MyClass(logger);
        }

        public IActionResult Index()
        {
            _logger.LogTrace("Common log");
            _logger.LogInformation("Common log");
            _logger.LogDebug("Common log");
            _logger.LogWarning("Common log");
            _logger.LogError("Common log");
            _logger.LogCritical("Common log");
            return View();
        }

        public IActionResult Divide(string result)
        {
            ViewBag.Result = result;
            return View();
        }
        [HttpPost]
        public IActionResult Calculate(int num1, int num2)
        {
            string result;
            try
            {
                result = (num1 / num2).ToString();
                result = ((double)num1 / num2).ToString();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                result = e.Message;
            }
            
            return RedirectToAction("Divide", new { result = result });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
