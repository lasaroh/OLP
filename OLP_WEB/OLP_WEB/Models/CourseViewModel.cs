using SharedModels;

namespace OLP_WEB.Models
{
	public class CourseViewModel
	{
		public int IdCategory { get; set; }
		public List<Course> Courses { get; set; } = new();
		public List<Category> Categories { get; set; } = new();
	}
}
