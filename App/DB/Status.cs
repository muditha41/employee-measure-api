using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.DB
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public UserStatus UserStatus { get; set; }
        public UserStatus FriendStatus { get; set; }
    }
}
