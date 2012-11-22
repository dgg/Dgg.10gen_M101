using System;
using System.Configuration;
using Dgg._10gen_M101.Web.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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

		public string Create(NewPost post, string author, DateTime utcNow)
		{
			string permaLink = post.GeneratePermaLink();
			var doc = new BsonDocument
			{
				{"title", post.Title},
				{"author", author},
				{"body", post.Bodify()}, 
				{"permalink", permaLink},
				{"tags", new BsonArray(post.Taggify())},
				{"date", utcNow}
			};

			var posts = _blogDb.GetCollection<BsonDocument>("posts");
			posts.Insert(doc);
			return permaLink;
		}
	}
}