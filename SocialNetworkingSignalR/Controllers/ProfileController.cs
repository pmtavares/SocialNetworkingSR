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
    }
}