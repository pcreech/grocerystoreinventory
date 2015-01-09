namespace GroceryStoreInventory.Migrations
{
    using GroceryStoreInventory.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GroceryStoreInventory.Models.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GroceryStoreInventory.Models.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.StoreItems.AddOrUpdate(
                p => p.Id,
                new StoreItem { Id = 1, Brand = "Merita", Description = "Merita Bread", Name = "Merita Bread", Quantity = 96, Sku = "1001" },
                new StoreItem { Id = 2, Brand = "Farm Fresh", Description = "Farm Fresh Eggs", Name = "Farm Fresh Eggs", Quantity = 34, Sku = "1002" },
                new StoreItem { Id = 3, Brand = "Kona", Description = "Kona Castaway IPA", Name = "Castaway IPA", Quantity = 64, Sku = "1003" },
                new StoreItem { Id = 4, Brand = "Maola", Description = "Maola Milk", Name = "2% Milk", Quantity = 29, Sku = "1004" },
                new StoreItem { Id = 5, Brand = "Maruchan", Description = "Maruchan Ramen", Name = "Beef Ramen", Quantity = 43, Sku = "1005" }
                );

            context.ReceivingItems.AddOrUpdate(
                p => p.Id,
                new ReceivingItem { Id = 1, DateReceived = new DateTime(2014, 08, 22), Invoice = "1001", QuantityReceived = 23, StoreItemId = 2},
                new ReceivingItem { Id = 2, DateReceived = new DateTime(2015, 01, 03), Invoice = "1021", QuantityReceived = 10, StoreItemId = 5},
                new ReceivingItem { Id = 3, DateReceived = new DateTime(2014, 12, 21), Invoice = "1011", QuantityReceived = 50, StoreItemId = 1},
                new ReceivingItem { Id = 4, DateReceived = new DateTime(2014, 09, 07), Invoice = "1003", QuantityReceived = 11, StoreItemId = 2},
                new ReceivingItem { Id = 5, DateReceived = new DateTime(2014, 09, 07), Invoice = "1003", QuantityReceived = 3, StoreItemId = 5},
                new ReceivingItem { Id = 6, DateReceived = new DateTime(2014, 10, 20), Invoice = "1009", QuantityReceived = 13, StoreItemId = 2},
                new ReceivingItem { Id = 7, DateReceived = new DateTime(2014, 10, 20), Invoice = "1009", QuantityReceived = 20, StoreItemId = 5},
                new ReceivingItem { Id = 8, DateReceived = new DateTime(2014, 10, 20), Invoice = "1009", QuantityReceived = 43, StoreItemId = 1}
                );
        }
    }
}
