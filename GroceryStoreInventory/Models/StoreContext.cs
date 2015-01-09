using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<ReceivingItem> ReceivingItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mappings.StoreItemMap());
            modelBuilder.Configurations.Add(new Mappings.ReceivingItemMap());
            base.OnModelCreating(modelBuilder);
        }

        public StoreContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<StoreContext>(new CreateDatabaseIfNotExists<StoreContext>());

        }
    }
}