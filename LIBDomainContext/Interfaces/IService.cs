using System;

namespace LIBDomainContext
{
    public interface IService
    {
        IApplication App { set; }
        string GetList(string value);
        string SaveEntity(string value);
        string UpdateEntity(string value);
        string DeleteEntity(string value);
    }
}