using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public interface IProduct
    {
        int ProductId { get; }

        string ProductName { get; }

        double Price { get; }

        void UpdatePrice(int newPrice);
    }
}
