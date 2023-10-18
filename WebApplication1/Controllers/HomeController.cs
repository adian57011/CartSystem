using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(CustomerDto obj)
        {
            var db = new CartSystemEntities();

            if (ModelState.IsValid)
            {
                Customer c = new Customer();
                c.Name = obj.Name;
                c.Email = obj.Email;
                c.Password = obj.Password;
    

                db.Customers.Add(c);
                db.SaveChanges();
                TempData["msg"] = "User Added!";
                return RedirectToAction("Login", "Home");
            }

            else
            {
                TempData["msg"] = "Signup failed";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDto obj)
        {
            var db = new CartSystemEntities();

            var customer = (from c in db.Customers
                            where c.Email.Equals(obj.Email) &&
                            c.Password.Equals(obj.Password)
                            select c).SingleOrDefault();

            if(customer != null)
            {
                Session["Id"] = customer.Id;
                Session["Name"] = customer.Name;
                Session["Email"] = customer.Email;

                return RedirectToAction("Index", "Customer");
            }

            else
            {
                TempData["msg"] = "Login Failed!";
                return View(obj);
            }



        }
    }
}