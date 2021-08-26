namespace MovieRentals_20020992.Migrations.StoreConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        MovieID = c.Int(),
                        RentTime = c.Int(nullable: false),
                        MovieTitle = c.String(),
                        WeeklyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        DeliveryName = c.String(),
                        DeliveryAddress_AddressLine1 = c.String(nullable: false),
                        DeliveryAddress_AddressLine2 = c.String(),
                        DeliveryAddress_Town = c.String(nullable: false),
                        DeliveryAddress_Country = c.String(nullable: false),
                        DeliveryAddress_PostCode = c.String(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLines", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "MovieID", "dbo.Movies");
            DropIndex("dbo.OrderLines", new[] { "MovieID" });
            DropIndex("dbo.OrderLines", new[] { "OrderID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
        }
    }
}
