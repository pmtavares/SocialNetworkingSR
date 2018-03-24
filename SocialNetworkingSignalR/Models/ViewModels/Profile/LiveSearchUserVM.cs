using SocialNetworkingSignalR.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.ViewModels.Profile
{
    public class LiveSearchUserVM
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public LiveSearchUserVM()
        {

        }

        public LiveSearchUserVM(UserDTO user)
        {
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
        }

    }
}