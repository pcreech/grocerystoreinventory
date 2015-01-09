using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GroceryStoreInventory.Models;
using GroceryStoreInventory.Services;
using GroceryStoreInventory.ViewModels.StoreItems;
using AutoMapper;
using GroceryStoreInventory.ViewModels.ReceivingItems;

namespace GroceryStoreInventory.Controllers
{
    public class InventoryController : ApiController
    {
        private StoreItemService service;

        public InventoryController(StoreItemService service)
        {
            this.service = service;

            Mapper.CreateMap<ReceivingItem, ReceivingApiViewModel>();
            Mapper.CreateMap<StoreItem, StoreApiViewModel>();
        }

        // GET: api/Inventory
        public IQueryable<StoreApiViewModel> GetStoreItems()
        {
            var items = service.GetItemsWithInclude()
                .OrderByDescending(p => p.ReceivingItems.Any() ? p.ReceivingItems.Max(q => q.DateReceived) : DateTime.MinValue)
                .ThenByDescending(p => p.Quantity)
             ;
            return Mapper.Map<IEnumerable<StoreApiViewModel>>(items).AsQueryable();
        }

    }
}