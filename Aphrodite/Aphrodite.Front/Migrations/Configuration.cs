namespace Aphrodite.Front.Migrations
{
    using Aphrodite.Front.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Aphrodite.Front.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Aphrodite.Front.Models.ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var hasher = new PasswordHasher();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "frank.vandriel@hotmail.com", Email = "frank.vandriel@hotmail.com", DisplayName = "Franky", BirthDay = DateTime.Parse("16-6-1992"), Gender = Gender.Man, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
                new ApplicationUser { UserName = "josh@lol.com", Email = "josh@lol.com", DisplayName = "Josh", BirthDay = DateTime.Parse("5-2-1980"), Gender = Gender.Man, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
                new ApplicationUser { UserName = "gary@lol.com", Email = "gary@lol.com", DisplayName = "Gary", BirthDay = DateTime.Parse("19-4-1985"), Gender = Gender.Man, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
                new ApplicationUser { UserName = "jake@lol.com", Email = "jake@lol.com", DisplayName = "Jake", BirthDay = DateTime.Parse("12-3-1989"), Gender = Gender.Man, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
                new ApplicationUser { UserName = "henk@lol.com", Email = "henk@lol.com", DisplayName = "Henk", BirthDay = DateTime.Parse("22-7-1980"), Gender = Gender.Man, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },

                new ApplicationUser { UserName = "jerry@lol.com", Email = "jerry@lol.com", DisplayName = "Jerry", BirthDay = DateTime.Parse("19-4-1985"), Gender = Gender.Man, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "billy@lol.com", Email = "billy@lol.com", DisplayName = "Billy", BirthDay = DateTime.Parse("12-3-1989"), Gender = Gender.Man, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "terry@lol.com", Email = "terry@lol.com", DisplayName = "Terry", BirthDay = DateTime.Parse("22-7-1980"), Gender = Gender.Man, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },

                new ApplicationUser { UserName = "behati@lol.com", Email = "behati@lol.com", DisplayName = "Behati", BirthDay = DateTime.Parse("12-3-1990"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "candice@lol.com", Email = "candice@lol.com", DisplayName = "Candice", BirthDay = DateTime.Parse("1-4-1990"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "doutzen@lol.com", Email = "doutzen@lol.com", DisplayName = "Doutzen", BirthDay = DateTime.Parse("16-6-1992"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "adriana@lol.com", Email = "adriana@lol.com", DisplayName = "Adriana", BirthDay = DateTime.Parse("12-3-1990"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "heidi@lol.com", Email = "heidi@lol.com", DisplayName = "Heidi", BirthDay = DateTime.Parse("1-4-1990"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "lauren@lol.com", Email = "lauren@lol.com", DisplayName = "Lauren", BirthDay = DateTime.Parse("12-3-1990"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "Jennifer@lol.com", Email = "Jennifer@lol.com", DisplayName = "Jennifer", BirthDay = DateTime.Parse("1-4-1990"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },
                new ApplicationUser { UserName = "emma@lol.com", Email = "emma@lol.com", DisplayName = "Emma", BirthDay = DateTime.Parse("16-6-1992"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Man, LeftyFlip = 1 },

                new ApplicationUser { UserName = "helga@lol.com", Email = "helga@lol.com", DisplayName = "Helga", BirthDay = DateTime.Parse("16-6-1970"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
                new ApplicationUser { UserName = "renee@lol.com", Email = "renee@lol.com", DisplayName = "Renee", BirthDay = DateTime.Parse("1-4-1972"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
                new ApplicationUser { UserName = "winnie@lol.com", Email = "winnie@lol.com", DisplayName = "Winnie", BirthDay = DateTime.Parse("16-8-1974"), Gender = Gender.Vrouw, SexualPreference = SexualPreference.Vrouw, LeftyFlip = 1 },
            };

            foreach(var user in users)
            {
                manager.Create(user, "Hello0");
            }
        }
    }
}
