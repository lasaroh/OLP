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
		//todo lvarela Ver si cambiar el string para traer el video etc
		//public string File_Path { get; set; } = string.Empty;
		//public string File_Video_Path { get; set; } = string.Empty;
	}
}
