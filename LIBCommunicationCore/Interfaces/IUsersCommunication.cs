using LIBDomainContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBCommunicationCore.Interfaces
{
    public interface IUsersCommunication : ICommunication 
    {
        Task<Dictionary<string, object>> Login(Dictionary<string, object> data);
    }
}