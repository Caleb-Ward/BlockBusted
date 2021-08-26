namespace MovieRentals_20020992.Migrations.StoreConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CartID = c.String(),
                        MovieID = c.Int(nullable: false),
                        RentTime = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.MovieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartLines", "MovieID", "dbo.Movies");
            DropIndex("dbo.CartLines", new[] { "MovieID" });
            DropTable("dbo.CartLines");
        }
    }
}
