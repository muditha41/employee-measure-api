using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers.Resources
{
    public class NotificationResource
    {
        public int UserNotificationId { get; set; }
        public string UserId { get; set; }

        public UserResource User { get; set; }

        public string FriendId { get; set; }
        public bool State { get; set; }
        public UserResource Friend { get; set; }
        public string Notification { get; set; }
    }
}
