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
                new ApplicationUser { UserName = "frank.vandriel@hotmail.com", Email = "frank.vandriel@hotmail.com", DisplayName = "Franky", BirthDay = new DateTime(1992, 6, 16), Gender = Gender.Male, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
                new ApplicationUser { UserName = "josh@lol.com", Email = "josh@lol.com", DisplayName = "Josh", BirthDay = new DateTime(1980, 2, 5), Gender = Gender.Male, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
                new ApplicationUser { UserName = "gary@lol.com", Email = "gary@lol.com", DisplayName = "Gary", BirthDay = new DateTime(1985, 4, 19), Gender = Gender.Male, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
                new ApplicationUser { UserName = "jake@lol.com", Email = "jake@lol.com", DisplayName = "Jake", BirthDay = new DateTime(1989, 3, 12), Gender = Gender.Male, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
                new ApplicationUser { UserName = "henk@lol.com", Email = "henk@lol.com", DisplayName = "Henk", BirthDay = new DateTime(1980, 7, 22), Gender = Gender.Male, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },

                new ApplicationUser { UserName = "jerry@lol.com", Email = "jerry@lol.com", DisplayName = "Jerry", BirthDay = new DateTime(1985, 4, 19), Gender = Gender.Male, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "billy@lol.com", Email = "billy@lol.com", DisplayName = "Billy", BirthDay = new DateTime(1989, 3, 12), Gender = Gender.Male, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "terry@lol.com", Email = "terry@lol.com", DisplayName = "Terry", BirthDay = new DateTime(1980, 7, 22), Gender = Gender.Male, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },

                new ApplicationUser { UserName = "behati@lol.com", Email = "behati@lol.com", DisplayName = "Behati", BirthDay = new DateTime(1990, 3, 12), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "candice@lol.com", Email = "candice@lol.com", DisplayName = "Candice", BirthDay = new DateTime(1990, 4, 1), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "doutzen@lol.com", Email = "doutzen@lol.com", DisplayName = "Doutzen", BirthDay = new DateTime(1992, 6, 16), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "adriana@lol.com", Email = "adriana@lol.com", DisplayName = "Adriana", BirthDay = new DateTime(1990, 3, 12), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "heidi@lol.com", Email = "heidi@lol.com", DisplayName = "Heidi", BirthDay = new DateTime(1990, 4, 1), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "lauren@lol.com", Email = "lauren@lol.com", DisplayName = "Lauren", BirthDay = new DateTime(1990, 3, 12), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "Jennifer@lol.com", Email = "Jennifer@lol.com", DisplayName = "Jennifer", BirthDay = new DateTime(1990, 4, 1), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },
                new ApplicationUser { UserName = "emma@lol.com", Email = "emma@lol.com", DisplayName = "Emma", BirthDay = new DateTime(1992, 6, 16), Gender = Gender.Female, SexualPreference = SexualPreference.Male, LeftyFlip = 1 },

                new ApplicationUser { UserName = "helga@lol.com", Email = "helga@lol.com", DisplayName = "Helga", BirthDay = new DateTime(1970, 6, 16), Gender = Gender.Female, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
                new ApplicationUser { UserName = "renee@lol.com", Email = "renee@lol.com", DisplayName = "Renee", BirthDay = new DateTime(1972, 4, 1), Gender = Gender.Female, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
                new ApplicationUser { UserName = "winnie@lol.com", Email = "winnie@lol.com", DisplayName = "Winnie", BirthDay = new DateTime(1974, 8, 16), Gender = Gender.Female, SexualPreference = SexualPreference.Female, LeftyFlip = 1 },
            };

            foreach(var user in users)
            {
                manager.Create(user, "Hello0");
            }
        }
    }
}
