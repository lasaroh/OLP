using System.ComponentModel.DataAnnotations;

namespace OLP_WEB.Models
{
	public class User
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Password2 { get; set; }
		public string? Phone { get; set; }
	}
}
