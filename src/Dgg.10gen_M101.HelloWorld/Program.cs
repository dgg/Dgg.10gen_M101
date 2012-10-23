using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dgg._10gen_M101.HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			MongoDatabase testDb = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/test"));

			MongoCollection<BsonDocument> names = testDb.GetCollection("names");
			foreach (var name in names.FindAll())
			{
				Console.WriteLine(name);
			}

			Console.WriteLine("...");
			Console.ReadLine();
		}
	}
}
