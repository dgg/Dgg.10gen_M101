namespace Dgg._10gen_M101.Web.Models
{
	public class Comment
	{
		public string Author { get; set; }
		public string Email { get; set; }
		public string Body { get; set; }

		public static Comment New(NewComment comment)
		{
			return new Comment
			{
				Author = comment.Name,
				Email = comment.Email,
				Body = comment.Body
			};
		}
	}
}