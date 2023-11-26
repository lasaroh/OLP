using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
	public class Course
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int Id_User { get; set; }
		public int Id_Category { get; set; }
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
	}

	public class CourseFilter
	{
		public int IdCategory { get; set; }
		public int Page { get; set; }
	}
}
