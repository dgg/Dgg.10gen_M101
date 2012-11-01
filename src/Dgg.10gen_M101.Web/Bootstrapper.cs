using Dgg._10gen_M101.Web.Infrastructure.Authentication;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using TinyIoC;

namespace Dgg._10gen_M101.Web
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			// We don't call "base" here to prevent auto-discovery of
			// types/dependencies
			base.ConfigureApplicationContainer(container);
			
		}

		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);

			//container.Register<IUserMapper, UsersDb>();
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