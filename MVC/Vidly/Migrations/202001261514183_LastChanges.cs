namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Birtdate", c => c.DateTime());
            AlterColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MembershipTypes", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Birtdate", c => c.DateTime(nullable: false));
        }
    }
}
