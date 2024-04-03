using System.ComponentModel.DataAnnotations;

namespace MVC_Session_03_Demo.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="Email Is Required!")]
		[EmailAddress(ErrorMessage ="Invalid Email!")]
        public string Email { get; set; }

		[Required(ErrorMessage = "User Name Is Required!")]
		public string Username { get; set; }

		[Required(ErrorMessage = "First Name Is Required!")]
		[Display(Name = "First Name")]
		public string FName { get; set; }


		[Required(ErrorMessage = "Last Name Is Required!")]
		[Display(Name = "Last Name")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Password Is Required!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't match with Password!")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
    }
}
