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
    public class ReviewerController : Controller
    {
        private MovieRecommenderContext db = new MovieRecommenderContext();

        /// <summary>
        /// Shows a List of Reviewers or the searched reviewer.
        /// </summary>
        /// <param name="SearchText">String that matches a reviewers name</param>
        /// <returns>returns a list of reviewer(s) to the view</returns>
        public ActionResult List(string SearchText)
        {
            string query = "Select * from reviewers";
            if (SearchText != "")
            {
                query = query + " where name like '%" + SearchText+"%'";
            }
            List<Reviewer> reviewers = db.Reviewers.SqlQuery(query).ToList();
            return View(reviewers);
        }
        /// <summary>
        /// shows the add view for a reviewer
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Since the correspoding Add View uses HTMLhelper the a reviewer is set as the parameter instead of variables from input tags
        /// </summary>
        /// <param name="reviewer">The reviewer to add to the db</param>
        /// <returns>Redirects to the List of reviewers</returns>
        [HttpPost]
        public ActionResult Add(Reviewer reviewer)
        {
            //Debug.WriteLine(reviewer.name);
            string query = "insert into reviewers(name) values (@ReviewerName)";
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@ReviewerName", reviewer.name);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        /// <summary>
        /// creates a view to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the view with the reviewer model</returns>
        public ActionResult Update(int id)
        {
            Reviewer reviewer = db.Reviewers.SqlQuery("select * from reviewers where reviewerid =@id", new SqlParameter("@id", id)).FirstOrDefault();
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            return View(reviewer);
        }
        /// <summary>
        /// Updates the reviewer passed in the parameter
        /// </summary>
        /// <param name="reviewer"></param>
        /// <returns>Redirects to the list of Reviewers</returns>
        [HttpPost]
        public ActionResult Update(Reviewer reviewer)
        {

            string query = "update reviewers set name = @name where reviewerid = @id";

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@name", reviewer.name);
            sqlparams[1] = new SqlParameter("@id",reviewer.ReviewerID );

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Shows a Reviewer and the list of thier Reviews. Then they are placed in a viewmodel so the View can display both
        /// </summary>
        /// <param name="id">Reviewer ID</param>
        /// <returns>Viewmodel that contains the list of reviews and the reviewer info</returns>
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Review> Reviews = db.Reviews.SqlQuery("select * from reviews where reviewerid = @id", new SqlParameter("@id", id)).ToList();
            Reviewer reviewer = db.Reviewers.SqlQuery("select * from reviewers where reviewerid =@id", new SqlParameter("@id", id)).FirstOrDefault();
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            ShowReviewer viewmodel = new ShowReviewer();
            viewmodel.Review = reviewer;
            viewmodel.Reviews = Reviews;
            return View(viewmodel);
        }



        //Deletes the reviewer with this id
        public ActionResult Delete(int id)
        {
            string query = "delete from reviewers where reviewerid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);
            return RedirectToAction("List");
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
