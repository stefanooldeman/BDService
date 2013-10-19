
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


namespace Joy
{

	public class User
	{
		public long id { get; set; }
		public string username { get; set; }
		public string password { get; set; }
	}

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
		readonly RedisClient redis = new RedisClient();

		public Service () { ; }

		[WebMethod]
		public User GetOrCreateCustomer (string username, string password)
		{
			if (username == null || username.Length < 3)
				throw new ArgumentNullException ("Username must be minimal 3 long");

			var userIdAliasKey = "id:User:Username:" + username.ToLower () + "Password:" + password.ToLower();

			using (var redisUsers = redis.As<User> ()) {
				//Get a typed version of redis client that works with <User>
				//Find user by DisplayName if exists
				string userKey = redis.GetValue (userIdAliasKey);
				if (userKey != null) {
					//found! return
					return redisUsers.GetValue (userKey);
				}

				//no user found, create a new one.
				User user = new User();
				user.id = redisUsers.GetNextSequence ();
				user.username = username;
				user.password = password;
				redisUsers.Store (user);

				//Save reference to User key using the DisplayName alias
				redis.SetEntry (userIdAliasKey, user.CreateUrn ());

				return redisUsers.GetById (user.id);
			}
		}

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
			using (var redisProduct = redis.As<Product>()) {
				product.id = redisProduct.GetNextSequence();
				redisProduct.Store(product);
			}
			return "201, Created";
		}

		[WebMethod]
		public List<Product> GetProducts ()
		{
			using (var redisProduct = redis.As<Product>()) {
				var list = new List<Product>();
				foreach(var product in redisProduct.GetAll()) {
					//do any magic here if needed
					list.Add(product);
				}
				return list;
			}
		}

		[WebMethod]
		public User GetUser ()
		{
			//no user found, create a new one.
			User user = new User();
			user.id = 12312;
			user.username = "foofofoof";
			user.password = "123";

			return user;
		}
	}
}