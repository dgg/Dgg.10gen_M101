<Query Kind="Statements">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
</Query>

MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/school"));

MongoCollection<BsonDocument> scores = db.GetCollection("scores");

scores.FindAll()
	.SetSortOrder(SortBy.Ascending("student_id").Descending("score"))
	/*.SetSkip(3)
	.SetLimit(2)*/
	.Dump("limited scores", 2);