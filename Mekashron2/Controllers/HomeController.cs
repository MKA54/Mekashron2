using Mekashron2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Xml.Linq;
using System;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.IdentityModel.Xml;
using ServiceMekachronLogin;
using Newtonsoft.Json;

namespace Mekashron2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Index(string UserName, string Password)
        {
            var client = new ICUTechClient();
            var responseString = client.LoginAsync(UserName, Password, "").Result.@return;

            var user = JsonConvert.DeserializeObject<User>(responseString);

            return user.FirstName == null ? View("Error") : View("SuccessfulResponse", user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}