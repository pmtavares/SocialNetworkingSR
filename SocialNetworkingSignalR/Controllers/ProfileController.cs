﻿using SocialNetworkingSignalR.Models.Data;
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
    }
}