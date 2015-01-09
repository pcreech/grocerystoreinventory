using GroceryStoreInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace GroceryStoreInventory.Services
{
    public class ReceivingItemService : CrudService<ReceivingItem>
    {
        private StoreItemService storeItemService;

        public ReceivingItemService(StoreContext context, StoreItemService storeItemService)
            : base(context)
        {
            this.storeItemService = storeItemService;
        }

        public override void AddItem(ReceivingItem item)
        {
            var storeItem = storeItemService.FindById(item.StoreItemId);
            if(storeItem != null)
            {
                storeItem.Quantity += item.QuantityReceived;
            }
            base.AddItem(item);
        }

        public override void EditItem(ReceivingItem item)
        {
            var entry = context.Entry<ReceivingItem>(item);
            entry.State = EntityState.Modified;
            var originalQuantity = context.ReceivingItems.Where(p => p.Id == item.Id).Select(p => p.QuantityReceived).SingleOrDefault();
            var storeItem = storeItemService.FindById(item.StoreItemId);
            if (storeItem != null)
            {
                var updateQuantityAmount = item.QuantityReceived - originalQuantity;
                storeItem.Quantity += updateQuantityAmount;
            }
            context.SaveChanges();
        }

        public override IEnumerable<ReceivingItem> GetItems()
        {
            return set.Include(r => r.StoreItem).ToList();
        }
        
        public IEnumerable<StoreItem> GetStoreItems()
        {
            return storeItemService.GetItems();
        }
        
    }
}