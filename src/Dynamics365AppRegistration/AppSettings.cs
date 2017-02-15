using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamics365AppRegistration
{
    public class AppSettings
    {
        public AppInfo[] AppInfo { get; set; }
        
        public InitialUser InitialUser { get; set; }        
    }

    public class InitialUser
    {
        public string email { get; set; }
        
        public string password { get; set; }
    }

    public class AppInfo
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
