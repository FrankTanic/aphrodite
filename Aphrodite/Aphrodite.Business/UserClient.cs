using System;
using System.Linq;
using System.Text;
using Default;
using Microsoft.OData.Client;
using Aphrodite.Business;
using Aphrodite.Data.Models;

namespace Aphrodite.Business
{
    public class UserClient
    {
        private Container container;

        public UserClient()
        {
            string serviceUri = "http://localhost:52427/";
            container = new Default.Container(new Uri(serviceUri));
        }

        // Get all the users
        public IQueryable<User> GetUsers()
        {
            return container.User;
        }

        // Save the user

    }
}
