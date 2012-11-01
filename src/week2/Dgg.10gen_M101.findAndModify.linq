<Query Kind="Program">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

void Main()
{
	nextSequenceNumber("uid").Dump();
	nextSequenceNumber("uid").Dump();
	nextSequenceNumber("uid").Dump();
	
	nextSequenceNumber("other").Dump();
	nextSequenceNumber("other").Dump();
}

// Define other methods and classes here

private int nextSequenceNumber(string name)
{
	MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/test?safe=true"));
	MongoCollection<BsonDocument> counters = db.GetCollection("counters");
	
	var counter = counters.FindAndModify(
		Query.EQ("type", name),
		SortBy.Null,
		Update.Inc("value", 1),
		returnNew : true,
		upsert: true);
	
	int counterValue = counter.ModifiedDocument["value"].AsInt32;
	return counterValue;
}
