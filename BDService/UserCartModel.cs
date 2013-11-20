using System;
using System.Collections.Generic;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace BDService
{

	/**
	 * Product Entity
	 */
	public class ShoppingItem  : ProductModel
	{
		public string Discount { get; set; }
	}

	/**
	 * The Interface endpoints and response types
	 */
	[Route("/user/{username}/cart", "GET, POST, OPTIONS")]
	public class UserCartCollection : List<UserCartModel> {}

	[Route("/user/{username}/cart/{itemId}", "GET, PUT, DELETE, OPTIONS")]
	public class UserCartResource : UserCartModel {}

}