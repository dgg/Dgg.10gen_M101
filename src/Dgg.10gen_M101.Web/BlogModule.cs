using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dgg._10gen_M101.Web.Infrastructure.Data;
using Dgg._10gen_M101.Web.Infrastructure.Validation;
using Dgg._10gen_M101.Web.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace Dgg._10gen_M101.Web
{
	public class BlogModule : NancyModule
	{
		public BlogModule(IBlogDb db)
		{
			this.RequiresAuthentication();
			Get["/welcome"] = _ =>
			{
				ViewBag.IsAuthenticated = true;
				ViewBag.UserName = Context.CurrentUser.UserName;
				return View["welcome"];
			};
			Get["/newpost"] = _ => View["newpost", new NewPost()];
			Post["/newPost"] = model =>
			{
				var post = this.Bind<NewPost>();
				var results = new List<ValidationResult>();
				if (!Validator.TryValidateObject(post, new ValidationContext(post, null, null), results))
				{
					post.Errors = new ValidationResults(results);
					return View["newpost", post];
				}
				
				string permalink = db.Create(post, Context.CurrentUser.UserName, DateTime.UtcNow);
				return View["/post/" + permalink];
			};
		}
	}
}