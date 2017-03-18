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
            return View(data); //會掛掉
        }

        public ActionResult Index()
        {
            string data = "123";
            return View((object)data);
        }

        public ActionResult View2()
        {
            return PartialView("Index");  //由controller決定沒有Layout : return PartialView("Index");
        }

        public ActionResult View3()
        {
            return View();          //由View決定沒有Layout  :  Layout = null in View3.cshml;
        }
    }
}