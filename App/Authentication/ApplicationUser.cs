using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace App.Authentication
{
    public class ApplicationUser : IdentityUser 
    {
        [JsonIgnore]
        public virtual UserFriends UserFriend { get; set; }
        [JsonIgnore]
        public virtual UserFriends Friend { get; set; }
    }
}

