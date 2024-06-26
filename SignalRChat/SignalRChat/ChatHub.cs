using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Model;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        private readonly ChatDBContext dbContext;
        public ChatHub(ChatDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Send(string username, string message)
        {
            await Clients.All.SendAsync("AddMessage", username, message);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user != null)
            {
                var newMessage = new Messages
                {
                    Message = message,
                    UserId = user.Id,
                    User = user
                };
                dbContext.Message.Add(newMessage);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task Connect(string username)
        {
            var currentUser = await dbContext.Users.Include(i => i.Messages).FirstOrDefaultAsync(i => i.Name == username);
            if (currentUser == null)
            {
                var newUser = new User
                {
                    ConnectionId = Context.ConnectionId,
                    Name = username
                };
                dbContext.Users.Add(newUser);
            }
            else 
            { 
                currentUser.ConnectionId = Context.ConnectionId; 
            }

            await dbContext.SaveChangesAsync();

            var allUsers = await dbContext.Users.ToListAsync();
            var allUsersData = allUsers.Select(i => new {ConnectionId = i.ConnectionId, Name = i.Name, Messages = i.Messages?.Select(g => new { UserName = g.User?.Name, g.Message }).ToList()}).ToList();
            foreach (var user in allUsers)
            {
                if (user.ConnectionId == Context.ConnectionId)
                {
                    await Clients.Caller.SendAsync("AddMessages", user.Messages);
                }
            }
            await Clients.Caller.SendAsync("Connected", Context.ConnectionId, username, allUsersData);
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewUserConnected", Context.ConnectionId, username);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = dbContext.Users.FirstOrDefault(i => i.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId, user.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
