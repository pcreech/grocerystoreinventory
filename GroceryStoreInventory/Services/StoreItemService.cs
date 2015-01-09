using GroceryStoreInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using GroceryStoreInventory.ViewModels.ReceivingItems;
using GroceryStoreInventory.ViewModels.StoreItems;
namespace GroceryStoreInventory.Services
{
    public class StoreItemService : CrudService<StoreItem>
    {
        public StoreItemService(StoreContext context)
            : base(context)
        {
        }

        public IEnumerable<StoreItem> GetItemsWithInclude()
        {
            return context.StoreItems
                .Include(p => p.ReceivingItems).ToList();
        }
    }
}