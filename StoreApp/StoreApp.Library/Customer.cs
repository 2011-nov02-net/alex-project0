using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    class Customer
    {
        private int _customerId = 0;
        private string _customerName = "";

        private List<Order> _customerOrders;

        public Customer(int id, string name)
        {
            _customerId = id;
            _customerName = name;

        }

        public int CustomerId
        {
            get
            {
                return _customerId;
            }
        }

        public string CustomerName
        {
            get
            {
                return _customerName;
            }
        }

        public void AddToOrderHistory(Order order)
        {
            _customerOrders.Add(order);
        }

        public List<Order> CustomerOrders
        {
            get
            {
                return _customerOrders;
            }
            
        }

    }
}
