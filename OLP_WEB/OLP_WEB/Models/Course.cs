namespace OLP_WEB.Models
{
	public class Course
	{
		public int Id { get; set; }
		public int Id_User { get; set; }
		public int Id_Category { get; set; }
		public int Id_Subcategory { get; set; }
		public string Description { get; set; } = string.Empty;
		public float Price { get; set; }
	}
}
