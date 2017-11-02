using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_ASP.NET.DAL;

namespace project_ASP.NET.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            List<Movies> list = MoviesTabelHelper.GetMoviesList();

            return View(list);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string movieName, string Category)
        {
            if (ModelState.IsValid)
            {
                MoviesTabelHelper.InsertNewMovie(movieName, Category);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "You must fill in all the fields!!";
                return View("New");
            }
        }            
    }
}