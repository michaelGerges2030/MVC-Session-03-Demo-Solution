using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
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
		}
		#endregion
	}
