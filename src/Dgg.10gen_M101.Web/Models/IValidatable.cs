using Dgg._10gen_M101.Web.Infrastructure.Validation;

namespace Dgg._10gen_M101.Web.Models
{
	public interface IValidatable
	{
		ValidationResults Errors { get; set; }
	}
}