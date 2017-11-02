using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace project_ASP.NET.DAL
{
    public class RentalTableHelper
    {
        public static List<Rental> GetRentalList()
        {
            List<Rental> list = new List<Rental>();
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());

            var resultQuery = from rent in nw.Rentals select rent;

            foreach (var item in resultQuery)
            {
                list.Add(new Rental
                {
                    Id = item.Id,
                    CustID = item.CustID,
                    MovieID = item.MovieID
                });
            }
            return list;
        }

        public static void InsertNewRental(string strCustName, string strMovieName)//כותב לטבלה את הנתונים ומחזיר כמה שורות הושפעו
        {
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());

            int getCustId = GetcustomerId(strCustName);
            int getMovieId = GetmovieId(strMovieName);

            Rental rental = new Rental();
            rental.CustID = getCustId;
            rental.MovieID = getMovieId;
            nw.Rentals.InsertOnSubmit(rental);
            nw.SubmitChanges();
        }

        static public bool DoseRentalSucceeded(string CustName, string MovieName)
        {
            bool exist = false;
            int getCustId = GetcustomerId(CustName);
            int getMovieId = GetmovieId(MovieName);
            bool DoesMovieExist = GetRentalList().Exists(x => (x.CustID == getCustId && x.MovieID == getMovieId));

            if (DoesMovieExist)
            {
                exist = true;
            }
            return exist;
        }

        static public void DeleteRentedMovie(string Name, string Movie)
        {
            int getCustId = GetcustomerId(Name);
            int getMovieId = GetmovieId(Movie);

            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());

            var listToBeDeleted = nw.Rentals.Where(x => x.CustID == getCustId && x.MovieID == getMovieId);
            foreach (var item in listToBeDeleted)
            {
                nw.Rentals.DeleteOnSubmit(item);
            }
            nw.SubmitChanges();
        }

        //בודק האם הסרט שהוחזר באמת נמחק מטבלת ההשכרות
        static public bool DoesMovieReturned(string customerName, string movieRented)
        {
            int getCustId = GetcustomerId(customerName);
            int getMovieId = GetmovieId(movieRented);

            bool IsMovieReturned = false;
            bool ExistInFile = GetRentalList().Exists(x => (x.CustID == getCustId && x.MovieID == getMovieId));

            if (!ExistInFile)
            {
                IsMovieReturned = true;
            }
            return IsMovieReturned;
        }

        static public int GetcustomerId(string custName)
        {
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());
            var getCustId = nw.Customers.Where(x => x.Name == custName).FirstOrDefault();

            return getCustId.Id;
        }

        static public int GetmovieId(string movieName)
        {
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());
            var getMovieId = nw.Movies.Where(x => x.Name == movieName).FirstOrDefault();

            return getMovieId.Id;
        }

        //מחזיר רשימה עם שמות הסרטים שהושכרו
        public static List<string> GetMoviesRentedNames()
        {
            List<string> RentalMoviesList = new List<string>();
            foreach (var item in MoviesTabelHelper.GetMoviesList())
            {
                foreach (var item1 in GetRentalList())
                {
                    if (item.Id == item1.MovieID)
                    {
                        RentalMoviesList.Add(item.Name);
                    }
                }
            }
            return RentalMoviesList;
        }

        public static List<string> GetCustomersWhoRented()
        {
            List<string> CustomerNameList = new List<string>();

            foreach (var item in GetRentalList())
            {
                foreach (var item1 in CustomerTabelHelper.GetCustomersList())
                {
                    if (item.CustID == item1.Id)
                    {
                        CustomerNameList.Add(item1.Name);
                    }
                }
            }
            return CustomerNameList;
        }

        //מחזיר את מספר ההשכרות הכולל של הספר
        static public int CountingMovieDuplicates(string Name, string Movie)
        {
            int count = 0;
            var movieList = GetRentalList();

            int getCustId = GetcustomerId(Name);
            int getMovieId = GetmovieId(Movie);

            foreach (var item in movieList)
            {
                foreach (var movie in movieList.GroupBy(x => x))
                {
                    if (item.MovieID == getMovieId && item.CustID == getCustId)
                    {
                        count = movie.Count();
                    }
                }
            }
            return count;
        }
    }
}