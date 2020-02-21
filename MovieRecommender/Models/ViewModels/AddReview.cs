using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRecommender.Models.ViewModels
{
    public class AddReview
    {
        public List<Movie> movies { get; set; }
        
        [ForeignKey("ReviewerID")]
        public int reviewerID { get; set; }
        public Reviewer Reviewer { get; set; }//the reviewer making the review
        public Review review { get; set; }
    }
}