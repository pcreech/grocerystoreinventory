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

namespace GroceryStoreInventory.Controllers
{
    [Authorize(Roles="user")]
    public class StoreItemsController : Controller
    {
        private StoreItemService service;

        public StoreItemsController(StoreItemService service)
        {
            this.service = service;
            Mapper.CreateMap<StoreItem, StoreItemViewModel>();
            Mapper.CreateMap<StoreItem, IndexViewModel>();
            Mapper.CreateMap<StoreItemViewModel, StoreItem>();
        }

        // GET: StoreItems
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<IndexViewModel>>(service.GetItems()));
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
                service.AddItem(Mapper.Map<StoreItem>(storeItem));
                return RedirectToAction("Index");
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
                service.EditItem(Mapper.Map<StoreItem>(storeItem));
                return RedirectToAction("Index");
            }
            return View(storeItem);
        }

        // GET: StoreItems/Delete/5
        [Authorize(Roles="admin")]
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
        [Authorize(Roles="admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}
