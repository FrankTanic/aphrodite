using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aphrodite.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Aphrodite.Data.DAL
{
    class AphroditeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AphroditeContext>
    {
        protected override void Seed(AphroditeContext context)
        {
            var Users = new List<User>
            {
                new User{ID=0,Password="nathan",UserName="Admin",Sex=0,SexualPreference=1,Age=19,Location="Dordrecht",CreatedOn=DateTime.Parse("2012-09-01"),LastLogin=DateTime.Parse("2014-11-03")},
            };
        }
    }
}
