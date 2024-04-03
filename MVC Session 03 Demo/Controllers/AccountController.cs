using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Session_03_Demo.ViewModels.User;
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
	}
}
