using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public class UserMapper : IUserMapper
	{
		private readonly IUsersDb _users;

		public UserMapper(IUsersDb users)
		{
			_users = users;
		}

		public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
		{
			if (context.CurrentUser != null) return context.CurrentUser;

			string userName = _users.UserNameFromSession(identifier);
			IEnumerable<string> claims = new[] { identifier.ToString() };
			return userName != null ? new UserIdentity { UserName = userName, Claims = claims } : null;
		}
	}
}