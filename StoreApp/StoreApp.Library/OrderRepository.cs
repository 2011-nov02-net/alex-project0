using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class OrderRepository
    {
        private  List<Order> _allOrders = new List<Order>();

        public List<Order> GetAllOrders()
        {
            return _allOrders;
        }


        public Order GetOrderById(int id)
        {
            foreach(Order order in _allOrders)
            {
                if(order.OrderId == id)
                {
                    return order;
                }
            }

            return null;
        }

        public List<Order> GetOrdersCustomerId(int id)
        {

            List<Order> matchingOrders = new List<Order>();

            foreach(Order order in _allOrders)
            {
                if(order.GetCustomerId == id)
                {
                    matchingOrders.Add(order);
                }
            }

            return matchingOrders;
        }

        public List<Order> GetOrdersByStoreId(int id)
        {
            List<Order> matchingOrders = new List<Order>();

            foreach(Order order in _allOrders)
            {
                if(order.GetStoreId == id)
                {
                    matchingOrders.Add(order);
                } 
            }

            return matchingOrders;
        }

        public bool AddNewOrder(Order order)
        {
            if(order != null)
            {
                _allOrders.Add(order);
                return true;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
