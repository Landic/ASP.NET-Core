using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.DTO;
using MusicPortal.Models;
using MusicPortal.Services;
using MusicPortal.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPortal.Controllers
{
    [Culture]
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICryptographyService _cryptoService;

        public RegistrationController(IUserService userService, ICryptographyService cryptoService)
        {
            _userService = userService;
            _cryptoService = cryptoService;
        }

        public IActionResult Register() 
        { 
            return View("~/Views/Music/Register.cshtml"); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register reg)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.IsLoginTaken(reg.Login!))
                {
                    ModelState.AddModelError("Login", "Пользователь с таким логином уже существует!");
                    return View("~/Views/Music/Register.cshtml", reg);
                }

                else if (reg.Password!.Length < 6)
                {
                    ModelState.AddModelError("Password", "Пароль должен содержать не менее 6 символов!");
                    return View("~/Views/Music/Register.cshtml", reg);
                }

                else if (!(reg.Password.Any(char.IsUpper) && reg.Password.Any(char.IsDigit)))
                {
                    ModelState.AddModelError("Password", "Пароль должен содержать хотя бы одну заглавную букву и одну цифру!");
                    return View("~/Views/Music/Register.cshtml", reg);
                }

                string salt = _cryptoService.GenerateSalt();
                string hashedPassword = _cryptoService.HashPassword(reg.Password, salt);

                var user = new UserDTO
                {
                    Login = reg.Login,
                    FullName = reg.FullName,
                    Password = hashedPassword,
                    Salt = salt,
                };
                if (reg.Login == "Admin")
                {
                    user.IsAuthorized = true;
                }
                await _userService.AddUser(user);
                await _userService.Save();

                HttpContext.Session.SetString("Authorization", user.IsAuthorized.ToString());
                HttpContext.Session.SetString("Login", user.Login!);
                return Redirect("/Music/Index");
            }
            return View("~/Views/Music/Register.cshtml", reg);
        }

        public IActionResult ToLogin() 
        {
            return Redirect("/Authorization/Login"); 
        }
    }
}
