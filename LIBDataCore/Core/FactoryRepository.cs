using LIBDomainContext;
using LIBUtilities.Core;
using System;

namespace LIBDataCore.Core
{
    public class FactoryRepository
    {
        public static IFactory<IRepository> IFactoryRepository;

        public static IRepository Get(string client, Types value)
        {
            if (IFactoryRepository == null)
                return null;

            return IFactoryRepository.Get(client, value);
        }
    }
}