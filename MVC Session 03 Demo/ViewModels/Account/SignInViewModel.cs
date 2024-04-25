using System.ComponentModel.DataAnnotations;

namespace MVC_Session_03_Demo.ViewModels.Account
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email Is Required!")]
		[EmailAddress(ErrorMessage = "Invalid Email!")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password Is Required!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
