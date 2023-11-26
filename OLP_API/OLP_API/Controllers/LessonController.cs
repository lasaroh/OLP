using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLP_API.Data;
using OLP_API.Services;
using OLP_WEB.Models;
using SharedModels;
using System.Linq;

namespace OLP_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LessonController : ControllerBase
	{
		private readonly OLP_DbContext _context;
		private readonly AmazonS3Service _amazonS3Service;

		public LessonController(OLP_DbContext context)
		{
			_context = context;
			_amazonS3Service = new();
		}

		#region HTTP Request
		[HttpGet("LessonsByCourse")]
		public async Task<IActionResult> LessonsByCourse(int IdCourse)
		{
			Course? course = await _context.Course.Where(x => x.Id == IdCourse).FirstOrDefaultAsync();
			List<Lesson> lessons = await _context.Lesson.Where(x => x.Id_Course == IdCourse).
														 OrderBy(x => x.Order_Number).
														 ToListAsync();


			if (course == null) return NotFound("Curso no encontrado");
			if (!lessons.Any()) return NotFound("Lecciones no encontrado");


			CourseLessonsViewModel model = new()
			{
				Course = course,
				Lessons = lessons
			};
			return Ok(model);
		}

		[HttpGet("LessonSelected")]
		[Produces("text/plain")]
		public IActionResult LessonSelected(int IdCourse, int OrderLesson)
		{
			string url = _amazonS3Service.GetLesson(IdCourse, OrderLesson);
			return Ok(url);
		}
		#endregion
	}
}
