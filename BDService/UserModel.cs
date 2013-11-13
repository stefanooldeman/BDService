using System;
using System.Collections.Generic;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;


namespace BDService
{
	/**
	 * User Entity
	 */
	public class UserModel : IModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}


	/**
	 * The Interface endpoints and response types
	 */
	[Route("/users", "POST, OPTIONS")]
	public class CreateNewUser : UserModel {}
	
//	[Route("/user/auth", "POST, OPTIONS")]
//	public class PostUserAuth : UserModel {}


	[Route("/user/{username}", "GET, PUT, OPTIONS")]
	public class UserResource : UserModel {}
	
}

