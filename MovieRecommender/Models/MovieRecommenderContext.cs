using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieRecommender.Models
{
    public class MovieRecommenderContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MovieRecommenderContext() : base("name=MovieRecommenderContext")
        {
        }

        public System.Data.Entity.DbSet<MovieRecommender.Models.Reviewer> Reviewers { get; set; }

        public System.Data.Entity.DbSet<MovieRecommender.Models.Movie> Movies { get; set; }

        public System.Data.Entity.DbSet<MovieRecommender.Models.Review> Reviews { get; set; }
    }
}
