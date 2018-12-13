using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BMS.Models;
using YK.Config.Model;
using YK.Platform.Core;
using YK.Platform.Core.Tools;
using YK.Platform.Core.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BMS.Controllers
{
  public class HomeController : Controller
  {
    ConfigContext ConfigContext = null;
    public HomeController(ConfigContext context, IConfiguration configuration, DbContextOptions<ConfigContext> options)
    {
      ConfigContext = context;
    }

    [HttpGet]
    public List<User> GetUsers()
    {
      ConfigContext = new ConfigContext();
      List<User> users = ConfigContext.User.ToList();
      return users;
    }

    public IActionResult Index()
    {
      EntityReflectionDataBase refEntity = new EntityReflectionDataBase();
      //var list = refEntity.GetEntitys();

      User user = EntityFactory.New<User>();
      user.UserName = "yank01";
      Framework<User>.Instance().Insert(user);

      var c = Framework<User>.Instance().Find(n => n.IsEnable == true);

      User userEf = new User();
      userEf.UserName = "yank02";

      ConfigContext = new ConfigContext();
      ConfigContext.User.Add(userEf);
      ConfigContext.SaveChanges();

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
