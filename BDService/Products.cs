using System;
using System.Collections.Generic;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace BDService
{

	/**
	 * Product Entity
	 */
	public class Product : IModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
		public double Amount { get; set; }
	}


	/**
	 * The Interface endpoints and response types
	 */
	
	[Route("/reset/products")]
	public class PostProductsReset {}
	// lacking a response type, use bool

	[Route("/products", "GET")]
	public class GetProducts {}

	// TODO implement as Collection of type <Product>
	public class ProductCollection
	{
		public List<Product> Result { get; set; }
	}

	[Route("/product", "POST")]
	[Route("/product/{Id}", "GET,PUT")] // DELETE (disabled)
	public class ProductResource : Product {}

	public class ProductEntity
	{
		public Product Result { get; set; }
	}

}