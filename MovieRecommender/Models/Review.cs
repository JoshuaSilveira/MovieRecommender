using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommender.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int rating { get; set; }
        public string content { get; set; }
        public int ReviewerID { get; set; }
        public int MovieID { get; set; }
        
        
        [ForeignKey("ReviewerID")]
        public virtual Reviewer Reviewer{ get; set; }
        
        [ForeignKey("MovieID")]
        public virtual Movie Movie { get; set; }


    }
}