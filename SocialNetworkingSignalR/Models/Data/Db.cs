﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetworkingSignalR.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<FriendDTO> Friends { get; set; }
    }
}