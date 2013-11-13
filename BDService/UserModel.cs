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
	[Route("/user", "POST")]
	public class CreateNewUser : UserModel {}
	// returns Entity
	
	[Route("/user/auth", "POST")]
	public class PostUserAuth : UserModel {}
	// returns true or false 200 Ok

	[Route("/user/{username}", "GET, PUT")]
	public class UserResource : UserModel {}
	// returns Entity

	
	/**
	 * Response types
	 */
	public class UserEntity
	{
		public UserModel Result { get; set; }
	}

	public class UserCollection
	{
		public List<UserEntity> Result { get; set; }
	}
}

