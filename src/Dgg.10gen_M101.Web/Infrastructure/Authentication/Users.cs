using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Dgg._10gen_M101.Web.Infrastructure.Authentication
{
	public interface IUsersDb
	{
		bool Authenticate(Models.Login login);
		bool Signup(Models.Signup signup);
	}

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
	}
}