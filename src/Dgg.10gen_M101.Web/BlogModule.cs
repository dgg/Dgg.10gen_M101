using System;
using Dgg._10gen_M101.Web.Infrastructure.Authentication;
using Dgg._10gen_M101.Web.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.ModelBinding;
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

	public class AuthModule : NancyModule
	{
		public AuthModule()
		{
			Get["/"] = _ => "This is a placeholder for the blog";
			Get["/signup"] = _ => View["signup"];
			Post["/signup"] = model =>
			{
				if (Context.CurrentUser != null) return Context.GetRedirect("/welcome");

				var signup = this.Bind<Signup>();
				return View["welcome"];
			};
			Get["/login"] = _ => View["login", new Login()];
			Post["/login"] = model =>
			{
				var login = this.Bind<Login>();
				bool authenticated = UsersDb.Authenticate(login);
				if (!authenticated)
				{
					return View["login", login];
				}
				return this.LoginAndRedirect(Guid.NewGuid(), fallbackRedirectUrl: "/welcome");
			};
			Get["/logout"] = _ => this.LogoutAndRedirect("/");
		}
	}
}