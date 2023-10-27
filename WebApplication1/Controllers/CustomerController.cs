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
        public List<CartDto> cart
        {
            get
            {
                if(Session["cart"] == null)
                {
                    Session["cart"] = new List<CartDto>();
                    return (List<CartDto>)Session["cart"];
                }
                return (List<CartDto>)Session["cart"];
            }
            set
            {
                Session["cart"] = value;
            }
        }
        
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
            CartDto p = new CartDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            cart.Add(p);
            Console.WriteLine(p);
            if(cart != null)
            {
                TempData["msg"] = "Product Added";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Add Failed";
                return RedirectToAction("Index");
            }
            

        }

        public ActionResult Cart()
        {
            var data = cart.ToList();
            double total = 0;
            foreach(var item in cart)
            {
                total += Double.Parse(item.Price); 
            }
            ViewBag.TotalPrice = total;

            return View(data);
        }

        public ActionResult DeleteCart(int id)
        {
            var obj = cart.FirstOrDefault(item => item.Id == id);
            if(obj != null)
            {
                cart.Remove(obj);
                TempData["msg"] = "Item Removed";
                return RedirectToAction("Cart");
            }
            else
            {
                TempData["msg"] = "Remove Failed";
                return RedirectToAction("Cart");
            }
            
        }

        public ActionResult Order()
        {
            var db = new CartSystemEntities();
            var o = new Order()
            {
                CustomerId = (int)Session["Id"],
                Date = DateTime.Now
            };

            db.Orders.Add(o);
            db.SaveChanges();

            foreach(var item in cart)
            {
                var od = new OrderDetail()
                {
                    OrderId = o.Id,
                    ProductId = item.Id,
                    CustomerId = (int)Session["Id"],
                };

                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            cart.Clear();
            TempData["msg"] = "Order Completed";
            return RedirectToAction("Index");
        }

        public ActionResult OrderDetails()
        {
            var customer = (int)Session["Id"];
            var db = new CartSystemEntities();

            var orderDetails = db.OrderDetails.Where(x => x.CustomerId == customer ).ToList();

            return View(orderDetails);
        }


    }
}