using MongoDB.Bson.Serialization.Conventions;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using TinyIoC;

namespace Dgg._10gen_M101.Web
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);
			var profile = new ConventionProfile();
			profile.SetElementNameConvention(new CamelCaseElementNameConvention());
			MongoDB.Bson.Serialization.BsonClassMap.RegisterConventions(profile, _ => true);
		}

		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

			var authConfig = new FormsAuthenticationConfiguration()
			{
				RedirectUrl = "/login",
				UserMapper = container.Resolve<IUserMapper>(),
			};
			FormsAuthentication.Enable(ApplicationPipelines, authConfig);
		}
	}
}