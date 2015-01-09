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
using AutoMapper;
using GroceryStoreInventory.ViewModels.ReceivingItems;

namespace GroceryStoreInventory.Controllers
{
    [Authorize(Roles="Owner,Employee")]
    public class ReceivingItemsController : Controller
    {
        private ReceivingItemService service;

        public ReceivingItemsController(ReceivingItemService service)
        {
            this.service = service;
            Mapper.CreateMap<ReceivingItem, ReceivingItemViewModel>();
            Mapper.CreateMap<ReceivingItemViewModel, ReceivingItem>();
        }

        // GET: ReceivingItems
        public ActionResult Index()
        {
            var items = Mapper.Map<IEnumerable<ReceivingItemViewModel>>(service.GetItems());
            return View(items);
        }

        // GET: ReceivingItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceivingItemViewModel receivingItem = Mapper.Map<ReceivingItemViewModel>(service.FindById(id));
            if (receivingItem == null)
            {
                return HttpNotFound();
            }
            return View(receivingItem);
        }

        // GET: ReceivingItems/Create
        public ActionResult Create()
        {
            ViewBag.StoreItemId = new SelectList(service.GetStoreItems(), "Id", "Name");
            return View(new ReceivingItemViewModel { DateReceived = DateTime.Today });
        }

        // POST: ReceivingItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoreItemId,Invoice,DateReceived,QuantityReceived")] ReceivingItemViewModel receivingItem)
        {
            if (ModelState.IsValid)
            {
                var mappedReceivingItem = Mapper.Map<ReceivingItem>(receivingItem);
                service.AddItem(mappedReceivingItem);
                return RedirectToAction("Index");
            }

            ViewBag.StoreItemId = new SelectList(service.GetStoreItems(), "Id", "Name", receivingItem.StoreItemId);
            return View(receivingItem);
        }

        // GET: ReceivingItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceivingItemViewModel receivingItem = Mapper.Map<ReceivingItemViewModel>(service.FindById(id));
            if (receivingItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreItemId = new SelectList(service.GetStoreItems(), "Id", "Name", receivingItem.StoreItemId);
            return View(receivingItem);
        }

        // POST: ReceivingItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoreItemId,Invoice,DateReceived,QuantityReceived")] ReceivingItemViewModel receivingItem)
        {
            if (ModelState.IsValid)
            {
                var mapReceivingItem = Mapper.Map<ReceivingItem>(receivingItem);
                service.EditItem(mapReceivingItem);
                return RedirectToAction("Index");
            }
            ViewBag.StoreItemId = new SelectList(service.GetStoreItems(), "Id", "Name", receivingItem.StoreItemId);
            return View(receivingItem);
        }

        // GET: ReceivingItems/Delete/5
        [Authorize(Roles="Owner")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceivingItemViewModel receivingItem = Mapper.Map<ReceivingItemViewModel>(service.FindById(id));
            if (receivingItem == null)
            {
                return HttpNotFound();
            }
            return View(receivingItem);
        }

        // POST: ReceivingItems/Delete/5
        [Authorize(Roles = "Owner")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.DeleteById(id);
            return RedirectToAction("Index");
        }

    }
}
