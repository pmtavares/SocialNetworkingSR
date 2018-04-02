using SocialNetworkingSignalR.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.ViewModels.Profile
{
    public class MessageVM
    {
        public int Id { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public string Message { get; set; }
        public DateTime DataSent { get; set; }
        public bool UserRead { get; set; }

        public int FromId { get; set; }
        public string FromUsername { get; set; }
        public string FromFirstName { get; set; }
        public string FromLastName { get; set; }

        public MessageVM()
        {

        }

        public MessageVM(MessageDTO row)
        {
            Id = row.Id;
            UserFrom = row.UserFrom;
            UserTo = row.UserTo;
            Message = row.Message;
            DataSent = row.DataSent;
            UserRead = row.UserRead;
            FromId = row.FromUsers.Id;
            FromUsername = row.FromUsers.Username;
            FromFirstName = row.FromUsers.FirstName;
            FromLastName = row.FromUsers.LastName;
        }
    }
}