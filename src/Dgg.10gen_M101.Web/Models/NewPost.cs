using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Dgg._10gen_M101.Web.Infrastructure.Validation;

namespace Dgg._10gen_M101.Web.Models
{
	public class NewPost : IValidatable
	{
		public NewPost()
		{
			Errors =  new ValidationResults();
		}

		[Required]
		public string Title { get; set; }
		[Required]
		public string Body { get; set; }
		[Required]
		public string Tags { get; set; }

		public ValidationResults Errors { get; set; }

		public string[] Taggify()
		{
			return Tags.Split(new[]{","}, StringSplitOptions.RemoveEmptyEntries);
		}

		public string Bodify()
		{
			return HttpUtility.HtmlEncode(Body)
				.Replace("\r\n", "<br />");
		}

		public string GeneratePermaLink()
		{
			string separator = "_";

			string slug = removeNonWordCharacters(Title);
			slug = removeTrailingPeriods(slug);

			IEnumerable<string> words = splitIntoWords(slug);
			IEnumerable<string> encodedWords = words.Select(replaceUnicodeCharacters);
			slug = String.Join(separator, encodedWords.ToArray());
			slug = slug.Trim(new[] { separator[0] });

			if (isNumeric(slug))
			{
				slug = "n_" + slug;
			}
			slug = slug.ToLower(CultureInfo.InvariantCulture);

			return slug;
		}

		private static readonly Regex _wordCharRegex = new Regex(@"[^\w\d\.\- ]+", RegexOptions.Compiled);
		private static string removeNonWordCharacters(string text)
		{
			return _wordCharRegex.Replace(text, string.Empty);
		}

		private static readonly Regex _trailingPeriodRegex = new Regex(@"\.+$", RegexOptions.Compiled);
		private static string removeTrailingPeriods(string text)
		{
			return _trailingPeriodRegex.Replace(text, string.Empty);
		}

		private static readonly Regex _numericRegex = new Regex(@"^\d+$", RegexOptions.Compiled);
		private static bool isNumeric(string text)
		{
			return _numericRegex.IsMatch(text);
		}

		private static string replaceUnicodeCharacters(string text)
		{
			string normalized = text.Normalize(NormalizationForm.FormKD);
			Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage,
				new EncoderReplacementFallback(string.Empty),
				new DecoderReplacementFallback(string.Empty));
			byte[] bytes = removal.GetBytes(normalized);

			string encoded = Encoding.ASCII.GetString(bytes);
			if (String.IsNullOrEmpty(encoded))
			{
				return HttpUtility.UrlEncode(text);
			}
			return encoded;
		}

		private static readonly Regex _splitWordsRegex = new Regex(@"\W+", RegexOptions.Compiled);
		public static IEnumerable<string> splitIntoWords(string source)
		{
			return _splitWordsRegex.Split(source.Trim());
		}
	}
}