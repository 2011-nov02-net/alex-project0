using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    interface IProduct
    {
        void IncrementQuantity(int amount);

        void DecrementQuantity(int amount);

        void UpdatePrice(int newPrice);
    }
}
