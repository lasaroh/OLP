using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}

	public class UserSignUp
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
		public string NewPassword2 { get; set; } = string.Empty; // Only used by client
	}
}
