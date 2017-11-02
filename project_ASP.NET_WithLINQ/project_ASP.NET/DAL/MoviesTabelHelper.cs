using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;


namespace project_ASP.NET.DAL
{
    public class MoviesTabelHelper
    {
        public static void InsertNewMovie(string strMovieName, string strCategoryName)
        {
            string strConnectionString = DalUtils.GetConnectionString();

            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());

            Movies movie = new Movies();

            movie.Name = strMovieName;
            movie.Category = strCategoryName;
            nw.Movies.InsertOnSubmit(movie);
            nw.SubmitChanges();
        }

        public static List<Movies> GetMoviesList()
        {
            List<Movies> list = new List<Movies>();
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());

            var resultQuery = from movie in nw.Movies select movie;

            foreach (var item in resultQuery)
            {
                list.Add(new Movies
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category
                });
            }
            return list;
        }

        //מחזיר את מספר העותקים הכולל של ספר אחד
        static public int CountingMovieDuplicates(string Name, string Category)
        {
            int count = 0;

            var movieList = GetMoviesList();

            foreach (var item in movieList)
            {
                foreach (var movie in movieList.GroupBy(x => x))
                {
                    if (item.Name == Name && item.Category == Category)
                    {
                        count = movie.Count();
                    }
                }
            }
            return count;
        }
    }
}
