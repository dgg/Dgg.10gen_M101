using System;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public interface IUsersDb
	{
		bool Authenticate(Models.Login login);
		bool Signup(Models.Signup signup);
		string UserKey { get; }
		Guid StartSession(Models.Login login);
		void EndSession(Guid sessionId);
		string UserNameFromSession(Guid sessionId);
	}
}