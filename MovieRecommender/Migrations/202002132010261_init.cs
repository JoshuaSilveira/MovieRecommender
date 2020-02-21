namespace MovieRecommender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        ReviewerID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.ReviewerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviewers");
        }
    }
}
