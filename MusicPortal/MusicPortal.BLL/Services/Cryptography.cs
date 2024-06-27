using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.Services {
    public interface ICryptographyService {
        string HashPassword(string password, string salt);
        string GenerateSalt();
    }
    public class CryptographyService : ICryptographyService {
        public string HashPassword(string password, string salt) {
            byte[] saltBytes = Convert.FromBase64String(salt), passwordBytes = Encoding.UTF8.GetBytes(password), saltedPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];
            Array.Copy(saltBytes, saltedPasswordBytes, saltBytes.Length);
            Array.Copy(passwordBytes, 0, saltedPasswordBytes, saltBytes.Length, passwordBytes.Length);

            using (var sha256 = SHA256.Create()) {
                byte[] hashedPasswordBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }
        public string GenerateSalt() {
            byte[] saltBytes = new byte[16];
            using (var rngCsp = new RNGCryptoServiceProvider()) rngCsp.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}
