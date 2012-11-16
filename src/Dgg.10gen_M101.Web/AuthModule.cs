using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dgg._10gen_M101.Web.Infrastructure.Authentication;
using Dgg._10gen_M101.Web.Infrastructure.Validation;
using Dgg._10gen_M101.Web.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace Dgg._10gen_M101.Web
{
	public class AuthModule : NancyModule
	{
		public AuthModule(IUsersDb users)
		{
			Get["/"] = _ => "This is a placeholder for the blog";
			Get["/signup"] = _ => View["signup", new Signup()];
			Post["/signup"] = model =>
			{
				if (Context.CurrentUser != null) return Context.GetRedirect("/welcome");
				
				var signup = this.Bind<Signup>();
				if (!users.Signup(signup))
				{
					signup.Errors = new ValidationResults(new ValidationResult("Username already in use. Please choose another", new[]{"username"}));
					return View["signup", signup];
				}
				return this.LoginAndRedirect(Guid.NewGuid(), fallbackRedirectUrl: "/welcome");
			};
			Get["/login"] = _ => View["login", new Login()];
			Post["/login"] = model =>
			{
				var login = this.Bind<Login>();
				bool authenticated = users.Authenticate(login);
				if (!authenticated)
				{
					login.Errors = new ValidationResults(new ValidationResult("Invalid Login", new[] { "userName" }));
					return View["login", login];
				}
				Guid sessionId = users.StartSession(login);
				return this.LoginAndRedirect(sessionId, fallbackRedirectUrl: "/welcome");
			};
			Get["/logout"] = _ =>
			{
				var sessionClaim = Context.CurrentUser.Claims.First();
				Guid sessionId = Guid.Parse(sessionClaim);
				users.EndSession(sessionId);
				return this.LogoutAndRedirect("/");
			};
		}
	}
}