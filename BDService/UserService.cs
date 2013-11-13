using System;
using System.Collections.Generic;
using ServiceStack.ServiceHost;
using ServiceStack.Common.Web; // for HTTP Status Codes
using ServiceStack.ServiceInterface;

namespace BDService
{

	public class UserService : Service
	{

		/**
		 * Service Endpoints
		 */
		public void OPTIONS(CreateNewUser request) {
			base.Response.StatusCode = 200;
			base.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
			base.Response.AddHeader ("Access-Control-Allow-Headers", "Content-Type");
		}

		public UserModel POST(CreateNewUser request)  //Products class is matching the "/products" route
		{
//			if (Repository.Users.
			if (Repository.Users.UsernameExists (request.Username)) {
				throw new HttpError(303, "This resource already exists", "/v1/user/" + request.Username);
			} else {
				// create a new user
				// generate a password
				base.Response.StatusCode = 201;
				string password = "Bd-123";
				UserModel user = new UserModel { Username = request.Username, Password = password };
				Repository.Users.Add (user);
				return user;
			}
		}

		public void OPTIONS(UserResource request) {
			base.Response.StatusCode = 200;
			base.Response.AddHeader("Access-Control-Allow-Methods", "GET, PUT, OPTIONS");
			base.Response.AddHeader ("Access-Control-Allow-Headers", "Content-Type");
		}

		public UserModel GET(UserResource request)
		{
			if (!Repository.Users.UsernameExists (request.Username)) {
				throw new HttpError (404, "User does not exists");
			}
			return Repository.Users.GetByUsername (request.Username);
		}

		public UserModel PUT(UserResource request)
		{
			// TO UPDATE THE USER INFO
			throw new HttpError (System.Net.HttpStatusCode.NotImplemented, "Not Implemented");
		}
	}
}