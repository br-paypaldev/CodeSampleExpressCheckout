namespace CodeSampleExpressCheckout {
	using System;
	
	/// <summary>
	/// Uma representação de um produto. Utilizamos isso para mostrar o carrinho e também no momento
	/// da integração, para enviar nome, descrição e preço para o PayPal.
	/// </summary>
	public class Product {
		private int id;
		private string name;
		private string description;
		private double price;
		
		public Product() :this(0, "", 0, "") {}
		
		public Product(int id, string name, double price) {
			Id = id;
			Name = name;
			Price = price;
		}
		
		public Product(int id, string name, double price, string description) :this(id, name, price) {
			Description = description;
		}
		
		public string Description {
			get { return description; }
			set { description = value; }
		}
		
		public int Id {
			get { return id; }
			set { id = value; }
		}
		
		public string Name {
			get { return name; }
			set { name = value; }
		}
		
		public double Price {
			get { return price; }
			set { price = value; }
		}
	}
}

