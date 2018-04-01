using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using SocialNetworkingSignalR.Models.Data;

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
    }
}