using System.Drawing;

namespace OLP_WEB.Common
{
	public class Routes
	{
		#region HomeController
		public const string HomeControllerName = "Home";

		public const string HomeActionIndex = "Index";
		public const string HomeActionHistory = "History";
		public const string HomeActionShop = "Shop";
		public const string HomeActionPrivacyPolicy = "PrivacyPolicy";
		public const string HomeActionTermsAndConditionsOfUse = "TermsAndConditionsOfUse";
		#endregion

		#region UserController
		public const string UserControllerName = "User";

		public const string UserActionSignUp = "SignUp";
		public const string UserActionLogIn = "LogIn";
		public const string UserActionProfile = "Profile";
		#endregion

		#region CourseController
		public const string CourseControllerCame = "Course";
		
		public const string CourseActionIndex = "Index";
		#endregion
	}
}
