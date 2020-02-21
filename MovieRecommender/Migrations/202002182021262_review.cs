namespace MovieRecommender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        rating = c.Int(nullable: false),
                        content = c.String(),
                        ReviewerID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Reviewers", t => t.ReviewerID, cascadeDelete: true)
                .Index(t => t.ReviewerID)
                .Index(t => t.MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ReviewerID", "dbo.Reviewers");
            DropForeignKey("dbo.Reviews", "MovieID", "dbo.Movies");
            DropIndex("dbo.Reviews", new[] { "MovieID" });
            DropIndex("dbo.Reviews", new[] { "ReviewerID" });
            DropTable("dbo.Reviews");
        }
    }
}
