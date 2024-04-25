using System.ComponentModel.DataAnnotations;

namespace MVC_Session_03_Demo.ViewModels.Account
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "New Password Is Required!")]
		[MinLength(5, ErrorMessage ="Minimum Password Length Is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required!")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match with New Password!")]
		public string ConfirmPassword { get; set; }
	}
}
