using GroceryStoreInventory.ViewModels.ReceivingItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.ViewModels.StoreItems
{
    public class StoreApiViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }

        public virtual List<ReceivingApiViewModel> ReceivingItems { get; set; }
    }
}