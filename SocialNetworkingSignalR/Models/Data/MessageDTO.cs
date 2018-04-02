using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.Data
{
    [Table("tblMessages")]
    public class MessageDTO
    {
        public int Id { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public string Message { get; set; }
        public DateTime DataSent { get; set; }
        public bool UserRead { get; set; }

        public virtual UserDTO FromUsers { get; set; }
        public virtual UserDTO ToUsers { get; set; }
    }
}