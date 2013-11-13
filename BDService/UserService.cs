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

		public UserEntity POST(CreateNewUser request)  //Products class is matching the "/products" route
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
				return new UserEntity { Result = user };
			}
		}

		public UserEntity GET(UserResource request)
		{
			if (!Repository.Users.UsernameExists (request.Username)) {
				throw new HttpError (404, "User does not exists");
			}
			UserModel user = Repository.Users.GetByUsername (request.Username);
			return new UserEntity { Result = user };
		}

		public UserEntity PUT(UserResource request)
		{
			// TO UPDATE THE USER INFO
			throw new HttpError (System.Net.HttpStatusCode.NotImplemented, "Not Implemented");
		}
	}
}