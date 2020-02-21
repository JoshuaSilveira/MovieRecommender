using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommender.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        public string name { get; set; }

        public string info { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}