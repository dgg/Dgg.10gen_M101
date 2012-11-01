using System.Collections.Generic;
using Nancy.Security;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public class UserIdentity : IUserIdentity
	{
		public string UserName { get; set; }
		public IEnumerable<string> Claims { get; set; }
	}
}