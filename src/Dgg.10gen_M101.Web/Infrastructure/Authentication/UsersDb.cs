using System;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public class UsersDb : IUserMapper
	{
		private readonly IUsers _users;

		public UsersDb(IUsers users)
		{
			_users = users;
		}

		public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
		{
			return context.CurrentUser ?? new UserIdentity { UserName = "userName" };
		}

		public static bool Authenticate(Models.Login login)
		{
			return login.UserName.Equals("DGG", StringComparison.OrdinalIgnoreCase);
		}

	}
}