using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using MVC_Session_03_Demo.Services.EmailSender;
using MVC_Session_03_Demo.ViewModels.Account;
using MVC_Session_03_Demo.ViewModels.Account;
using Route.C41.G03.DAL.Models;
using System.Threading.Tasks;

namespace MVC_Session_03_Demo.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IEmailSender emailSender,
			IConfiguration configuration
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_configuration = configuration;
		}
		#region Sign Up
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Username);

				if (user is null)
				{
					user = new ApplicationUser
					{
						UserName = model.Username,
						Email = model.Email,
						IsAgree = model.IsAgree,
						FName = model.FName,
						LName = model.LName
					};

					var result = await _userManager.CreateAsync(user, model.Password);
					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}

				ModelState.AddModelError(string.Empty, "This UserName Is Already In Use For Another Account!");
			}

			return View(model);
		}
		#endregion


		#region Sign In

		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account Is Locked!");
						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");
						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed yet!");
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}

		#endregion


		#region Sign Out

		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		#endregion


		#region Forget Password
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
					var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = user.Email , token = resetPasswordToken}, "https");

					await _emailSender.SendAsync(
						from: _configuration["EmailSettings:SenderEmail"],
						recipients: model.Email,
						subject: "Reset Your Password",
						body: resetPasswordUrl);

					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "These Is No Account With This Email!");
			}
			return View(model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion


		#region Reset Password


		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
           return View();
        }

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;

				var user = await _userManager.FindByEmailAsync(email);

				if(user is not null)
				{
					await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					return RedirectToAction(nameof(SignIn));	
				}
				ModelState.AddModelError(string.Empty, "URL Is Not Valid");
            }
			return View(model);
        }

		#endregion
	}

}
