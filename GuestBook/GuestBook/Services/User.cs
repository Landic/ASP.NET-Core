using GuestBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace GuestBook.Services
{
	public interface IUser
	{
		Task<User> GetLogin(string log);
		Task<User> GetFullname(string log);
		Task AddUser(RegisterModel user);
		Task<bool> IsUser();
		Task<bool> LoginSalt(LoginModel salt);
	}

	public class UserService : IUser
	{

		private readonly GuestBookContext dbContext;

		public UserService(GuestBookContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task AddUser(RegisterModel registerUser)
		{
			User user = new User();
			user.FullName = registerUser.Fullname;
			user.Login = registerUser.Login;
			byte[] saltbuf = new byte[16];
			RandomNumberGenerator rand = RandomNumberGenerator.Create();
			rand.GetBytes(saltbuf);
			StringBuilder str = new StringBuilder(16);
			for (int i = 0; i < 16; i++)
			{
				str.Append(string.Format("{0:X2}", saltbuf[i]));
			}
			string salt = str.ToString();
			byte[] password = Encoding.Unicode.GetBytes(salt + registerUser.Password);
			byte[] byteHash = SHA256.HashData(password);
			StringBuilder hash = new StringBuilder(byteHash.Length);
			for (int i = 0; i < byteHash.Length; i++)
			{
				hash.Append(string.Format("{0:X2}", byteHash[i]));
			}
			user.Password = hash.ToString();
			user.Salt = salt;
			dbContext.Users.Add(user);
			await dbContext.SaveChangesAsync();
		}

		public async Task<User> GetFullname(string log)
		{
			return await dbContext.Users.FirstOrDefaultAsync(i => i.Login == log);
		}

		public async Task<User> GetLogin(string log)
		{
			return await dbContext.Users.FirstOrDefaultAsync(i => i.Login == log);
		}

		public async Task<bool> IsUser()
		{
			return await dbContext.Users.AnyAsync();
		}

		public async Task<bool> LoginSalt(LoginModel logForm)
		{
			var user = await dbContext.Users.FirstOrDefaultAsync(i => i.Login == logForm.Login);
			string? salt = user.Salt;
			byte[] password = Encoding.Unicode.GetBytes(salt + logForm.Password);
			byte[] byteHash = SHA256.HashData(password);
			StringBuilder hash = new StringBuilder(byteHash.Length);
			for (int i = 0; i < byteHash.Length; i++)
			{
				hash.Append(string.Format("{0:X2}", byteHash[i]));
			}
			if (user.Password != hash.ToString())
			{
				return false;
			}
			return true;
		}
	}
}
