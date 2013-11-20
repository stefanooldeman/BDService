using System;
using System.Collections.Generic;
using System.Linq;

namespace BDService
{

	public interface IModel {
		int Id { get; set; }
	}
	
	public class DataStore : Dictionary<int, IModel> {}
	public class UniqueStore : Dictionary<string, int> {}
	
	/**
	 * Abstract StorageModel
	 */
	public abstract class StorageModel
	{
		public abstract DataStore data { get; }

		public int GetNextId ()
		{
			List<int> keys = new List<int> (this.data.Keys);
			keys.Sort();
			if (keys.Count () == 0) {
				return 10;
			}
			int newKey = keys.Last() + 1;
			return newKey;
		}

		public bool Exists (int id)
		{
			return this.data.ContainsKey (id);
		}

		public virtual List<IModel> GetAll ()
		{
			return new List<IModel> (this.data.Values);
		}

		public virtual IModel Get (int id) 
		{
			return this.data [id];
		}

		public virtual bool Add (IModel m)
		{
			if (this.Exists(m.Id)) {
				return false;
			}
			int newId = this.GetNextId ();
			m.Id = newId;
			this.data.Add (newId, m);
			return true;
		}

		public virtual bool Remove (int id)
		{
			return this.data.Remove (id);
		}
	}


	/**
	 * Concrete =====> Products <===== (Derived StorageModel)
	 */
	public class Products : StorageModel {
	
		private static DataStore _data = new DataStore ();

		#region implemented abstract members of StorageModel
		public override DataStore data {
			get {
				return  _data;
			}
		}
		#endregion

		public new List<ProductModel> GetAll ()
		{
			return base.GetAll().Cast<ProductModel>().ToList(); // .NET 4.0 Covariance
		}

		public new ProductModel Get (int id) // UGLY why I have to "new" this..
		{
			return (ProductModel) base.Get(id); // Simple ecplicit Cast
		}
	}
	
	/**
	 * Concrete ====> Users <===== (Derived StorageModel)
	 */
	public class Users : StorageModel {

		private static DataStore _data = new DataStore ();
		private static UniqueStore _secondIndex = new UniqueStore ();

		#region implemented abstract members of StorageModel
		public override DataStore data {
			get {
				return  _data;
			}
		}
		#endregion

		public new List<UserModel> GetAll ()
		{
			return base.GetAll().Cast<UserModel>().ToList(); // .NET 4.0 Covariance
		}

		public new UserModel Get (int id) // UGLY why I have to "new" this.. (i'm not sure if I'm doing right, but it works)
		{
			return (UserModel) base.Get(id); // Simple ecplicit Cast
		}


		// Adding extra functionality as addition to typical StorageModel

		public virtual bool Add (UserModel m)
		{
			bool success = base.Add (m);
			if (success) {
				_secondIndex.Add (m.Username, m.Id);
			}
			return success;
		}

		public bool UsernameExists (string username) {
			return _secondIndex.ContainsKey (username);
		}

		public UserModel GetByUsername (string username) {
			int userId = _secondIndex [username];
			return (UserModel) base.Get (userId);
		}
	}
	/**
	 * Concrete ====> UsersCart <===== (Derived StorageModel)
	 */
	public class UsersCart : StorageModel {

		private static DataStore _data = new DataStore ();

		#region implemented abstract members of StorageModel
		public override DataStore data {
			get {
				return  _data;
			}
		}
		#endregion

		// Hiding the base methods because the types should be overridden!
		public new List<UserCartModel> GetAll ()
		{
			return base.GetAll().Cast<UserCartModel>().ToList(); // .NET 4.0 Covariance
		}

		public new UserCartModel Get (int id)
		{
			return (UserCartModel) base.Get(id); // Simple ecplicit Cast
		}


		// Adding extra functionality as addition to typical StorageModel
		public virtual bool Add (UserCartModel userCart)
		{
			if (!Repository.Users.Exists (userCart.Id) || this.Exists(userCart.Id)) {
				return false;
			}
			this.data.Add (userCart.Id, userCart);
			return true;
		}
	}

	/**
	 *  Repository A "Collection" of DataStores
	 */
	public static class Repository {

		private static Products ProductsInstance = new  Products ();

		public static Products Products {
			get {
				return Repository.ProductsInstance;
			}
		}

		private static Users UsersInstance = new  Users ();

		public static Users Users {
			get {
				return Repository.UsersInstance;
			}
		}

		private static UsersCart UsersCartInstance = new  UsersCart ();

		public static UsersCart UsersCart {
			get {
				return Repository.UsersCartInstance;
			}
		}
	}

}

