using SocialNetworkingSignalR.Models.Data;
using SocialNetworkingSignalR.Models.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetworkingSignalR.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LiveSearch(string search)
        {
            Db db = new Db();

            //create list
            List<LiveSearchUserVM> usernames = db.Users
                .Where(x => x.Username.Contains(search) && x.Username != User.Identity.Name)
                .ToArray().Select(x => new LiveSearchUserVM(x)).ToList();

            return Json(usernames);
        }

        [HttpPost]
        public void AddFriend(string friend)
        {
            Db db = new Db();

            //get User id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;

            //friend ID
            UserDTO userDTO2 = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            int friendId = userDTO2.Id;

            //Add DTO
            FriendDTO friendDTO = new FriendDTO();

            friendDTO.User1 = userId;
            friendDTO.User2 = friendId;

            db.Friends.Add(friendDTO);
            db.SaveChanges();

        }
        [HttpPost]
        public JsonResult DisplayFriendRequest()
        {
            //Init DB
            Db db = new Db();
            //Get user id

            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;
            //Create list
            List<FriendRequestVM> list = db.Friends.Where(x => x.User2 == userId && x.Active == false).ToArray()
                .Select(x => new FriendRequestVM(x)).ToList();
            //init lis of users
            List<UserDTO> users = new List<UserDTO>();

            foreach(var item in list)
            {
                var user = db.Users.Where(x => x.Id == item.User1).FirstOrDefault();
                users.Add(user);
            }

            return Json(users);
        }

        //Accept frinds request
        [HttpPost]
        public void AcceptFriendRequest( int friendId)
        {

            //Init DB
            Db db = new Db();
            //Get user id

            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;

            FriendDTO friendDTO = db.Friends.Where(x => x.User1 == friendId && x.User2 == userId).FirstOrDefault();

            friendDTO.Active = true;

            db.SaveChanges();

        }
        [HttpPost]
        public void DeclineFriendRequest(int friendId)
        {
            //Init DB
            Db db = new Db();
            //Get user id

            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;


            //delete friend request

            FriendDTO friendDTO = db.Friends.Where(x => x.User1 == friendId && x.User2 == userId).FirstOrDefault();

            db.Friends.Remove(friendDTO);

            db.SaveChanges();
        }
        [HttpPost]
        public void SendMessage(string friend, string message)
        {
            //Init DB
            Db db = new Db();
            //Get user id

            //get user id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;
            //Get friend id
            UserDTO userDTO2 = db.Users.Where(x => x.Username.Equals(friend)).FirstOrDefault();
            int userId2 = userDTO2.Id;

            //save message
            MessageDTO msgDto = new MessageDTO {
                UserFrom = userId,
                UserTo = userId2,
                Message = message,
                DataSent = DateTime.Now,
                UserRead = false

            };

            db.Messages.Add(msgDto);
            db.SaveChanges();



        }

        //Unread messages
        [HttpPost]
        public JsonResult DisplayUnreadMessages()
        {
            //Init DB
            Db db = new Db();
            //Get user id

            //get user id
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = userDTO.Id;

            List<MessageVM> list = db.Messages.Where(x => x.UserTo ==userId && x.UserRead == false).ToArray().Select(x =>
                new MessageVM(x)).ToList();

            //Make unread read
            db.Messages.Where(x => x.UserTo == userId && x.UserRead == false).ToList().ForEach(x => x.UserRead = true);
            db.SaveChanges();

            return Json(list);


        }
        [HttpPost]
        public void UpdateWallMessage(int id, string message)
        {
            //Init DB
            Db db = new Db();
            //Get user id

            WallDTO wall = db.Walls.Find(id);

            wall.Message = message;
            wall.DateEdited = DateTime.Now;

            db.SaveChanges();
        }
    }
}