namespace MovieRentals_20020992.Migrations.StoreConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twocontextreset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                        ReleaseYear = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Int(nullable: false),
                        GenreID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.GenreID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.MovieImageMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageNumber = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                        MovieImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.MovieImages", t => t.MovieImageID, cascadeDelete: true)
                .Index(t => t.MovieID)
                .Index(t => t.MovieImageID);
            
            CreateTable(
                "dbo.MovieImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.FileName, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieImageMappings", "MovieImageID", "dbo.MovieImages");
            DropForeignKey("dbo.MovieImageMappings", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Movies", "GenreID", "dbo.Genres");
            DropIndex("dbo.MovieImages", new[] { "FileName" });
            DropIndex("dbo.MovieImageMappings", new[] { "MovieImageID" });
            DropIndex("dbo.MovieImageMappings", new[] { "MovieID" });
            DropIndex("dbo.Movies", new[] { "GenreID" });
            DropTable("dbo.MovieImages");
            DropTable("dbo.MovieImageMappings");
            DropTable("dbo.Movies");
            DropTable("dbo.Genres");
        }
    }
}
