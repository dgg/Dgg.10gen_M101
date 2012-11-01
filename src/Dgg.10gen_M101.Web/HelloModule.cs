using Nancy;

namespace Dgg._10gen_M101.Web
{
	public class HelloModule : NancyModule
	{
		public HelloModule()
		{
			Get["/hello"] = _ => "Hello World";
			Get["/hello/{name}"] = param => "Hello " + param.name;
			Get["/foundation"] = _ => View["foundation"];
		}
	}
}