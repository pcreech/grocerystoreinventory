using GroceryStoreInventory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Services
{
    public class StoreItemService : CrudService<StoreItem>
    {
        public StoreItemService(StoreContext context)
            : base(context)
        { }


    }
}