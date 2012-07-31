using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSampleExpressCheckout.Model.CodeSampleExpressCheckout
{
    public class Product
    {
        private int id;
        private string name;
        private string description;
        private double price;

        public Product() : this(0, "", 0, "") { }

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Product (int id, string name, double price, string description) : this(id, name,price)
        {
            Description = description;
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}