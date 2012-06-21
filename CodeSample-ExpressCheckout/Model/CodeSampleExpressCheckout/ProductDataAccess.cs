namespace CodeSampleExpressCheckout {
	using System;
	using System.Collections.Generic;
	
	/// <summary>
	/// Uma simulação de acesso a dados de produtos. Como o exemplo é sobre o fluxo de checkout, utilizamos
	/// uma lista de produtos pré-definidos.
	/// </summary>
	public class ProductDataAccess {
		private List<Product> products = new List<Product>();
		
		public ProductDataAccess () {
			products.Add(new Product(1, "Item 1", 10, "Um item de exemplo"));
			products.Add(new Product(2, "Item 2", 20, "Outro item de exemplo"));
		}
		
		/// <summary>
		/// Recupera a lista de produtos do banco de dados.
		/// </summary>
		/// <value>
		/// Uma lista de produtos.
		/// </value>
		public Product[] ProductList {
			get { return products.ToArray(); }
		}
	}
}

