using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroceryStoreInventory.Models;
using GroceryStoreInventory.Services;
using GroceryStoreInventory.ViewModels.StoreItems;
using AutoMapper;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace GroceryStoreInventory.Controllers
{
    [Authorize(Roles="Owner,Employee")]
    public class StoreItemsController : Controller
    {
        private StoreItemService service;

        public StoreItemsController(StoreItemService service)
        {
            this.service = service;
            Mapper.CreateMap<StoreItem, StoreItemViewModel>();
            Mapper.CreateMap<StoreItem, IndexViewModel>()
                .ForMember(
                    dest => dest.LastReceived, 
                    opt => opt.MapFrom(src => src.ReceivingItems.Any() ? 
                                                (DateTime?)src.ReceivingItems.Max(item => item.DateReceived) : 
                                                null));
            Mapper.CreateMap<StoreItemViewModel, StoreItem>();
        }

        // GET: StoreItems
        public ActionResult Index()
        {
            var items = Mapper.Map<IEnumerable<IndexViewModel>>(service.GetItems());
            return View(items.OrderByDescending(p => p.LastReceived));
        }

        // GET: StoreItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItemViewModel storeItem = Mapper.Map<StoreItemViewModel>(service.FindById(id));
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // GET: StoreItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Sku,Brand,Quantity")] StoreItemViewModel storeItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.AddItem(Mapper.Map<StoreItem>(storeItem));
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (!checkSku(ex)) throw ex;
                }
            
            }

            return View(storeItem);
        }

        // GET: StoreItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItemViewModel storeItem = Mapper.Map<StoreItemViewModel>(service.FindById(id));
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // POST: StoreItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Sku,Brand,Quantity")] StoreItemViewModel storeItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.EditItem(Mapper.Map<StoreItem>(storeItem));
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (!checkSku(ex)) throw ex;
                }
            }
            return View(storeItem);
        }

        // GET: StoreItems/Delete/5
        [Authorize(Roles="Owner")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreItemViewModel storeItem = Mapper.Map<StoreItemViewModel>(service.FindById(id));
            if (storeItem == null)
            {
                return HttpNotFound();
            }
            return View(storeItem);
        }

        // POST: StoreItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Owner")]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteById(id);
            return RedirectToAction("Index");
        }

        bool checkSku(DbUpdateException ex)
        {
            // Not the most elegant, but it works.
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                string message = ex.InnerException.InnerException.Message;
                if (message.StartsWith("Cannot insert duplicate key row in object 'dbo.StoreItems' with unique index 'IX_Sku'"))
                {
                    ModelState.AddModelError("Sku", "Sku already exists");
                    return true;
                }
            }
            return false;
        }
    }
}
