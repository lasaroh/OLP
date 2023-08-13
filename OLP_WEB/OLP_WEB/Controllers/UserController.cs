using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using OLP_WEB.Common;
using OLP_WEB.Models;

namespace OLP_WEB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult SignUp()
        {
			ViewBag.msgErr = TempData[Constants.TempData_msgErr];
			if (Request.Cookies.ContainsKey(Constants.nameUserSessionCookie))
            {
                return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
            } else
            {
                return View();
            }
        }

        public IActionResult LogIn()
        {
			ViewBag.msgErr = TempData[Constants.TempData_msgErr];
			if (Request.Cookies.ContainsKey(Constants.nameUserSessionCookie))
            {
                return RedirectToAction(Routes.UserActionProfile, Routes.UserControllerName);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Profile(ProfileViewModel profileViewModel)
        {
			ViewBag.msgErr = TempData[Constants.TempData_msgErr];
			return View(profileViewModel);
        }

        public IActionResult CloseSession()
        {
            CloseSessionCookie(Response);
            return RedirectToAction(Routes.HomeActionIndex, Routes.HomeControllerName);
        }

        #region "Manage cookies"
        public static void SaveSesionCookie(User user, HttpResponse response)
        {
            response.Cookies.Append(Constants.nameUserSessionCookie, user.ToJson());   
        }
        public static void CloseSessionCookie(HttpResponse response)
        {
            response.Cookies.Delete(Constants.nameUserSessionCookie);
        }
        #endregion
    }
}
