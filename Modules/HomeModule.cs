using Nancy;
using System.Collections.Generic;

namespace Salon
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ =>
			{
				return View["index.cshtml"];
			};

		}
	}
}
