using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    class Store
    {
        private readonly int _storeId = 0;
        private readonly string _storeName = "";
        private List<InventoryProduct> _storeInventory;
        private List<Order> _storeOrderHistory;

        public Store(int id, string name)
        {
            _storeId = id;
            _storeName = name;
        }

        public InventoryProduct CheckInventory(string productName)
        {
            return new InventoryProduct(0,"jk",12.0);
        }

        public void PlaceOrder(Order order)
        {

        }

        public void Restock(string name, int amout)
        {

        }
        
    }
}
