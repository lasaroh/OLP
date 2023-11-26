using SharedModels;

namespace OLP_WEB.Models
{
	public class CourseLessonsViewModel
	{
		public Course Course { get; set; } = new();
		public List<Lesson> Lessons { get; set; } = new();
	}
}
