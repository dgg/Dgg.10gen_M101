using System;
using Dgg._10gen_M101.Web.Models;

namespace Dgg._10gen_M101.Web.Infrastructure.Data
{
	public interface IBlogDb {
		string Create(NewPost post, string author, DateTime utcNow);
	}
}