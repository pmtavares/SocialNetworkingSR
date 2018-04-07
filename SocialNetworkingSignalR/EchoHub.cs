using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using SocialNetworkingSignalR.Models.Data;
using System.Web.Mvc;

namespace SocialNetworkingSignalR
{
    [HubName("echo")]
    public class EchoHub : Hub
    {
        public void Hello(string message)
        {
            //Clients.All.hello();
            Trace.WriteLine(message);

            var clients = Clients.All;

            clients.test("This is a test");
        }

        public void Notify(string friend)
        {
            //init db
            Db db = new Db();

            //get friend id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            int friendId = userDTO.Id;

            //get fr count
            var frcount = db.Friends.Count(x => x.User2 == friendId && x.Active == false);

            //set clients
            var clients = Clients.Others;

            //call js function
            clients.frnotify(friend, frcount);

        }

        public void getFrCount()
        {
            // init db
             Db db = new Db();

            //get friend id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;

            //get fr count
            var friendReqCount = db.Friends.Count(x => x.User2 == userId && x.Active == false);

            //set clients
            var clients = Clients.Caller;


            //call js function
            clients.frcount(Context.User.Identity.Name, friendReqCount);
        }

        public void GetFCount(int friendId)
        {
            // init db
            Db db = new Db();

            //get user id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;

            //Get friend count for user
            var friendCount1 = db.Friends.Count(x => x.User2 == userId && x.Active == true || x.User1 == userId && x.Active == true);

            //Get user2 username
            UserDTO userDTO2 = db.Users.Where(x => x.Id == friendId).FirstOrDefault();
            string username = userDTO2.Username;

            //Get friend count for user
            var friendCount2 = db.Friends.Count(x => x.User2 == friendId && x.Active == true || x.User1 == friendId && x.Active == true);

            //set clients
            var clients = Clients.All;


            //call js function
            clients.fcount(Context.User.Identity.Name, username, friendCount1, friendCount2);


        }

        public void NotifyOfMessage(string friend)
        {
            // init db
            Db db = new Db();

            //get user id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            int friendId = userDTO.Id;

            //Get message count
            var messageCount = db.Messages.Count(x => x.UserTo == friendId && x.UserRead == false);

            //set clients
            var clients = Clients.Others;


            //call js function
            clients.msgcount(friend, messageCount);
        }

        public void NotifyOfMessageOwner()
        {
            Db db = new Db();

            //get user id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(Context.User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;

            //Get message count
            var messageCount = db.Messages.Count(x => x.UserTo == userId && x.UserRead == false);

            //set clients
            var clients = Clients.Caller;


            //call js function
            clients.msgcount(Context.User.Identity.Name, messageCount);
        }
    }
}