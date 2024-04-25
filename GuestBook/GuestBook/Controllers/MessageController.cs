using GuestBook.Models;
using GuestBook.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace GuestBook.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessage messageService;
        private readonly IUser userService;

        public MessageController(IMessage messageService, IUser userService)
        {
            this.messageService = messageService;
            this.userService = userService;
		}

        public async Task<IActionResult> Message()
        {
            if (HttpContext.Session.GetString("Login") != null)
            {
				IEnumerable<Messages> mes = await messageService.GetMessage();
				ViewBag.Message = mes;
				return View();
			}
            else
                return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(Messages Mes)
        {
            if (ModelState.IsValid)
            {
                var log = HttpContext.Session.GetString("Login");
                if (log != null)
                {
					var user = await userService.GetLogin(log);
                    if(user != null)
                    {
						await messageService.SendMessage(Mes,user);
						return RedirectToAction("Message");
					}
				}
            }
			return RedirectToAction("Message");
		}
    }
}
