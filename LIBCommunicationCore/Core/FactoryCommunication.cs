using LIBDomainContext;
using LIBUtilities.Core;

namespace LIBCommunicationCore.Core
{
    public class FactoryCommunication
    {
        public static IFactory<ICommunication> IFactoryCommunication;

        public static ICommunication Get(string client, Types value)
        {
            if (IFactoryCommunication == null)
                return null;

            return IFactoryCommunication.Get(client, value);
        }
    }
}