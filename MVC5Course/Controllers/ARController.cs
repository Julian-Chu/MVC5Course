using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index1()
        {
            string data = "123";
            return View(data); //會掛掉, 抓到參數為string ViewName 非object
        }

        public ActionResult Index()
        {
            string data = "123";
            return View((object)data);  //強制轉型
        }

        public ActionResult View2()
        {
            return PartialView("Index");  //由controller決定沒有Layout : return PartialView("Index");
        }

        public ActionResult View3()
        {
            return View();          //由View決定沒有Layout  :  Layout = null in View3.cshml;
        }

        public ActionResult Content1()
        {
            return Content("<script>alert('新增成功');location.href='/';</script>");
            //利用content回傳javascript ==> 不好寫法
        }

        public ActionResult File1()
        {
            //return File(@"C:\Projects\MVC5Course\MVC5Course\Content\251178_medium.png", "image/png");
            return File(Server.MapPath(@"~/Content/\251178_medium.png"), "imag/png");
        }

        public ActionResult File2()
        {
            //return File(@"C:\Projects\MVC5Course\MVC5Course\Content\251178_medium.png", "image/png");
            return File(Server.MapPath(@"~/Content/\251178_medium.png"), "imag/png", "downloadedFile");
        }
    }
}