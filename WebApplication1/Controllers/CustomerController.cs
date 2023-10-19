using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var db = new CartSystemEntities();

           var data =  db.Products.ToList();

            return View(data);
        }

        public ActionResult AddToCart(int id)
        {
            var db = new CartSystemEntities();

            var product = db.Products.Find(id);

            Session["ProductId"] = product.Id;
            Session["ProductName"] = product.Name;
            Session["ProductPrice"] = product.Price;

            if(Session["ProductId"]!=null)
            {
                TempData["msg"] = "Product Added";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Product Addition Failed";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Cart()
        {
            CartDto cart = new CartDto()
            {
                Id = (int)Session["ProductId"],
                Name = Session["ProductName"].ToString(),
                Price = (double)Session["ProductPrice"]
            };

            return View(cart);
        }
    }
}