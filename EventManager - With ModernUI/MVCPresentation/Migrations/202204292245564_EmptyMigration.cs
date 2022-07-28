namespace MVCPresentation.Migrations
{
    // No Name because I was trying to fix an error -  Vinayak
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmptyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entrances",
                c => new
                    {
                        EntranceID = c.Int(nullable: false, identity: true),
                        LocationID = c.Int(nullable: false),
                        EntranceName = c.String(),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EntranceID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PricingInfo = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(),
                        ZipCode = c.String(nullable: false),
                        ImagePath = c.String(),
                        AverageRating = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Locations");
            DropTable("dbo.Entrances");
        }
    }
}
