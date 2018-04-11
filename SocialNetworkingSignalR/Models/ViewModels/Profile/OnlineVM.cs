using SocialNetworkingSignalR.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.ViewModels.Profile
{
    public class OnlineVM
    {
        public int Id { get; set; }
        public string ConnId { get; set; }

        public OnlineVM()
        {

        }
        public OnlineVM(OnlineDTO row)
        {
            Id = row.Id;
            ConnId = row.ConnId;
        }
    }
}