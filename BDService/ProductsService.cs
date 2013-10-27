using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace BDService
{

	public class ProductsService : IService
	{

		/**
		 * Service Endpoints
		 */

		public ProductCollection GET(GetProducts request)  //Products class is matching the "/products" route
		{

			return new ProductCollection { Result = Repository.products};
		}

		public bool POST(PostProductsReset request)
		{
			Repository.products = new List<Product> ();
			Repository.products.Add (new Product { Title = "Soup", Price = 1.59 });
			Repository.products.Add (new Product { Title = "Milk", Price = 0.72 });
			Repository.products.Add (new Product { Title = "Bananas", Price = 0.96 });
			return true;
		}

		public ProductEntity GET(ProductResource request) // For consistency use ProductEntity?
		{
			return new ProductEntity { Result = Repository.products[request.Id] };
		}

		public bool POST(ProductResource p) 
		{
			if (Repository.products.Contains (p)) {
				return false;
			}

			Repository.products.Add (p);
			return true;
		}

		/**
		 * Protected methods
		 */

	}
}