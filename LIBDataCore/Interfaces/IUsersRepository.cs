using LIBDomainContext;
using System.Collections.Generic;

namespace LIBDataCore.Interfaces
{
    public interface IUsersRepository : IRepository
    {
        Dictionary<string, object> Login(Dictionary<string, object> data);
    }
}
