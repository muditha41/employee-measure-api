using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Authentication
{
    public class UserFriends
    {
        [Key]
        public string UserFirendId { get; set; }        
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FriendId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
