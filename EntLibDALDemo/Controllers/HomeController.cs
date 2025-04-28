using System.Diagnostics;
using EntLibDALDemo.Models;
using EntLibDALDemo.Util;
using Microsoft.AspNetCore.Mvc;

namespace EntLibDALDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DbHelper _dbHelper;
        public HomeController(ILogger<HomeController> logger, DbHelper dbHelper)
        {
            _logger = logger;

            _dbHelper = dbHelper;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel();

            string testSql = "SELECT version();";

            vm.QuerySingleValue = _dbHelper.TestPgQuery(testSql);

            return View(vm);
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
