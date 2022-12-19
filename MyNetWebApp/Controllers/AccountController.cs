using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyNetWebApp.ViewModels;
using MyNetWebApp.Models;
using MyNetWebApp.Data;

namespace MyNetWebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ApplicationDbContext _applicationDbContext;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext applicationDbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_applicationDbContext = applicationDbContext;
		}

		public IActionResult Login()
		{
			var response = new LoginViewModel();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);
			// check user
			var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
			if(user != null)
			{
				// when user is founded, check password
				bool passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
				if (passwordCheck)
				{
					// when password checking is successed, login
					var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,false,false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index","Race");
					}
				}
				// password is incorrected
				TempData["Error"] = "Wrong credentials, please try again!";
				return View(loginViewModel);
			}
			// user is not founded
            TempData["Error"] = "Wrong credentials, please try again!";
            return View(loginViewModel);

        }
	}
}

