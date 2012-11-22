using Dgg._10gen_M101.Web.Infrastructure.Validation;

namespace Dgg._10gen_M101.Web.Models
{
	public class Login : IValidatable
	{
		public Login()
		{
			Errors =  new ValidationResults();
		}

		public string UserName { get; set; }
		public string Password { get; set; }
		public ValidationResults Errors { get; set; }
	}
}