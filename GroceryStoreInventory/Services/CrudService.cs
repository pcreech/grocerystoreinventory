using GroceryStoreInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Services
{
    public abstract class CrudService<T> where T : class
    {
        protected StoreContext context;
        protected DbSet<T> set;
        
        public CrudService(StoreContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public virtual void AddItem(T item)
        {
            set.Add(item);
            context.SaveChanges();
        }

        public virtual void DeleteById(int id)
        {
            T item = FindById(id);
            set.Remove(item);
            context.SaveChanges();
        }

        public virtual void EditItem(T item)
        {
            var entry = context.Entry<T>(item);
            entry.State = EntityState.Modified;
            context.SaveChanges();
        }

        public virtual T FindById(int? id)
        {
            return set.Find(id);
        }

        public virtual IEnumerable<T> GetItems()
        {
            return set.ToList();
        }
    }
}
