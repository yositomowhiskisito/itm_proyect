using LIBInfrastructure.Core;
using LIBPresentationContext.Core;
using LIBPresentationCore.Core;
using LIBUtilities.Utilities;
using System.Collections.Generic;

namespace LIBPresentationContext.Implementations.Helpers
{
    public abstract class Helper<T>
    {
        protected IParser<T> IParser;
        protected IConfigurationManager IConfigurationManager;

        protected virtual void LoadImplementation(Dictionary<string, object> data){ }

        public virtual Dictionary<string, object> LoadDefault(Dictionary<string, object> data)
        {
            data["Client"] = "Default";
            data["CreateUser"] = "Admin";
            return data;
        }
    }
}