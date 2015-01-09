using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Models
{
    public class ReceivingItem
    {
        public int Id { get; set; }
        public int StoreItemId { get; set; }
        public string Invoice { get; set; }
        public DateTime DateReceived { get; set; }
        public int QuantityReceived { get; set; }

        public virtual StoreItem StoreItem { get; set; }
    }
}