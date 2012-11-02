using System;
using System.Security.Cryptography;
using System.Text;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public class PasswordHasher
	{
		public string CreateSalt(string userName)
		{
			var hasher = new Rfc2898DeriveBytes(userName, Encoding.UTF8.GetBytes("thisisasalt"), 10000);
			return Convert.ToBase64String(hasher.GetBytes(25));
		}

		public string HashPassword(string userName, string password)
		{
			string salt = CreateSalt(userName);
			var Hasher = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
			return Convert.ToBase64String(Hasher.GetBytes(25));
		}
	}
}