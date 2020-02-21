using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRecommender.Models;

namespace MovieRecommender.Controllers
{
    public class MovieController : Controller
    {
        private MovieRecommenderContext db = new MovieRecommenderContext();


        /// <summary>
        /// return the view for the route /Movie/Add
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        /// <summary>
        /// Takes a Movie model from the add page after the form has been submitted the name and 
        /// info columns are set via sql parameters from the movie object fields
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>N/A redirects to Movie List</returns>
        public ActionResult Add(Movie movie)
        {
            string query = "insert into movies(name,info) values (@MovieName,@info)";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@MovieName", movie.name);
            sqlparams[1] = new SqlParameter("@info", movie.info);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

   
        /// <summary>
        ///  The Movie list method that takes a search string. if the string is empty then just retrieve the list of movies like usual. 
        ///  However if the string has a value set then the query has a where clause and the string appended too it.
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns>The View with the List of Movies</returns>
        public ActionResult List(string SearchText)
        {
            string query = "Select * from movies";
            if (SearchText != "")
            {
                query = query + " where name like '%" + SearchText + "%'";
            }
            List<Movie> movies = db.Movies.SqlQuery(query).ToList();
            return View(movies);
        }


        /// <summary>
        /// returns a movie with the id provided in the parameter. this movie's information populates the text boxes in the view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The View with the movie retrieved from the db</returns>
        public ActionResult Update(int id)
        {
            Movie movie = db.Movies.SqlQuery("select * from movies where movieid =@id", new SqlParameter("@id", id)).FirstOrDefault();
            if (movie == null)
            {
                return HttpNotFound();
            }
            //TO-DO Viewmodel for reviews per reviewer
            return View(movie);
        }

        /// <summary>
        /// takes the movie from the view after the update form has been submitted. Also takes an id for the movie that needs updating
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="id"></param>
        /// <returns>Redirects to List of Movie</returns>
        [HttpPost]
        public ActionResult Update(Movie movie,int id)
        {

            string query = "update movies set name = @name, info = @info where movieid = @id";

            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@name", movie.name);
            sqlparams[1] = new SqlParameter("@id", movie.MovieID);
            sqlparams[2] = new SqlParameter("@info", movie.info);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }



        /// <summary>
        /// Deletes the movie at the this id and then redirects to the List view
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirects to List of Movie</returns>
        public ActionResult Delete(int id)
        {
            string query = "delete from movies where movieid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);
            return RedirectToAction("List");
        }


        /// <summary>
        /// Sends the id of the movie to display
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the View iwht hte movie from the db with an id from the parameter</returns>
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = db.Movies.SqlQuery("select * from movies where movieid =@id", new SqlParameter("@id", id)).FirstOrDefault();
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

    

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
