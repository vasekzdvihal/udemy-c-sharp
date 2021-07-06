
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EntityFramework.Demo.CodeFirst.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EntityFramework.Demo.CodeFirst.BlogDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}