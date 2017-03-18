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

namespace MVC5Course.Controllers
{
    
    public class ProductsController : BaseController
    {
        //private ProductRepository repoProduct = RepositoryHelper.GetProductRepository();
        
        // GET: Products
        public ActionResult Index(string sortBy, string keyword , int pageNo = 1)
        {
            DoSearchOnIndex(sortBy, keyword, pageNo);
            return View();
        }

        private void DoSearchOnIndex(string sortBy, string keyword, int pageNo)
        {
            var data = repoProduct.All().AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                data = data.Where(p => p.ProductName.Contains(keyword));


            if (sortBy == "+Price")
                data = data.OrderBy(p => p.Price);
            else
                data = data.OrderByDescending(p => p.Price);

            ViewBag.keyword = keyword;
            ViewBag.pageNo = pageNo;

            //return View(data.ToPagedList(pageNo,5));

            //使用ViewData.Model 等同return View(data.ToPagedList(pageNo,5));
            ViewData.Model = data.ToPagedList(pageNo, 5);
        }

        [HttpPost]
        public ActionResult Index(Product[] data, string sortBy, string keyword, int pageNo = 1)
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
            DoSearchOnIndex(sortBy, keyword, pageNo);

            return View();


        }

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
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                var db = repoProduct.UnitOfWork.Context;
                db.Entry(product).State = EntityState.Modified;
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
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
