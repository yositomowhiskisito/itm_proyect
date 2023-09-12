using LIBDomainContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LIBPresentationCore.Interfaces.IHelpers
{
    public interface IUsersHelper : IHelper 
    {
        Task<Dictionary<string, object>> Login(Dictionary<string, object> data);
    }
}
