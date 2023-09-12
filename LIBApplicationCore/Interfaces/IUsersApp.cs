using LIBDomainContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIBApplicationCore.Interfaces
{
    public  interface IUsersApp : IApplication 
    {
        Dictionary<string, object> Login(Dictionary<string, object> data);
    }
}
