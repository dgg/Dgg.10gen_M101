using System;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public class UsersDb : IUsersDb
	{
		private readonly MongoDatabase _blogDb;
		public UsersDb()
		{
			var mongoUrl = new MongoUrl(ConfigurationManager.ConnectionStrings["mongo_local"].ConnectionString);
			_blogDb = MongoDatabase.Create(mongoUrl);
		}

		public bool Authenticate(Models.Login login)
		{
			bool authenticated = false;

			var users = _blogDb.GetCollection<BsonDocument>("users");

			var user = users.FindOne(Query.EQ("_id", login.UserName));
			if (user != null)
			{
				string hashed = new PasswordHasher().HashPassword(login.UserName, login.Password);
				authenticated = user["password"].AsString.Equals(hashed);
			}
			return authenticated;
		}

		public Guid StartSession(Models.Login login)
		{
			var sessions = _blogDb.GetCollection<BsonDocument>("sessions");
			Guid sessionId = Guid.NewGuid();
			sessions.Insert(new
			{
				_id = sessionId,
				userName = login.UserName
			});

			return sessionId;
		}

		public void EndSession(Guid sessionId)
		{
			var sessions = _blogDb.GetCollection<BsonDocument>("sessions");
			sessions.Remove(Query.EQ("_id", sessionId));
		}

		public string UserNameFromSession(Guid sessionId)
		{
			var sessions = _blogDb.GetCollection<BsonDocument>("sessions");
			var session = sessions.FindOne(Query.EQ("_id", sessionId));
			return session != null ? session["userName"].AsString : null;
		}

		public bool Signup(Models.Signup signup)
		{
			bool created = false;
			var users = _blogDb.GetCollection<BsonDocument>("users");
			try
			{
				string hashed = new PasswordHasher().HashPassword(signup.UserName, signup.Password);
				var result = users.Insert(new
				{
					_id = signup.UserName,
					password = hashed,
					email = signup.Email
				});
				created = !result.HasLastErrorMessage;
			}
			catch (MongoSafeModeException ex)
			{
				if (ex.CommandResult.Response["code"] != 11000) throw;
			}
			return created;
		}

		public string UserKey { get { return "userName"; } }
	}
}