using System;
using System.Collections.Generic;

namespace LIBInfrastructure.Core
{
    public interface IParser<T>
    {
        T CreateEntity(object[] item);
        T ToEntity(Dictionary<string, object> data);
        Dictionary<string, object> ToDictionary(T entity);
        bool Validate(T entity);
    }
}