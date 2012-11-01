<Query Kind="Statements">
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

MongoDatabase testDb = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/school"));

MongoCollection<BsonDocument> scores = testDb.GetCollection("scores");

var rangedExams = scores
	.Find(Query.And(
			Query.EQ("type", "exam"),
			Query.GT("score", 50),
			Query.LT("score", 70)))
	.Dump("projected exams", 2);