using Microsoft.AspNetCore.Mvc;
using GuestBook.Models;
using GuestBook.Services;

namespace GuestBook.Controllers
{
    public class GuestBookController : Controller
    {
        private readonly IUser userService;
        private readonly IMessage messageService;

        public GuestBookController(IUser userService, IMessage messageService)
        {
            this.userService = userService;
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("api/GuestBook/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userService.GetLogin(register.Login);
                if (existingUser != null)
                {
                    return BadRequest("Пользователь с таким логином уже существует.");
                }
                if (register.Login == "Guest")
                {
                    return BadRequest("Нельзя зарегистрироваться с таким логином");
                }
                await userService.AddUser(register);
                return Ok();
            }
            return BadRequest("Invalid data.");
        }

        [HttpPost("api/GuestBook/Continue")]
        public IActionResult Continue()
        {
            HttpContext.Session.SetString("Login", "Guest");
            return Ok();
        }

        [HttpPost("api/GuestBook/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginModel log)
        {
            if (ModelState.IsValid)
            {
                if (!await userService.IsUser())
                {
                    return BadRequest("Неверный логин или пароль!");
                }
                var users = await userService.GetLogin(log.Login);
                if (users == null)
                {
                    return BadRequest("Неверный логин или пароль!");
                }
                if (!await userService.LoginSalt(log))
                {
                    return BadRequest("Неверный логин или пароль!");
                }
                HttpContext.Session.SetString("Login", users.Login);
                HttpContext.Session.SetString("FullName", users.FullName);
                return Ok(new { fullName = users.FullName });
            }
            return BadRequest("Invalid data.");
        }

        [HttpPost("api/GuestBook/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok();
        }

        [HttpGet("api/GuestBook/Messages")]
        public async Task<IActionResult> Messages()
        {
            if (HttpContext.Session.GetString("Login") != null)
            {
                IEnumerable<Messages> mes = await messageService.GetMessage();
                return Ok(mes);
            }
            else
                return Unauthorized();
        }

        [HttpPost("api/GuestBook/Send")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send([FromBody] Messages Mes)
        {
            if (ModelState.IsValid)
            {
                var log = HttpContext.Session.GetString("Login");
                if (log != null)
                {
                    var user = await userService.GetLogin(log);
                    if (user != null)
                    {
                        await messageService.SendMessage(Mes, user);
                        return Ok();
                    }
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}
