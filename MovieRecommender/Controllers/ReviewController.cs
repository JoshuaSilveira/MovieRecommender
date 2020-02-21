using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRecommender.Models;
using MovieRecommender.Models.ViewModels;

namespace MovieRecommender.Controllers
{
    public class ReviewController : Controller
    {
        private MovieRecommenderContext db = new MovieRecommenderContext();

        /// <summary>
        /// Takes an id so the method knows which reviewer to use as a foriegn key for the movie. uses a viewmodel for the list of movies from the db
        /// and to hold the reviewerid
        /// </summary>
        /// <param name="id">Reviewer ID</param>
        /// <returns>the viewmodel to the view</returns>
        public ActionResult Add(int id)
        {
            List<Movie> movies = db.Movies.SqlQuery("select * from movies").ToList();
            AddReview viewmodel = new AddReview();
            viewmodel.movies = movies;
            viewmodel.reviewerID = id;
            //Debug.WriteLine(id+": is this Correct reviewerid ?");
            return View(viewmodel);
        }

        /// <summary>
        /// Oncce the form is submitted in the Add(Review) View the submitted model is added to the db with the reviewerid from the view as well
        /// </summary>
        /// <param name="review">The model to add to the db</param>
        /// <param name="reviewerid">The id for th reviewer</param>
        /// <returns>Redirects to the Show reviewer page that belongs to the reviewer that made the review</returns>
        [HttpPost]
        public ActionResult Add(Review review,int reviewerid)
        {
            //Debug.WriteLine(review.ReviewerID);
            string query = "insert into reviews (rating,content,ReviewerID,MovieID) values (@rating,@content,@reviewerid,@movieid)";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@rating", review.rating);
            sqlParameters[1] = new SqlParameter("@content", review.content);
            sqlParameters[2] = new SqlParameter("@reviewerid", reviewerid);
            sqlParameters[3] = new SqlParameter("@movieid", review.MovieID);

            db.Database.ExecuteSqlCommand(query, sqlParameters);

            return RedirectToAction("../Reviewer/Show/"+reviewerid);
        }

        /// <summary>
        /// Returns a review to update with the id from the parameter
        /// </summary>
        /// <param name="id">Review ID</param>
        /// <returns>The view with the review from the db with the correct id</returns>
        public ActionResult Update(int id)
        {
            Review review = db.Reviews.SqlQuery("select * from reviews where reviewid =@id", new SqlParameter("@id", id)).FirstOrDefault();

            return View(review);
        }

        /// <summary>
        /// The post method for the update Review method. 
        /// This Update view doesnt use HTML helper so all the paramaters are sent from explicit <input> tags.
        /// </summary>
        /// <param name="content">Movie Review content</param>
        /// <param name="rating">Movie Rating</param>
        /// <param name="reviewerid">The reviewers ID</param>
        /// <param name="movieid">The movie that is being reviewed</param>
        /// <param name="id">The review to update</param>
        /// <returns>Returns to the reviewer that made this review</returns>
        [HttpPost]
        public ActionResult Update(string content,int rating,int reviewerid,int movieid,int id)
        {
            string query = "update reviews set rating=@rating, content=@content where reviewid=@id";
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@rating",rating);
            sqlParameters[1] = new SqlParameter("@content",content);
            sqlParameters[2] = new SqlParameter("@id",id);
            db.Database.ExecuteSqlCommand(query, sqlParameters);
            return RedirectToAction("../Reviewer/Show/" + reviewerid);
        }
        /// <summary>
        /// Deletes the review with this particular id from the db. Since this delete method is called from the showReviewer view we retrieve that
        /// reviewerid so we can go back to that view
        /// </summary>
        /// <param name="id">Review ID</param>
        /// <returns>Goes back to the reviewer that owns this review</returns>
        public ActionResult Delete(int id)
        {   
            Review review = db.Reviews.SqlQuery("select * from reviews where reviewid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            int reviewerid = review.ReviewerID;
            string query = "delete from reviews where reviewid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);
            return RedirectToAction("../Reviewer/Show/" + reviewerid);
        }

        /// <summary>
        /// Shows the review with this id
        /// </summary>
        /// <param name="id">Review ID</param>
        /// <returns>Returns the view with the retrieved review</returns>
        public ActionResult Show(int id)
        {
            Review review = db.Reviews.SqlQuery("select * from reviews where reviewid = @id", new SqlParameter("@id", id)).FirstOrDefault();

            return View(review);
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
