using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dgg._10gen_M101.Web.Infrastructure.Validation
{
	public class ValidationResults : IEnumerable<ValidationResult>
	{
		public ValidationResults(params ValidationResult[] results) : this(results.AsEnumerable()) { }

		private readonly IEnumerable<ValidationResult> _results;
		public ValidationResults(IEnumerable<ValidationResult> results)
		{
			_results = results;
		}

		public IEnumerator<ValidationResult> GetEnumerator()
		{
			return _results.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<string> MessagesFor(string fieldName)
		{
			return _results.Where(r => r.MemberNames.Contains(fieldName, StringComparer.OrdinalIgnoreCase)).Select(r => r.ErrorMessage);
		}

		public bool HasErrors(string fieldName)
		{
			return _results.Any(r => r.MemberNames.Contains(fieldName, StringComparer.OrdinalIgnoreCase));
		}

	}
}