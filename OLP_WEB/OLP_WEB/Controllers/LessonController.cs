using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using OLP_WEB.Common;
using OLP_WEB.Models;
using SharedModels;
using System.Net;
using System.Net.Http.Headers;

namespace OLP_WEB.Controllers
{
	public class LessonController : Controller
	{
		private readonly HttpClient client_api;
		public LessonController()
		{
			client_api = new()
			{
				BaseAddress = new Uri("https://localhost:7210/")
			};
		}

		public async Task<IActionResult> LessonSelected(int IdCourse, int OrderLesson, string LessonName)
		{
			try
			{
				string parameters = $"IdCourse={IdCourse}&OrderLesson=" + OrderLesson;
				using HttpResponseMessage result = await client_api.GetAsync("api/Lesson/LessonSelected?" + parameters);
				result.EnsureSuccessStatusCode();
				string video_url = await result.Content.ReadAsStringAsync();
				return View("LessonSelected", (IdCourse, LessonName, video_url));
			}
			catch (Exception ex)
			{
				ViewBag.msgErr = ex.Message;
				return RedirectToAction(Routes.LessonActionLessonSelected, Routes.LessonControllerName);
			}
		}
	}
}
