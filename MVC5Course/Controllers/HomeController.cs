﻿using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string ex = "")
        {
            ViewBag.Message = "Your application description page.";
            if(ex == "exception") //http://localhost:52102/Home/About?ex=exception 拋出例外
            {
                throw new Exception(ex);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login(string ReturnUrl="")
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login, string ReturnUrl="")
        {
            
            if (ModelState.IsValid)
            {
                FormsAuthentication.RedirectFromLoginPage("Will", false);
                TempData["LoginViewModel"] = login;

                if (ReturnUrl.StartsWith("/"))
                    return Redirect(ReturnUrl);
                else
                {
                    return RedirectToAction("Index");
                    
                }

                //return Content($"Username:{login.Username}, Passowrd:{login.Password}");
                //return RedirectToAction("Index");
                
            }
            return View();

        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}