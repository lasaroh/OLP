using SharedModels;

namespace OLP_WEB.Models
{
	public class ProfileViewModel
	{
		public User User { get; set; } = new User();
		public UserPassword UserPassword { get; set; } = new UserPassword();
	}
}
