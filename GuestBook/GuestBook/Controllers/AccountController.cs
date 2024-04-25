using GuestBook.Models;
using GuestBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUser userService;

		public AccountController(IUser userService)
		{
			this.userService = userService;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel register)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await userService.GetLogin(register.Login);
				if (existingUser != null)
				{
					ModelState.AddModelError("Login", "Пользователь с таким логином уже существует.");
					return View(register);
				}
				if(register.Login == "Guest")
				{
					ModelState.AddModelError("Login", "Нельзя зарегистрироваться с таким логином");
					return View(register);
				}
				await userService.AddUser(register);
                return RedirectToAction("Login");
            }
			return View(register);
		}


        public IActionResult Continue()
        {
            HttpContext.Session.SetString("Login", "Guest");
			return RedirectToAction("Message", "Message");
        }

        public ActionResult Login()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel log)
		{
			if (ModelState.IsValid)
			{
				if(!await userService.IsUser())
				{
					ModelState.AddModelError("", "Неверный логин или пароль!");
					return View(log);
				}
				var users = await userService.GetLogin(log.Login);
				if(users == null)
				{
                    ModelState.AddModelError("", "Неверный логин или пароль!");
                    return View(log);
                }
				if(!await userService.LoginSalt(log))
				{
                    ModelState.AddModelError("", "Неверный логин или пароль!");
					return View(log);
                }
				HttpContext.Session.SetString("Login", users.Login);
				HttpContext.Session.SetString("FullName", users.FullName);
				return RedirectToAction("Message", "Message");
			}
			return View(log);
		}
	}
}
