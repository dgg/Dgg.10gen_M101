using Nancy;

namespace Dgg._10gen_M101.Web
{
	public class HelloModule : NancyModule
	{
		public HelloModule()
		{
			Get["/"] = param => "Hello World";
			Get["/hello/{name}"] = param => "Hello " + param.name;
		}
	}
}