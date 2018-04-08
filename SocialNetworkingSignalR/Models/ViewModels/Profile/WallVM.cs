using SocialNetworkingSignalR.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.ViewModels.Profile
{
    public class WallVM
    {

        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateEdited { get; set; }

        public WallVM()
        {

        }

        public WallVM(WallDTO wall)
        {
            Id = wall.Id;
            Message = wall.Message;
            DateEdited = wall.DateEdited;
        }
    }
}