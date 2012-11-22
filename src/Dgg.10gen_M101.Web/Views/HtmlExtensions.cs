using Dgg._10gen_M101.Web.Infrastructure.Validation;
using Dgg._10gen_M101.Web.Models;
using HtmlTags;
using Nancy.ViewEngines.Razor;
using IHtmlString = Nancy.ViewEngines.Razor.IHtmlString;


namespace Dgg._10gen_M101.Web.Views
{
	public static class HtmlExtensions
	{
		public static IHtmlString Error(this HtmlHelpers<dynamic> html, string fieldName)
		{
			ValidationResults errors = html.Model.Errors;
			return new NonEncodedHtmlString(errors.HasErrors(fieldName) ? "error" : string.Empty);
		}

		public static IHtmlString Error<T>(this HtmlHelpers<T> html, string fieldName) where T : IValidatable
		{
			ValidationResults errors = html.Model.Errors;
			return new NonEncodedHtmlString(errors.HasErrors(fieldName) ? "error" : string.Empty);
		}

		public static IHtmlString Errors(this HtmlHelpers<dynamic> html, string @class, string fieldName)
		{
			ValidationResults errors = html.Model.Errors;
			HtmlTag small;
			if (errors.HasErrors(fieldName))
			{
				small = new HtmlTag("small").AddClass(@class).AddClass("error").Append(
					new HtmlTag("ul", tt =>
					{
						foreach (var message in errors.MessagesFor(fieldName))
						{
							tt.Add("li").Text(message);
						}
					}));	
			}
			else
			{
				small = new HtmlTag("small").AddClass(@class).AddClass("hide");
			}
			return new NonEncodedHtmlString(small.ToString());
		}

		public static IHtmlString Errors<T>(this HtmlHelpers<T> html, string @class, string fieldName) where T : IValidatable
		{
			ValidationResults errors = html.Model.Errors;
			HtmlTag small;
			if (errors.HasErrors(fieldName))
			{
				small = new HtmlTag("small").AddClass(@class).AddClass("error").Append(
					new HtmlTag("ul", tt =>
					{
						foreach (var message in errors.MessagesFor(fieldName))
						{
							tt.Add("li").Text(message);
						}
					}));
			}
			else
			{
				small = new HtmlTag("small").AddClass(@class).AddClass("hide");
			}
			return new NonEncodedHtmlString(small.ToString());
		}
	}
}