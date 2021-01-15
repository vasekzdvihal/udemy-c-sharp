namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'35dfea98-0b11-49e9-a058-beab5abd0826', N'admin@vidly.com', 0, N'APBSgfGbqc8C1CRHqX+HV2fV5143TXy0HndMrtxVK2ueKg6SLpV1onLl/KFAnlAzRw==', N'df26db32-d741-4a5f-9850-4aa37ea68564', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4da03f16-42fd-4d30-84f4-e8223f054235', N'guest@vidly.com', 0, N'AB30/Ti+07LesOBUwyxKMPtQLTktjd/TtpYLPzZFSFbj8JbW0+kqIJgJPigUhqEDhg==', N'c247fd37-cb91-4b81-b92a-8aabd447b2e0', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f3477553-60d8-4f76-9ea0-8e3a0148c97e', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'35dfea98-0b11-49e9-a058-beab5abd0826', N'f3477553-60d8-4f76-9ea0-8e3a0148c97e')
");
        }
        
        public override void Down()
        {
        }
    }
}
