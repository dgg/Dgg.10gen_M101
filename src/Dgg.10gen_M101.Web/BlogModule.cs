using Nancy;
using Nancy.Security;

namespace Dgg._10gen_M101.Web
{
	public class BlogModule : NancyModule
	{
		public BlogModule()
		{
			this.RequiresAuthentication();
			Get["/welcome"] = _ =>
			{
				ViewBag.IsAuthenticated = true;
				ViewBag.UserName = Context.CurrentUser.UserName;
				return View["welcome"];
			};
		}
	}
}