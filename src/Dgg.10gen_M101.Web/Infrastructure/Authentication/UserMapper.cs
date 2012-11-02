using System;
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
			return context.CurrentUser ?? new UserIdentity { UserName = "userName" };
		}
	}
}