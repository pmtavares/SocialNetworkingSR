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
        public string Username(string username = "")
        {
            return username;
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

    }
}