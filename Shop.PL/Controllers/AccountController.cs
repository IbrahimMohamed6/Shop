using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Shop.DAL.Entites.Identity;
using Shop.PL.Helper;
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


		#region Log Out
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		#region Forget Password
		[HttpGet]		
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if(User is not null)
				{
					var Token= await _userManager.GeneratePasswordResetTokenAsync(User);
					var EmailUrl= Url.Action("ResetPassword","Account",new {Email=model.Email,Token=Token},Request.Scheme);
					var email = new DAL.Entites.Helper.Email
                    {
						To = model.Email,
						Subject = "Rset Password",
						Body = EmailUrl
					};
					EmailSetting.SendEmail(email);
					return RedirectToAction(nameof(CheckYorInbox));
				}
				
			}
			ModelState.AddModelError("", "User Name Not Exist");
				return View(model);
		}
		#endregion

		#region Check Password
		public IActionResult CheckYorInbox()
		{
			return View();
		}
		#endregion

		#region Reset Password
		public IActionResult Resetpassword(string Email,string Token)
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Resetpassword(ResetpasswordViewModel model)
        {
			if(ModelState.IsValid)
			{
				var User=await _userManager.FindByEmailAsync(model.Email); 
				if(User is not null)
				{
					var ResetPassword = await _userManager.ResetPasswordAsync(User, model.Token, model.Password);
					if(ResetPassword.Succeeded)
						return RedirectToAction(nameof(SignIn));
					foreach (var error in ResetPassword.Errors)
						ModelState.AddModelError("", error.Description);

				}
			}
			ModelState.AddModelError("", "Reset Password Is Valid");
			return View(model);
			
            
        }

        #endregion



    }
}
