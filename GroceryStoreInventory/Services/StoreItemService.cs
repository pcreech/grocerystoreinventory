using GroceryStoreInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Services
{
    public class StoreItemService
    {
        private StoreContext context;

        public StoreItemService(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<StoreItem> GetItems()
        {
            return context.StoreItems.ToList();
        }

        public void AddItem(StoreItem storeItem)
        {
            context.StoreItems.Add(storeItem);
            context.SaveChanges();
        }

        public void EditItem(StoreItem storeItem)
        {
            context.Entry(storeItem).State = EntityState.Modified;
            context.SaveChanges();
        }

        public StoreItem FindById(int? id)
        {
            return context.StoreItems.Find(id);
        }

        public void DeleteById(int id)
        {
            StoreItem storeItem = FindById(id);
            context.StoreItems.Remove(storeItem);
            context.SaveChanges();
        }
    }
}