namespace Aphrodite.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Aphrodite.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Aphrodite.Data.DAL.AphroditeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Aphrodite.Data.DAL.AphroditeContext context)
        {
            context.Users.AddOrUpdate(
                new User() { ID = 1, Password = "nathan", UserName = "Admin", Sex = 0, SexualPreference = 1, Age = 19, Location = "Dordrecht", CreatedOn = DateTime.Parse("2012-09-01"), LastLogin = DateTime.Parse("2014-11-03") },
                new User() { ID = 2, Password = "frankie", UserName = "Frankie", Sex = 0, SexualPreference = 1, Age = 22, Location = "Dordrecht", CreatedOn = DateTime.Parse("2012-09-01"), LastLogin = DateTime.Parse("2014-11-03") },
                new User() { ID = 2, Password = "hello", UserName = "Jennifer", Sex = 0, SexualPreference = 1, Age = 20, Location = "Amsterdam", CreatedOn = DateTime.Parse("2012-09-01"), LastLogin = DateTime.Parse("2014-11-03") });  
        }
    }
}
