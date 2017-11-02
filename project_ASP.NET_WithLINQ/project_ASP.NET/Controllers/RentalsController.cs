using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_ASP.NET.DAL;

namespace project_ASP.NET.Controllers
{
    public class RentalsController : Controller
    {
        string ErrorMsg = "";

        public ActionResult New()
        {
            return View();
        }
   
        public ActionResult ReturnMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string name, string movie)
        {
            ViewBag.ErrorMsg = "";
            int getCustId = RentalTableHelper.GetcustomerId(name);
            int getMovieId = RentalTableHelper.GetmovieId(movie);

            bool ExistInFile = RentalTableHelper.GetRentalList().Exists(x => (x.CustID == getCustId && x.MovieID == getMovieId));

            if (ExistInFile)
            {
                RentalTableHelper.DeleteRentedMovie(name, movie);

                if (RentalTableHelper.DoesMovieReturned(name, movie))
                {
                    ViewBag.success = "Movie returned successful!";
                }
                else
                {
                    ViewBag.ErrorMsg = "There seems to be a problem, Failed to return movie, try again";
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Oops... The customer didn't rent this movie!";
            }
            return View("ReturnMovie");
        }

        [HttpPost]
        public ActionResult InsertNewRental(string name, string movie)
        {
            bool boolean = true;

            if (RentalTableHelper.CountingMovieDuplicates(name, movie) <= MoviesTabelHelper.CountingMovieDuplicates(name, movie))
            {
                RentalTableHelper.InsertNewRental(name, movie);

                if (RentalTableHelper.DoesMovieReturned(name, movie))
                {
                    ErrorMsg = "There seems to be a problem, the movie rental has not been made, try again";
                    boolean = true;
                }
                else
                {
                    ErrorMsg = "Movie rental successful!";
                    boolean = false;
                }
            }
            var ReqOfRent = new { strMsg = ErrorMsg, Isbool = boolean };
            return Json(ReqOfRent);
        }
    }
}