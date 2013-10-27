
using System;
using System.Collections.Generic;
//Webservices
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
// 3rd party
using ServiceStack.Redis;
using ServiceStack.Text;
using ServiceStack.Common.Utils;
using System.Collections;
using ServiceStack.ServiceHost;


namespace Joy
{

	public class Product
	{
		public long id { get; set; }
		public string title { get; set; }
		public string category { get; set; }
		public string price { get; set; }
		public int quantity { get; set; }
	}

	[ScriptService ()]
	public class Service : System.Web.Services.WebService
	{

		public List<Product> list = new List<Product>();

		[WebMethod]
		public string AddProduct (string customerId, string title, string category, string price, string quantity)
		{
			if (title == null || category == null || price == null) {
				throw new HttpRequestValidationException("params title, category and price are mentadory");
			}

			//validate price
			double value;
			double.TryParse(price, out value);

			var product = new Product {
				title = title,
				category = category,
				price = price, //do not convert to double as it causes problems with JSON
				quantity = Convert.ToInt32(quantity)
			};
			this.list.Add(product);

			return "201, Created";
		}

		[WebMethod]
		public List<Product> GetProducts ()
		{
			return this.list;
		}

		[WebMethod]
		public Product GetUser ()
		{
			//no user found, create a new one.
			Product p = new Product();
			p.category = "Product";
			p.title = "Tomato";
			p.quantity = 5;
			p.price = "10.0";

			return p;
		}

	}

}