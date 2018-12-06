using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BMS.Models;
using YK.Config.Model;
using YK.Platform.Core;

namespace BMS.Controllers
{
    public class HomeController : Controller
    {
        ConfigContext ConfigContext = null;
        public HomeController(ConfigContext context) {
            ConfigContext = context;
        }

        public IActionResult Index()
        {
            var c =  Framework<User>.Instance().Find(n => n.IsEnable == true);
            int count = ConfigContext.Modules.Count();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
