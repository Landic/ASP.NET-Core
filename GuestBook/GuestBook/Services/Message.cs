using GuestBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestBook.Services
{
	public interface IMessage
	{
		Task<List<Messages>> GetMessage();
		Task SendMessage(Messages mes, User user);
	}

	public class MessageService : IMessage
	{
		private readonly GuestBookContext dbContext;

		public MessageService(GuestBookContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<List<Messages>> GetMessage()
		{
			return await dbContext.Messages.Include(i=>i.User).ToListAsync();
		}

		public async Task SendMessage(Messages mes, User user)
		{
			mes!.User = user;
			mes.Message_Date = DateTime.Now;
			dbContext.Add(mes);
			await dbContext.SaveChangesAsync();
		}
	}
}
