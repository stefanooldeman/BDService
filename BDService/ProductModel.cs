using System;
using System.Collections.Generic;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace BDService
{

	/**
	 * Product Entity
	 */
	public class ProductModel : IModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
		public int Amount { get; set; }
	}

	/**
	 * The Interface endpoints and response types
	 */
	[Route("/reset/products")]
	public class ResetProducts {}
	// lacking a response type, use bool


	[Route("/products", "GET")]
	public class GetProducts {}
	public class ProductsCollection
	{
		public List<ProductModel> Result { get; set; }
	}

	[Route("/products", "POST")]
	public class PostProducts : List<ProductModel> {}

	
	[Route("/product/{id}", "GET,PUT,DELETE")]
	public class ProductResource : ProductModel {}
	public class ProductEntity
	{
		public ProductModel Result { get; set; }
	}

}