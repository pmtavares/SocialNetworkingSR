using SocialNetworkingSignalR.Models.Data;
using SocialNetworkingSignalR.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetworkingSignalR.Controllers
{
    public class AccountController : Controller
    {
        // GET: /
        public ActionResult Index()
        {
            //confirm the user is not loggedin
            string username = User.Identity.Name;

            if(!string.IsNullOrEmpty(username))
            {
                return Redirect("~/" + username);
            }

            return View();
        }

        //GET: /{username}
        [Authorize]
        public ActionResult Username(string username = "")
        {
            Db db = new Db();
            if(!db.Users.Any(x => x.Username.Equals(username)))
            {
                return Redirect("~/");
            }

            ViewBag.Username = username;

            
            //get view full name
            string user = User.Identity.Name;
            UserDTO userDTO = db.Users.Where(x => x.Username.Equals(user)).FirstOrDefault();
            ViewBag.FullName = userDTO.FirstName + " " + userDTO.LastName;


            //Get user's id
            int userId = userDTO.Id;

            UserDTO userDTO2 = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();
            ViewBag.ViewFullName = userDTO2.FirstName + " " + userDTO2.LastName;



            ViewBag.UsernameImage = userDTO2.Id + ".jpg";

            //check user type
            ViewBag.UserType = "guest";

            if(username.Equals(user))
            {
                ViewBag.UserType = "owner";
            }

            //Check if they are friends
            if (ViewBag.UserType == "guest")
            {
                UserDTO u1 = db.Users.Where(x => x.Username.Equals(user)).FirstOrDefault();
                int id1 = u1.Id;

                UserDTO u2 = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();
                int id2 = u2.Id;

                FriendDTO f1 = db.Friends.Where(x => x.User1 == id1 && x.User2 == id2).FirstOrDefault();
                FriendDTO f2 = db.Friends.Where(x => x.User1 == id2 && x.User2 == id1).FirstOrDefault();


                if (f1 == null && f2 ==null)
                {
                    ViewBag.NotFriends = "True";
                }
                if (f1 != null)
                {
                    if(!f1.Active)
                    {
                        ViewBag.NotFriends = "Pending";
                    }
                    
                }
                if (f2 != null)
                {
                    if(!f2.Active)
                    {
                        ViewBag.NotFriends = "Pending";
                    }
                    
                }
            }
            //Get friends request count
            var friendCount = db.Friends.Count(x => x.User2 == userId && x.Active == false);
            if(friendCount > 0)
            {
                ViewBag.FRCount = friendCount;
            }
            //Get friend count
            UserDTO uDTO = db.Users.Where(x => x.Username.Equals(username)).FirstOrDefault();
            int usernameId = uDTO.Id;

            var friendCount2 = db.Friends.Count(x => x.User2 == usernameId && x.Active == true || x.User1 == usernameId && x.Active == true);

            ViewBag.FCount = friendCount2;

            //get View bag Message count
            var messageCount = db.Messages.Count(x => x.UserTo == usernameId && x.UserRead == false);
            ViewBag.MsgCount = messageCount;
           

            return View();
        }

        //Get: account/logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect("~/");
        }


        public ActionResult CreateAccount(UserVM model, HttpPostedFileBase file)
        {
            Db db = new Db();

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            //Verify if user exists
            if (db.Users.Any(x => x.Username.Equals(model.Username)))
            {
                ModelState.AddModelError("", "Username " + model.Username + " is taken.");
                model.Username = "";
                return View("Index", model);
            }

            //Create UserDTO
            UserDTO userDTO = new UserDTO()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password,
                EmailAddress = model.EmailAddress

            };

            //Add to DTO
            db.Users.Add(userDTO);
            db.SaveChanges();

            //Get inserted id
            int userId = userDTO.Id;

            //Login User
            FormsAuthentication.SetAuthCookie(model.Username, false);

            //set upload
            var uploadDir = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\")));

            //if file was uploaded
            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();

                if (ext != "image/jpg" && ext != "image/png" && ext != "image/jpeg")
                {
                    ModelState.AddModelError("", "The image was not uploaded. Wrong Extention!");
                    return View("Index", model);
                }


                //image name
                string imageName = userId + ".jpg";

                var path = string.Format("{0}\\{1}", uploadDir, imageName);


                //save image

                file.SaveAs(path);
            }
            return Redirect("~/" + model.Username);
        }


        public ActionResult LoginPartial()
        {
            return PartialView();
        }

        //Post: account/login
        [HttpPost]
        public string LoginPartial(string username, string password)
        {
            Db db = new Db();

            if(db.Users.Any(x => x.Username.Equals(username) && x.Password.Equals(password)))
            {
                //Login
                FormsAuthentication.SetAuthCookie(username, false);
                return "ok";
            }
            else
            {
                return "There was a problem when trying to login";
            }
        }

       
    }
}