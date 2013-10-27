using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace BDService
{

	public class ProductsService : IService
	{
		public static List<Product> list = new List<Product> ();

		public static List<Product> Reset ()
		{
			list.Add (new Product { Title = "Soup", Price = 1.59 });
			list.Add (new Product { Title = "Milk", Price = 0.72 });
			list.Add (new Product { Title = "Bananas", Price = 0.96 });
			return list;
		}

		protected static bool Add (Product p)
		{
			if (list.Contains (p)) {
				return false;
			}

			list.Add (p);
			return true;
		}

		// Service Endpoints

		public ProductsResponse GET(Products request)  //Products class is matching the "/products" route
		{

			return new ProductsResponse { Result = this.list};
		}

		public object POST(Product request) 
		{
			return ProductService.Add ( request);
		}

		// Done with Endpoints

	}
}