using System;
using MongoDB.Bson;

namespace Dgg._10gen_M101.Web.Models
{
	public class Post
	{
		public BsonObjectId Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Body { get; set; }
		public string Permalink { get; set; }
		public string[] Tags { get; set; }
		public Comment[] Comments { get; set; }
		public DateTime Date { get; set; }

		public static Post New(NewPost post, string author)
		{
			return new Post
			{
				Title = post.Title,
				Author = author,
				Body = post.Bodify(),
				Tags = post.Taggify(),
				Date = DateTime.UtcNow,
				Permalink = post.GeneratePermaLink()
			};
		}

		public string Taggify()
		{
			return string.Join(", ", Tags ?? new string[0]);
		}
	}
}