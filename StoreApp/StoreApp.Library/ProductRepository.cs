using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class ProductRepository
    {
        public List<IProduct> _allProducts  = new List<IProduct>();

        public List<IProduct> GetAllProduct()
        {
            return _allProducts;
        }

        public IProduct GetProductByName(string name)
        {
            foreach(IProduct product in _allProducts)
            {
                if(product.ProductName == name)
                {
                    return product;
                }
            }

            return null;
        }

        public IProduct GetProductById(int id)
        {
            foreach(IProduct product in _allProducts)
            {
                if(product.ProductId == id)
                {
                    return product;
                }
            }

            return null;
        }

        public bool AddNewProduct(IProduct product)
        {
            if(product != null)
            {
                _allProducts.Add(product);
                return true;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public int GetProductIdWithName(string name)
        {

            foreach (Product product in _allProducts)
            {
                if (product.ProductName == name)
                {
                    return product.ProductId;
                }
            }

            return -1;
        }
    }
}
