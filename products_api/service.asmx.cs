
using System;
using System.Collections.Generic;
//Webservices
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
// 3rd party
using ServiceStack.Redis;
using ServiceStack.Text;


namespace products_api
{

	public class Product
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Category { get; set; }
		public double Price { get; set; }
		public int Aantal { get; set; }
	}


	[WebService (Namespace = "http://tempuri.org/")]
	[ScriptService ()]
	public class Service : System.Web.Services.WebService
	{
		readonly RedisClient redisClient = new RedisClient();

		public Service ()
		{
		}


		[WebMethod]
		[ScriptMethod (UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
		public string addProduct (string title, string category, double price, int aantal)
		{
			var product = new Product {
				Title = title,
				Category = category,
				Price = price,
				Aantal = aantal
			};
			using (var redisProduct = redisClient.As<Product>()) {	
				product.Id = redisProduct.GetNextSequence();
				redisProduct.Store(product);
			}
			return "201, Created";
		}

		[WebMethod]
		[ScriptMethod (UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
		public object listProducts ()
		{
			using (var redisProduct = redisClient.As<Product>()) {
				var list = new List<Dictionary<string, object>>();
				foreach(var product in redisProduct.GetAll()) {
					Dictionary<string, object> item = new  Dictionary<string, object>();
					item.Add("id", product.Id);
					item.Add("title", product.Title);
					item.Add("price", product.Price);
					item.Add("aantal", product.Aantal);
					list.Add(item);
				}
				return list;
			}
		}
	}

}