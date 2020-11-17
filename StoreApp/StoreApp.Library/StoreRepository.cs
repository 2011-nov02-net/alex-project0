using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class StoreRepository
    {
        List<Store> _allStores = new List<Store>();

        public List<Store> GetAllStores()
        {
            return _allStores;
        }

        public Store GetStoreByName(string name)
        {
            foreach (Store store in _allStores)
            {
                if (store.StoreName == name)
                {
                    return store;
                }
            }

            return null;
        }
        
        public Store GetStoreById(int id)
        {
            foreach (Store store in _allStores)
            {
                if (store.StoreId == id)
                {
                    return store;
                }
            }

            return null;
        }

        public Dictionary<int, int> StoreInventory(int storeId)
        {
            Store store = GetStoreById(storeId);

            if (store != null)
            {
                return store.StoreInventory;
            }

            return null;
        }
            

        public int CheckStoreInventoryForProduct(int storeId, int productId)
        {
            Store store = GetStoreById(storeId);

            if (store != null && store.StoreInventory.ContainsKey(productId))
            {
                return store.StoreInventory[productId];
            }

            return -1;
        }

        public bool AddNewStore(Store store)
        {
            if(store != null)
            {
                _allStores.Add(store);
                return true;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public bool PlaceOrder(int storeId, Dictionary<int, int> items)
        {
            Store store = GetStoreById(storeId);
            if(store != null)
            {
                foreach (var item in items)
                {
                    store.StoreInventory[item.Key] -= item.Value;
                }

                return true;
            }
            else
            {
                return false;
            }
            
        }

        public int GetStoreIdWithName (string name)
        {

            foreach (Store store in _allStores)
            {
                if (store.StoreName == name)
                {
                    return store.StoreId;
                }
            }

            return -1;
        }
    }

    

}
