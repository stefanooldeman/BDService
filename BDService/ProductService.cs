using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace BDService
{

	public class ProductService : IService
	{

		/**
		 * Service Endpoints
		 */

		public ProductsCollection GET(GetProducts request)  //Products class is matching the "/products" route
		{

			return new ProductsCollection { Result = Repository.Products.GetAll () };
		}
		
		public bool POST(PostProducts request) 
		{
			foreach (ProductModel p in request) {
				if (p.Id > 0) {
					return false;
				}
				p.Id = Repository.Products.GetNextId ();
				Repository.Products.Add (p);
			}

			return true;
		}

		public ProductEntity GET(ProductResource request) // For consistency use ProductEntity?
		{
			return new ProductEntity { Result = Repository.Products.Get (request.Id) };
		}

		public bool PUT(ProductResource p) 
		{
			//TODO figure out how the request objects are populated (json request)
			ProductModel x = Repository.Products.Get(p.Id);
			x = p;
			Repository.Products.Remove (p.Id);
			return Repository.Products.Add (x);
		}

		
		public ProductsCollection GET(ResetProducts request)
		{
//			Repository.Products = new  Products ();
			Repository.Products.Add (new ProductModel { Title = "Soup", Price = 1.59 });
			Repository.Products.Add (new ProductModel { Title = "Milk", Price = 0.72 });
			Repository.Products.Add (new ProductModel { Title = "Bananas", Price = 0.96 });
			return new ProductsCollection { Result = Repository.Products.GetAll () };
		}

	}
}