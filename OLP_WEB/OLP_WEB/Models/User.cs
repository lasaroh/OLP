using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace OLP_WEB.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Password2 { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}

    public class UserPassword
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string NewPassword2 { get; set; } = string.Empty;
    }
}
