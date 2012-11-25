using Dgg._10gen_M101.Web.Models;

namespace Dgg._10gen_M101.Web.Infrastructure.Data
{
	public interface IBlogDb
	{
		void Create(Post post);
		Post Get(string permalink);
		void Create(Comment comment, string permalink);
	}
}