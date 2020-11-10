using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace StoreApp.Library
{
    public class Product
    {
        private readonly int _productId =0;
        private readonly string _productName = "";
        private int _productQuantity = 0;
        private double _price = 0.0; 

        public Product(int id, string name, double price)
        {
            _productId = id;
            _productName = name;
            _price = price;
        }

        public int ProductId
        {
            get
            {
                return _productId;
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }
        }


        public double Price
        {
            get
            {
                return _price;
            }
        }

        public void IncrementQuantity(int amount)
        {
            _productQuantity += amount;
        }

        public void DecrementQuantity(int amount)
        {
            _productQuantity -= amount;
        }

        public void UpdatePrice(int newPrice)
        {
            _price = newPrice;
        }

    }
}
