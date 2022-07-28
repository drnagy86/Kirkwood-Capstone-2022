namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Oddsandends : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Zip", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Zip", c => c.Int(nullable: false));
        }
    }
}
