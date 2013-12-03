using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;
using ServiceStack.Common.Web; // for HTTP Status Codes
using ServiceStack.ServiceInterface;
using System.Net;

namespace BDService
{

	public class UserCartService : Service
	{

		/**
		 * Service Endpoints
		 */
		public void OPTIONS(UserCartCollection request) {
			base.Response.StatusCode = (int) HttpStatusCode.NoContent;
			base.Response.AddHeader("Access-Control-Allow-Methods", "POST, OPTIONS");
			base.Response.AddHeader ("Access-Control-Allow-Headers", "Content-Type");
		}

		public bool POST(UserCartCollection request)  //Products class is matching the "/products" route
		{
			// Check Username
			if (!Repository.Users.UsernameExists (request.Username)) {
				throw new HttpError (HttpStatusCode.NotFound, "A. User not found");
			}
			// Check Password
			UserModel user = Repository.Users.GetByUsername (request.Username);
			if (user.Password != request.Password) {
				throw new HttpError (HttpStatusCode.Forbidden, "B. Invalid username password combination");
			}
			UserCartModel record = new UserCartModel {Id = user.Id, Username = user.Username };
			foreach (ProductModel cartItem in request.Products) {
				// Check Product
				if (!Repository.Products.Exists (cartItem.Id)) {
					throw new HttpError (HttpStatusCode.NotFound, "C. Product not found");
				}
				ProductModel p = Repository.Products.Get (cartItem.Id);
				if (p.Amount < cartItem.Amount) {
					throw new HttpError (HttpStatusCode.Conflict, "D. Stock does not allow to : " + cartItem.Amount + " for productId: " + cartItem.Title);
				} else {
					// ensure the original price is used, except the amount in the Cart.
					p.Amount = cartItem.Amount;
					record.Products.Add (p);
				}
			}

//			Repository.Products. REPLACE THE AMOUNT
			Repository.UsersCart.Add (record);
			base.Response.StatusCode = (int) HttpStatusCode.Created;
			return true;
		}

		public void OPTIONS(UserCartResource request) {
			base.Response.StatusCode = (int) HttpStatusCode.NoContent;
			base.Response.AddHeader("Access-Control-Allow-Methods", "GET, PUT, DELETE, OPTIONS");
			base.Response.AddHeader ("Access-Control-Allow-Headers", "Content-Type");
		}

		public List<ProductModel> GET(UserCartResource request)
		{
			// Check Username
			if (!Repository.Users.UsernameExists (request.Username)) {
				throw new HttpError (HttpStatusCode.NotFound, "A. User not found");
			}
			// Check Password
			UserModel user = Repository.Users.GetByUsername (request.Username);
			if (user.Password != request.Password) {
				throw new HttpError (HttpStatusCode.Forbidden, "B. Invalid username password combination");
			}
			// Return all products in the cart
			return Repository.UsersCart.Get (user.Id).Products;
		}


	}
}