using LIBCommunicationContext.Implementations;
using LIBDomainEntities.Entities;
using LIBInfrastructure.Implementation;
using LIBPresentationCore.Interfaces.IHelpers;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBPresentationContext.Implementations.Helpers.Communications
{
    public class UsersHelper : HelperCommBaseProyect<Users>, IUsersHelper
    {
        public async Task<Dictionary<string, object>> Login(Dictionary<string, object> data)
        {
            try
            {
                data = LoadDefault(data);
                LoadImplementation(data);
                Dictionary<string, object> response = null;
                if (data != null && !data.ContainsKey("Syncronized"))
                    response = await ((UsersCommunication)ICommunication).Login(data);
                else
                    response = await ((UsersCommunication)ICommunication).Login(data);
                if (response.ContainsKey("Error"))
                {
                    await MessagesHelper.AsyncShow("No exist User");
                    return response;
                }
                if (IParser != null && response.ContainsKey("User"))
                    response["User"] = IParser.ToEntity((Dictionary<string, object>)response["User"]);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return new Dictionary<string, object>() { { "Error", ex.ToString() } };
            }
        }

        protected override void LoadImplementation(Dictionary<string, object> data)
        {
            base.LoadImplementation(data);
            IParser = new UsersParser();
            ICommunication = ICommunication ?? new UsersCommunication();
        }
    }
}