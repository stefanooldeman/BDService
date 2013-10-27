using System;
using System.Collections.Generic;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace BDService
{

	// ROOT URL
	[Route("/products")]
	public class Product
	{
		public string Title { get; set; }
		public double Price { get; set; }
	}

	[Route("/products/reset")]
	public class ProductsReset : Product {}

	public class ProductsEntity
	{
		public Product Result { get; set; }
	}

	public class ProductsCollection
	{
		public List<Product> Result { get; set; }
	}


	// HELLO URI
	[Route("/hello")]
	[Route("/hello/{Name}")]
	public class Hello
	{
		public string Name { get; set; }
	}

	public class HelloResponse
	{
		public string Result { get; set; }
	}

	public class Endpoints : IService
	{
		public object Any(Hello request)
		{
			return new HelloResponse { Result = "Hello, " + request.Name };
		}


		public object Any(Product request) 
		{
			return new ProductsCollection { Result = ProductService.getAll ()};
		}

		public object POST(Product request) 
		{
			return ProductService.Add ( request);
		}
	}
}

