using System;
using System.Collections.Generic;
using StoreApp.DataModel;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StoreApp.Library
{
    public class OrderRepository
    {
        /// <summary>
        /// Database context options to create database context
        /// </summary>
        private readonly DbContextOptions<StoreAppContext> _contextOptions;

        /// <summary>
        /// Constructor for order repository
        /// </summary>
        /// <param name="contextOptions">Context options to initialize database context</param>
        public OrderRepository(DbContextOptions<StoreAppContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        /// <summary>
        /// Gets customer orders by  customer id from database 
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>A list of order objects the customer has placed if any in database or an empty list if none found in database</returns>
        public List<Order> GetOrdersCustomerId(int id)
        {
            using var context = new StoreAppContext(_contextOptions);
            List<OrderDetail> dbOrders;
            List<Order> matchingOrders = new List<Order>();
            try
            {
                dbOrders = context.OrderDetails
                .Include(o => o.OrderProducts)
                .Where(o => o.CustomerId == id).ToList();
            } catch (InvalidOperationException)
            {
                return matchingOrders;
            }

            foreach(var order in dbOrders)
            {
                Dictionary<int, int> products = new Dictionary<int, int>();

                foreach (var product in order.OrderProducts)
                {
                    products.Add(product.ProductId, product.Quantity);
                }

                matchingOrders.Add(new Order(order.Id, order.LocationId, order.CustomerId, order.Date, order.Total, products));
            }
            return matchingOrders;
        }


        /// <summary>
        /// Gets store orders by store id from database
        /// </summary>
        /// <param name="id">Store id</param>
        /// <returns>A list of order objects the store has received if any in database or an empty list if none found in database</returns>
        public List<Order> GetOrdersByStoreId(int id)
        {
            using var context = new StoreAppContext(_contextOptions);
            List<OrderDetail> dbOrders;
            List<Order> matchingOrders = new List<Order>();
            try
            {
                dbOrders = context.OrderDetails
                .Include(o => o.OrderProducts)
                .Where(o => o.LocationId == id).ToList();
            }
            catch (InvalidOperationException)
            {
                return matchingOrders;
            }

            foreach (var order in dbOrders)
            {
                Dictionary<int, int> products = new Dictionary<int, int>();

                foreach (var product in order.OrderProducts)
                {
                    products.Add(product.ProductId, product.Quantity);
                }

                matchingOrders.Add(new Order(order.Id, order.LocationId, order.CustomerId, order.Date, order.Total, products));
            }
            return matchingOrders;

        }

        /// <summary>
        /// Adds new order products and details to database
        /// </summary>
        /// <param name="order">Library Order object containing relevant order info</param>
        /// <returns>True if order added succesfully false if otherwise</returns>
        public bool AddNewOrder(Order order)
        {
            using var context = new StoreAppContext(_contextOptions);
           
            if (order != null)
            {
                var dbOrder = new OrderDetail()
                {
                    CustomerId = order.GetCustomerId,
                    LocationId = order.GetStoreId,
                    Date = order.GetTime,
                    Total = order.GetTotal
                };

                context.OrderDetails.Add(dbOrder);
                context.SaveChanges();

                foreach (var product in order.GetOrderItems)
                {
                    var dbOrderProduct = new OrderProduct()
                    {
                        OrderId = dbOrder.Id,
                        ProductId = product.Key,
                        Quantity = product.Value
                    };

                    context.OrderProducts.Add(dbOrderProduct);
                }

                context.SaveChanges();
                return true;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
