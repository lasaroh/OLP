﻿namespace OLP_API.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}

	public class UserPassword
	{
		public int Id { get; set; }
		public string CurrentPassword { get; set; } = string.Empty;
		public string NewPassword { get; set; } = string.Empty;
	}
}
