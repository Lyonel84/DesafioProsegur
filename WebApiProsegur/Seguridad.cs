using System.Security.Cryptography;
using System.Text;

namespace WebApiProsegur
{
    public class Seguridad
    {
        public string GetHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256
                        .ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter
                     .ToString(hashedBytes).ToLower();
            }
        }
    }
}
