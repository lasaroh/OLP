using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OLP_WEB.Common;
using OLP_WEB.Models;
using SharedModels;
using System.Net;

namespace OLP_WEB.Controllers
{
	public class CourseController : Controller
	{
		private readonly HttpClient client_api;
		public CourseController()
		{
			client_api = new()
			{
				BaseAddress = new Uri("https://localhost:7210/")
			};
		}

		public async Task<IActionResult> Index()
		{
			if (!Request.Cookies.ContainsKey(Constants.NameUserSessionCookie))
			{
				return RedirectToAction(Routes.UserActionLogIn, Routes.UserControllerName);
			}
			else
			{
				try
				{
					CourseViewModel courseViewModel = new();
					List<Course> courses = new();

					// GET CATEGORIES
					using HttpResponseMessage requestCategories = await client_api.GetAsync("api/Category");
					requestCategories.EnsureSuccessStatusCode();
					List<Category> categories = await requestCategories.Content.ReadAsAsync<List<Category>>();

					// GET COURSES
					string parameters = "Id_Category=0&Page=0";
					using HttpResponseMessage result = await client_api.GetAsync("api/Course/Courses?" + parameters);
					if (result.StatusCode == HttpStatusCode.OK)
					{
						courses = await result.Content.ReadAsAsync<List<Course>>();
					}
					else
					{
						ViewBag.msgErr = await result.Content.ReadAsStringAsync();
					}

					courseViewModel.Categories = categories;
					courseViewModel.Courses = courses;
					return View(courseViewModel);
				}
				catch (Exception ex)
				{
					ViewBag.msgErr = ex.Message;
					return View();
				}
			}
		}
	}
}
