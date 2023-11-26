using Microsoft.AspNetCore.Mvc;
using OLP_API.Data;
using SharedModels;
using System.Configuration;
using OLP_API.Common;

namespace OLP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly OLP_DbContext _context;

        public CourseController(OLP_DbContext context)
        {
            _context = context;
        }


        #region HTTP Request
        [HttpGet("Courses")]
		public IActionResult Courses(int Id_Category, int Page)
        {
            int skipCourses = Constants.MaxCoursesReturned * Page;
            List<Course> courses;

			try
            {
                if (Id_Category == 0)
                {
					// No filter by category
					courses = _context.Course.Skip(skipCourses).
											  Take(Constants.MaxCoursesReturned).
											  ToList();
				} else
                {
					courses = _context.Course.Where(x => x.Id_Category == Id_Category).
											  Skip(skipCourses).
											  Take(Constants.MaxCoursesReturned).
											  ToList();
				}
				
                if (!courses.Any()) return BadRequest("No hay cursos que coincidan con el filtro");
				return Ok(courses);
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? BadRequest(ex.InnerException.Message) : BadRequest(ex.Message);
            }
        }
		#endregion
	}
}
