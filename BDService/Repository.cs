using System;
using System.Collections.Generic;
using System.Linq;

namespace BDService
{

	public class IModel {}  // Don't use interface because I can't convert to it..

	public class DataStore : Dictionary<int, IModel> {}

	public abstract class StorageModel
	{
		protected DataStore data = new DataStore ();

		public void Sync(DataStore data) {
			this.data = data;
		}

		public int GetNextId ()
		{
			List<int> keys = new List<int> (this.data.Keys);
			keys.Sort();
			return keys [0];
		}

		public bool Exists (int Id)
		{
			return this.data.ContainsKey (Id);
		}

		public virtual List<IModel> GetAll ()
		{
			return new List<IModel> (this.data.Values);
		}

		public virtual IModel Get (int Id) 
		{
			return this.data [Id];
		}

		public virtual bool Add (IModel m)
		{
			int newId = this.GetNextId ();
			this.data.Add (newId, m);
			return true;
		}

		public virtual bool Remove (int Id)
		{
			return this.data.Remove (Id);
		}
	}

	public class Products : StorageModel {


		public Products (DataStore data) {

			this.data = data;
		}

		public new List<Product> GetAll ()
		{

			return base.GetAll().Cast<Product>().ToList(); // .NET 4.0 Covariance
		}

		public new Product Get (int Id) // UGLY why I have to "new" this..
		{

			return (Product) base.Get(Id); // Simple ecplicit Cast
		}
	}

	public static class Repository {

		private static DataStore ProductsData = new DataStore ();

		private static Products ProductsInstance = null;

		public static Products Products {
			get
			{
				if (Repository.ProductsInstance == null) {
					Repository.ProductsInstance = new  Products (Repository.ProductsData, this);
				}
				return Repository.ProductsInstance;
			}
		}

//		public void Sync (DataStore data) { // Almost like transaction
//			Repository.ProductsData = data;
//		}
	}

}

