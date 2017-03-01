using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamics365AppRegistration.Models
{
    public class CheckRegistration
    {
        public AccessLevel AccessLevel { get; set; }

        public string NotificationMessage { get; set; }

        public string RedirectUrl { get; set; }

        public string NotificationActionText { get; set; }
    }
}
