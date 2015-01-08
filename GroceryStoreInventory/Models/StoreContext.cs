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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mappings.StoreItemMap());
            base.OnModelCreating(modelBuilder);
        }

        public StoreContext()
            : base("DefaultCOnnection")
        {
            Database.SetInitializer<StoreContext>(new CreateDatabaseIfNotExists<StoreContext>());

        }
    }
}