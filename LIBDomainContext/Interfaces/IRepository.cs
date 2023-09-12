using System;
using System.Collections.Generic;

namespace LIBDomainContext
{
    public interface IRepository
    {
        Dictionary<string, object> GetList(Dictionary<string, object> data);
        Dictionary<string, object> SaveEntity(Dictionary<string, object> data);
        Dictionary<string, object> UpdateEntity(Dictionary<string, object> data);
        Dictionary<string, object> DeleteEntity(Dictionary<string, object> data);
    }
}