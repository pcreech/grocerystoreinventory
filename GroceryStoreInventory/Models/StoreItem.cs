using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Models
{
    public class StoreItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }

        public virtual List<ReceivingItem> ReceivingItems { get; set; }
    }
}