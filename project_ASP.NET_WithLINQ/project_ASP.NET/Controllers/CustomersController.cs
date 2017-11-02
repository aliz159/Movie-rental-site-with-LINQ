using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_ASP.NET.DAL;

namespace project_ASP.NET.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult Index()
        {
            List<Customer> list = CustomerTabelHelper.GetCustomersList();

            return View(list);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string userName, int userAge, string subscription)
        {
            if(ModelState.IsValid)
            {
                if (CustomerTabelHelper.DoseCustomerExists(userName))
                {
                    TempData["errorName"] = "Name already exist, try another one";
                    return View("New");
                }
                else
                {
                    CustomerTabelHelper.InsertNewCustomer(userName, userAge, subscription);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["error"] = "You must fill in all the fields!!";
                return View("New");
            }
        }
    }
}