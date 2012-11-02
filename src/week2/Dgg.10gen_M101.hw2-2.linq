<Query Kind="Program">
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Bson.dll</Reference>
  <Reference Relative="..\..\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll">D:\projects\Dgg.10gen_M101\packages\mongocsharpdriver.1.6\lib\net35\MongoDB.Driver.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>MongoDB.Bson</Namespace>
  <Namespace>MongoDB.Driver</Namespace>
  <Namespace>MongoDB.Driver.Builders</Namespace>
  <Namespace>MongoDB.Driver.Linq</Namespace>
  <Namespace>MongoDB.Bson.Serialization.Attributes</Namespace>
</Query>

void Main()
{
	MongoDatabase db = MongoDatabase.Create(new MongoUrl("mongodb://localhost:27017/students?safe=true"));

	var grades = db.GetCollection<Grade>("grades");

	grades
		.AsQueryable()
		.Where(g => g.Type == "homework")
		// done in memory as .GroupBy not supported in linq yet
		.ToArray()
		.GroupBy(s => s.StudentId)
		.ForEach(student =>
		{
			Grade lowest = student.Select(s => s).MinBy(g => g.Score);
			grades.Remove(Query<Grade>.EQ(g => g.Id, lowest.Id));
		});
}

// Define other methods and classes here
public class Grade
{
	[BsonIdAttribute]
	public BsonObjectId Id {get; set;}
	[BsonElement("student_id")]
	public int StudentId { get; set; }
	[BsonElement("type")]
	public string Type { get; set; }
	[BsonElement("score")]
	public double Score { get; set; }
}

public static class MoreEnumerable
{
	public static  void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (var s in source) action(s);
	}

	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
			Func<TSource, TKey> selector)
	{
		return source.MinBy(selector, Comparer<TKey>.Default);
	}
	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
		   Func<TSource, TKey> selector, IComparer<TKey> comparer)
	{
		using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
		{
			if (!sourceIterator.MoveNext())
			{
				throw new InvalidOperationException("Sequence was empty");
			}
			TSource min = sourceIterator.Current;
			TKey minKey = selector(min);
			while (sourceIterator.MoveNext())
			{
				TSource candidate = sourceIterator.Current;
				TKey candidateProjected = selector(candidate);
				if (comparer.Compare(candidateProjected, minKey) < 0)
				{
					min = candidate;
					minKey = candidateProjected;
				}
			}
			return min;
		}
	}
}