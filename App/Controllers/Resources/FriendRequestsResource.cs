using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers.Resources
{
    public class FriendRequestsResource
    {
         
        public string FriendId { get; set; }
        public UserResource Friend { get; set; }
          
    }
}
