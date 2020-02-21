using System.Collections.Generic;

namespace MovieRecommender.Models.ViewModels
{
    public class ShowReviewer
    {
        //The List of reviews that this reviewer has completed
        public List<Review> Reviews { get; set; }
        public Reviewer Review { get; set; }
    }
}