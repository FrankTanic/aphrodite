﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Aphrodite.Front.Models;

namespace Aphrodite.Front.Hubs
{
    [Authorize]
    public class MatchesHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void SendCountMatches()
        {
            int count = GetMatchCount();
            string userId = Context.User.Identity.GetUserId();

            if (count == 1)
            {
                Clients.Group(userId).addNotification("Je hebt een nieuwe match!");
            }
            else
            {
                if(count > 1)
                {
                    Clients.Group(userId).addNotification("Je hebt nieuwe matches!");
                }
            }

            Clients.Group(userId).addCount(count);
        }

        public override Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();

            Groups.Add(Context.ConnectionId, userId);

            return base.OnConnected();
        }

        public int GetMatchCount()
        {
            string userId = Context.User.Identity.GetUserId();

            int matchCount = (from mine in db.Matches
                              from theirs in db.Matches
                              from name in db.Users
                              where mine.SenderId == userId && theirs.ReceiverId == userId && mine.ReceiverId == theirs.SenderId && mine.Approve == 1 && theirs.Approve == 1 && name.Id == theirs.SenderId
                              select new matches
                              {
                                  Id = theirs.SenderId,
                              }).Count();

            Clients.Group(userId).addCount(matchCount);

            return (matchCount);
        }

    }
}