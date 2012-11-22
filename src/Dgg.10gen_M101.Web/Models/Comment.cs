using MongoDB.Bson;

namespace Dgg._10gen_M101.Web.Models
{
	public class Comment
	{
		public BsonObjectId Id { get; set; }
		public string Author { get; set; }
		public string Body { get; set; }
	}
}