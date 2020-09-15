using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Authentication
{
    public class UserFriends
    {


        [Key]
        public int UserFriendId { get; set; }
       
        public string UserId { get; set; }
        public virtual ApplicationUser User {get;set;}
     
        public string FriendId { get; set; }
        public virtual ApplicationUser Friend { get; set; }

        public Boolean InviteStatus { get; set; }
         
       
         
       
    }
}
