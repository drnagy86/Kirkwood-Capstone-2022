namespace MVCPresentation.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class notsure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Availabilities",
                c => new
                {
                    AvailabilityID = c.Int(nullable: false, identity: true),
                    ForeignID = c.Int(nullable: false),
                    DateID = c.DateTime(),
                    TimeStart = c.DateTime(),
                    TimeEnd = c.DateTime(),
                    Sunday = c.Boolean(nullable: false),
                    Monday = c.Boolean(nullable: false),
                    Tuesday = c.Boolean(nullable: false),
                    Wednesday = c.Boolean(nullable: false),
                    Thursday = c.Boolean(nullable: false),
                    Friday = c.Boolean(nullable: false),
                    Saturday = c.Boolean(nullable: false),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.AvailabilityID);

        }

        public override void Down()
        {
            DropTable("dbo.Availabilities");
        }
    }
}
