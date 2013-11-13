using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;
using System.Linq;
using ServiceStack.Common.Web; // for HTTP Status Codes
using ServiceStack.ServiceInterface;

namespace BDService
{

	public class ProductService : Service
	{

		/**
		 * Service Endpoints
		 */
		public void OPTIONS(GetProducts request) {
			base.Response.StatusCode = 200;
			base.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
			base.Response.AddHeader ("Access-Control-Allow-Headers", "Content-Type");
		}

		public IEnumerable<ProductModel> GET(GetProducts request)  //Products class is matching the "/products" route
		{
			// only return products in stock!
			var list = Repository.Products.GetAll ().Where (product => product.Amount != 0);
			return list;
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
			base.Response.StatusCode = 201;
			return true;
		}

		public void OPTIONS(ProductResource request) {
			base.Response.StatusCode = 200;
			base.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
			base.Response.AddHeader ("Access-Control-Allow-Headers", "Content-Type");
		}

		public ProductModel GET(ProductResource request) // For consistency use ProductEntity?
		{
			return Repository.Products.Get (request.Id);
		}

		public bool PUT(ProductResource p) 
		{
			//TODO figure out how the request objects are populated (json request)
			ProductModel x = Repository.Products.Get(p.Id);
			x = p;
			Repository.Products.Remove (p.Id);
			return Repository.Products.Add (x);
		}

		// Loose hanging fruits..	
		public List<ProductModel> GET(ResetProducts request)
		{
			Repository.Products.Add (new ProductModel { Title = "Soup", Price = 1.59 });
			Repository.Products.Add (new ProductModel { Title = "Milk", Price = 0.72 });
			Repository.Products.Add (new ProductModel { Title = "Bananas", Price = 0.96 });
			return Repository.Products.GetAll ();
		}

	}
}