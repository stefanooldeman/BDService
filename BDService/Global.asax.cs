using System;
using ServiceStack.WebHost.Endpoints;

namespace BDService
{

	public class Global : System.Web.HttpApplication
	{
		public class AppHost : AppHostBase
		{
			//Tell Service Stack the name of your application and where to find your web services

			// TODO check if the ProductService should be the first one.
			public AppHost() : base("BeeDee Shop", typeof(BDService.ProductsService).Assembly) { }

			public override void Configure(Funq.Container container)
			{

			}
		}

		protected void Application_Start(object sender, EventArgs e)
		{
			//Initialize application w/ singleton
			new AppHost().Init();
		}

		/*
		 *  NOT IN USE
		protected void Session_Start (Object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest (Object sender, EventArgs e)
		{
		}

		protected void Application_EndRequest (Object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest (Object sender, EventArgs e)
		{
		}

		protected void Application_Error (Object sender, EventArgs e)
		{
		}

		protected void Session_End (Object sender, EventArgs e)
		{
		}

		protected void Application_End (Object sender, EventArgs e)
		{
		}
		*/
	}
}

