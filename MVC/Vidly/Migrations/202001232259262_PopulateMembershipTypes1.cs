namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypes1 : DbMigration
    {
        public override void Up()
        {
            Sql(
                "UPDATE MembershipTypes set Name = (CASE WHEN Id = 1 THEN 'Pay as You Go' WHEN Id = 2 THEN 'Monthly' WHEN Id = 3 THEN 'Quarterly' WHEN Id = 4 THEN 'Annual' END); ");
        }

        public override void Down()
        {
        }
    }
}
