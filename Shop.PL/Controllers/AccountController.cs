using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.DAL.Entites.Identity;
using Shop.PL.Models.Identity;

namespace Shop.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region SignUp
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var User = await _userManager.FindByNameAsync(model.UserName);
			if (User == null)
			{
				User = new ApplicationUser()
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email,
					IsAgree = model.IaAgreee,
					UserName=model.UserName,
					

				};
				var result = await _userManager.CreateAsync(User,model.Password);
				if (result.Succeeded)
					return RedirectToAction("SignIn");
				foreach (var error in result.Errors)
					ModelState.AddModelError("", error.Description);
			}
			ModelState.AddModelError(nameof(SignUpViewModel.UserName), "User Name Is Alredy Used");
			return View(model);

		}
		#endregion

		#region SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task< IActionResult> SignIn(SignInViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var Email=await _userManager.FindByEmailAsync(model.Email);
			if(Email is not null)
			{
				var CheckPassword = await _userManager.CheckPasswordAsync(Email,model.Password);
				if (CheckPassword)
				{
					var UserSignIn = await _signInManager.PasswordSignInAsync(Email, model.Password, model.RemeberMe, true);
					if(UserSignIn.IsLockedOut)
						ModelState.AddModelError(string.Empty, "Account Is Not Confirmrd Yet!!");
					if (UserSignIn.IsLockedOut)
						ModelState.AddModelError(string.Empty, "Account Is Loked!!");
					if (UserSignIn.Succeeded)
						return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Email Or Password Is InCorrect");
				return View(model);
			}
			ModelState.AddModelError("", "Email Is Not Found");

			return View(model);
		
		}
		#endregion

	}
}
