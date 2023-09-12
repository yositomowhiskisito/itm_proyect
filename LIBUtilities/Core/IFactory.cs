using LIBUtilities.Core;
using System;

namespace LIBUtilities.Core
{
    public interface IFactory<T>
    {
        T Get(string client, Types value);
    }
}