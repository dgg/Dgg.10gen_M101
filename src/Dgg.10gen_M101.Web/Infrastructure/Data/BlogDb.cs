using System.Configuration;
using Dgg._10gen_M101.Web.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Dgg._10gen_M101.Web.Infrastructure.Data
{
	public class BlogDb : IBlogDb
	{
		private readonly MongoDatabase _blogDb;
		public BlogDb()
		{
			var mongoUrl = new MongoUrl(ConfigurationManager.ConnectionStrings["mongo_local"].ConnectionString);
			_blogDb = MongoDatabase.Create(mongoUrl);
		}

		public void Create(Post post)
		{
			var posts = _blogDb.GetCollection<Post>("posts");
			posts.Insert(post);
		}

		public Post Get(string permalink)
		{
			var posts = _blogDb.GetCollection<Post>("posts");
			return posts.FindOne(Query<Post>.EQ(p => p.Permalink, permalink));
		}
	}
}