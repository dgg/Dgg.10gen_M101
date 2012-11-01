<Query Kind="Program">
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
</Query>

void Main()
{
	MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/m101?safe=true"));
	
	MongoCollection<BsonDocument> funnyNumbers = db.GetCollection("funnynumbers");
	
	int magic = 0;
	try
	{
		var cursor = funnyNumbers.FindAll();
		/*foreach (var item in cursor)
		{
			if ((value(item) % 3) == 0) magic = magic + value(item);
		}*/
		magic = cursor.Select(i => value(i)).Where(n => n % 3 == 0).Sum();
	}
	catch(Exception ex)
	{
		string error = string.Format("Error trying to read collection: {0}", ex);
		error.Dump();
	}
	
	string.Format("The answer to Homework One, Problem 2 is {0}", magic).Dump();
}

// Define other methods and classes here
private int value(BsonDocument item)
{
	return (int)item["value"].AsDouble;
}
