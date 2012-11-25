using System.ComponentModel.DataAnnotations;
using Dgg._10gen_M101.Web.Infrastructure.Validation;

namespace Dgg._10gen_M101.Web.Models
{
	public class NewComment : IValidatable
	{
		public NewComment()
		{
			Errors = new ValidationResults();
		}

		[Required]
		public string Name { get; set; }
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string Body { get; set; }
		public ValidationResults Errors { get; set; }
	}
}