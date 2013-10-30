using System;
using System.Collections.Generic;
using System.Linq;

namespace BDService
{

	public interface IModel {
		int Id { get; set; }

	}

	public class DataStore : Dictionary<int, IModel> {}

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
			if (this.Exists(m.Id)) {
				return false;
			}
			int newId = this.GetNextId ();
			m.Id = newId;
			this.data.Add (newId, m);
			return true;
		}

		public virtual bool Remove (int Id)
		{
			return this.data.Remove (Id);
		}
	}

	
	/**
	 * Concrete Products (Derived StorageModel)
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

		public new List<Product> GetAll ()
		{

			return base.GetAll().Cast<Product>().ToList(); // .NET 4.0 Covariance
		}

		public new Product Get (int Id) // UGLY why I have to "new" this..
		{

			return (Product) base.Get(Id); // Simple ecplicit Cast
		}
	}

	/**
	 * Repository A "Collection" of DataStores
	 */
	public static class Repository {

		private static Products ProductsInstance = new  Products ();

		public static Products Products {
			get {
				return Repository.ProductsInstance;
			}
		}
	}

}

