<Query Kind="Statements">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/test?safe=true"));
MongoCollection<BsonDocument> things = db.GetCollection("things");

things.Drop();
IMongoQuery apple = Query.EQ("thing", "apple"), pear = Query.EQ("thing", "pear");
things.Update(apple, Update.Set("color", "red"), UpdateFlags.Upsert);
things.Update(pear, Update.Replace(new{color = "green"}), UpdateFlags.Upsert);

things.FindOne(apple).Dump("apple", 2);
things.FindOne(pear).Dump("pear", 2);

things.FindAll()
	.Dump("upserted things", 2);