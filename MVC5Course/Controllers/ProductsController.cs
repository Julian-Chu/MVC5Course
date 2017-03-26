using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    
    public class ProductsController : BaseController
    {
        //private ProductRepository repoProduct = RepositoryHelper.GetProductRepository();
        
        // GET: Products
        public ActionResult Index(string sortBy, string keyword , int pageNo = 1, string active="")
        {


            //ViewBag.list = new SelectList(new string[] { "True", "False" }); //靜態寫法 

            var items = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToList();
            ViewBag.active = new SelectList(items); 

            DoSearchOnIndex(sortBy, keyword, pageNo,active);
            return View();
        }

        private void DoSearchOnIndex(string sortBy, string keyword, int pageNo, string active)
        {
            var data = repoProduct.All().AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                data = data.Where(p => p.ProductName.Contains(keyword));


            if (sortBy == "+Price")
                data = data.OrderBy(p => p.Price);
            else
                data = data.OrderByDescending(p => p.Price);

            if (!string.IsNullOrEmpty(active))
                data = data.Where(p => p.Active.ToString() == active);

            ViewBag.keyword = keyword;
            ViewBag.pageNo = pageNo;

            //return View(data.ToPagedList(pageNo,5));

            //使用ViewData.Model 等同return View(data.ToPagedList(pageNo,5));
            ViewData.Model = data.ToPagedList(pageNo, 5);
        }

        [HttpPost]
        public ActionResult Index(Product[] data, string sortBy, string keyword, int pageNo = 1, string active="")
        {

            if (ModelState.IsValid)  // 模型驗證後 記得要加!
            {
                
                foreach (var item in data)
                {
                    var prod = repoProduct.Find(item.ProductId);
                    prod.ProductName = item.ProductName;
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                    prod.Active = item.Active;
                }
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            var items = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToList();
            ViewBag.active = new SelectList(items);
            DoSearchOnIndex(sortBy, keyword, pageNo,active);

            return View();


        }
        [AllowAnonymous]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ProductOrderLines(int id)
        {
            Product product = repoProduct.Find(id);
            var Orders = product.OrderLine;
            return View(Orders.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(View = "Error_DbEntityValidationException", ExceptionType = typeof(DbEntityValidationException))]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {
            //if (ModelState.IsValid)
            //{
            //    var db = repoProduct.UnitOfWork.Context;
            //    db.Entry(product).State = EntityState.Modified;
            //    repoProduct.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}

            var product = repoProduct.Find(id);
            if (TryUpdateModel(product, new string[] { "ProductName", "Stock","Active" }))
            {
            }
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            //return View(product);
        }

        // GET: Products/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

    }
}
