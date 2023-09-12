using LIBCommunicationContext.Implementations;
using LIBDomainEntities.Entities;
using LIBInfrastructure.Implementation;
using LIBPresentationCore.Interfaces.IHelpers;
using LIBUtilities.Core;
using System.Collections.Generic;

namespace LIBPresentationContext.Implementations.Helpers.Communications
{
    public class PersonsHelper : HelperCommBaseProyect<Persons>, IPersonsHelper
    {
        protected override void LoadImplementation(Dictionary<string, object> data)
        {
            base.LoadImplementation(data);
            IParser = new PersonsParser();
            ICommunication = ICommunication ?? new PersonsCommunication();
        }
    }
}