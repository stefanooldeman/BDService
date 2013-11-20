using System;
using System.Collections.Generic;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace BDService
{

	/**
	 * Product Entity
	 */
	public class UserCartModel : UserModel
	{
		public List<ProductModel> Products { get; set; }

		public UserCartModel () {
			Products = new List<ProductModel> ();
		}
	}

	/**
	 * The Interface endpoints and response types
	 */
	[Route("/user/{username}/cart", "POST, OPTIONS")]
	public class UserCartCollection : UserCartModel {}

	[Route("/user/{username}/cart/{id}", "GET, PUT, DELETE, OPTIONS")]
	public class UserCartResource : UserCartModel {}

}