using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using OLP_WEB.Models;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace OLP_WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }

		public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult TermsAndConditionsOfUse()
        {
            return View();
        }
        /// <summary>
        /// Handles unhandled errors in the application
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}