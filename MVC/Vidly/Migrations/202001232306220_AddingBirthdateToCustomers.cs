namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBirthdateToCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Birtdate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Birtdate");
        }
    }
}
