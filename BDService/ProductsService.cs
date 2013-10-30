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

			return new ProductCollection { Result = Repository.Products.GetAll () };
		}

		public bool POST(PostProductsReset request)
		{
			Repository.Products.Add (new Product { Title = "Soup", Price = 1.59 });
			Repository.Products.Add (new Product { Title = "Milk", Price = 0.72 });
			Repository.Products.Add (new Product { Title = "Bananas", Price = 0.96 });
			return true;
		}

		public ProductEntity GET(ProductResource request) // For consistency use ProductEntity?
		{
			return new ProductEntity { Result = Repository.Products.Get (request.Id) };
		}

		public bool POST(ProductResource p) 
		{
			//TODO figure out how the request objects are populated (json request)

			return Repository.Products.Add (p);
		}

	}
}