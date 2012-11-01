using Nancy;
using Nancy.ModelBinding;

namespace Dgg._10gen_M101.Web
{
	public class BlogModule : NancyModule
	{
		public BlogModule()
		{
			Get["/"] = _ => "This is a placeholder for the blog";
			Get["/signup"] = _ => View["signup"];
			Post["/signup"] = model =>
			{
				var signup = this.Bind<Models.Signup>();

				return View["welcome"];
			};
			Get["/login"] = _ => View["login"];
			Post["/login"] = model =>
			{
				var login = this.Bind<Models.Login>();

				return View["welcome"];
			};
		}
	}
}