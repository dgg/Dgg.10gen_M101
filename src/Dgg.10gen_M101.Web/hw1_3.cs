using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Nancy;

namespace Dgg._10gen_M101.Web
{
	public class hw1_3 : NancyModule
	{
		public hw1_3()
		{
			Get["/hw1/{n}"] = param =>
			{
				MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/m101?safe=true"));
				int number = param.n;
				MongoCollection<BsonDocument> funnyNumbers = db.GetCollection("funnynumbers");
				var cursor = funnyNumbers.FindAll().SetSkip(number).SetLimit(1).SetSortOrder(SortBy.Ascending("value"));
				return value(cursor.Single()) + "\n";
			};
		}

		private int value(BsonDocument item)
		{
			return (int)item["value"].AsDouble;
		}
	}
}