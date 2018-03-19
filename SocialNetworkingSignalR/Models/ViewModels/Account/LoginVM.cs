using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        [DisplayName("Username")]
        public string UsernameLogin { get; set; }
        [Required]
        [DisplayName("Password")]
        public string PasswordLogin { get; set; }
    }
}