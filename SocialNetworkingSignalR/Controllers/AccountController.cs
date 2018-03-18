using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}