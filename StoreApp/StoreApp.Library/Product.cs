using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    public class Product : IProduct
    {
        private readonly int _productId = 0;
        private readonly string _productName = "";
        private double _price = 0.0;


        public Product(int id, string name, double price)
        {
            _productId = id;
            _productName = name;
            _price = price;
        }

        public int ProductId => _productId;

        public string ProductName => _productName;


        public double Price => _price;

        public void UpdatePrice(int newPrice)
        {
            _price = newPrice;
        }
    }
}
