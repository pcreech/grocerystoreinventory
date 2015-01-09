using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.ViewModels.ReceivingItems
{
    public class ReceivingApiViewModel
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public DateTime DateReceived { get; set; }
        public int QuantityReceived { get; set; }
    }
}