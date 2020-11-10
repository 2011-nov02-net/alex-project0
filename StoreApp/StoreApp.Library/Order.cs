using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    [Serializable]
    class Order
    {
        private readonly int _orderId = 0;
        private readonly Store _store;
        private readonly Customer _customer;
        private readonly DateTime _time;
        private readonly int _total = 0;

        private readonly List<OrderProduct> _products;

        public Order(int id, Store store, Customer customer, DateTime time, List<OrderProduct> products)
        {
            _orderId = id;
            _store = store;
            _customer = customer;
            _time = time;
            _products = products;
        }

        public int OrderId
        {
            get
            {
                return _orderId;
            }
        }

        public Store GetStore
        {
            get
            {
                return _store;
            }
        }

        public Customer GetCustomer
        {
            get
            {
                return _customer;
            }
        }

        public DateTime GetTime
        {
            get
            {
                return _time;

            }
        }

        public List<OrderProduct> GetProduct
        {
            get
            {
                return _products;
            }
        }

        public int GetTotal
        {
            get
            {
                return _total;
            }
        }



    }
}
