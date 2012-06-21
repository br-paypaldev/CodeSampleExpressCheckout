namespace CodeSampleExpressCheckout {
	using System;
	using System.Collections.Generic;
	
	/// <summary>
	/// A classe Cart é uma simulação de um carrinho de compras do cliente. Aqui está bem simples
	/// pois a preocupação é mostrar o fluxo do checkout, não um carrinho de compras.
	/// </summary>
	public class Cart {
		private static Cart instance;
		private Dictionary<Product, int> items;
		
		private Cart () {
			items = new Dictionary<Product, int>();
			
			//Populamos o carrinho com alguns itens, como se o cliente os tivesse selecionado para
			//compra.
			selfPopulate();
		}
		
		private void selfPopulate() {
			int i = 1;
			ProductDataAccess products = new ProductDataAccess();
			
			foreach (Product product in products.ProductList) {
				Add(product, i++);
			}
		}
		
		public static Cart Instance() {
			if (instance == null) {
				instance = new Cart();
			}
			
			return instance;
		}
		
		public void Add(Product product, int quantity) {
			items.Add(product, quantity);
		}
		
		public Dictionary<Product, int> Items {
			get { return items; }
		}
	}
}