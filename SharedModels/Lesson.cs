using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
	public class Lesson
	{
		public int Id { get; set; }
		public int Id_Course { get; set; }
		public int Order_Number { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
