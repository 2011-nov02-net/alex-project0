using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class CustomerRepository
    {
        public List<Customer> _allCustomers = new List<Customer>();

        public List<Customer> GetAllCustomers()
        {
            return _allCustomers;
        }

        public Customer GetCustomerById(int id)
        {
            foreach(Customer customer in _allCustomers)
            {
                if(customer.CustomerId == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer GetCustomerByFirstAndLastName(string first, string last)
        {
            foreach(Customer customer in _allCustomers)
            {
                if(customer.CustomerFirstName == first && customer.CustomerLastName == last)
                {
                    return customer;
                }
            }

            return null;
        }

        public bool AddNewCustomer(Customer customer)
        {
            if(customer != null)
            {

                if (GetCustomerByFirstAndLastName(customer.CustomerFirstName, customer.CustomerLastName) == null)
                {
                    _allCustomers.Add(customer);
                    return true;
                }
                return false;
                
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
