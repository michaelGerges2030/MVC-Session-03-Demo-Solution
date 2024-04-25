using System.ComponentModel.DataAnnotations;

namespace MVC_Session_03_Demo.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required!")]
		[EmailAddress(ErrorMessage = "Invalid Email!")]
		public string Email { get; set; }
	}
}
