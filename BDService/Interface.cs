using System;
using System.Collections.Generic;

namespace BDService.Interace
{
	public class Interfaces
	{
		public class Product 
		{
			public string Title { get; set; }
			public double Price { get; set; }
		}

		public class ProductsEntity
		{
			public Product Result { get; set; }
		}

		public class ProductsCollection
		{
			public List<Product> Result { get; set; }
		}

	}
}

