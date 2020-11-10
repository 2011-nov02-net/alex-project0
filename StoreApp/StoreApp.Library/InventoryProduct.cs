using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    class InventoryProduct : IProduct
    {
        private readonly int _productId = 0;
        private readonly string _productName = "";
        private int _inStock = 0;
        private double _price = 0.0;


        public InventoryProduct(int id, string name, double price)
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
            _inStock += amount;
        }

        public void DecrementQuantity(int amount)
        {
            _inStock -= amount;
        }

        public void UpdatePrice(int newPrice)
        {
            _price = newPrice;
        }
    }
}
