using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_ASP.NET.DAL
{
    public class DalUtils
    {
        public static string GetConnectionString()
        {
            return @"Data Source = (LocalDB)\MSSQLLocalDB;" +
            @"AttachDbFilename = |DataDirectory|\RentMovieDB.mdf;" +
            "Initial Catalog = RentMovieDB;" +
            "Integrated Security = True;";
        }
    }
}