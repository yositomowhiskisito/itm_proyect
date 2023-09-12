using LIBCommunicationContext.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBCommunicationContext.Implementations
{
    public class UsersCommunication : Communication
    {
        protected override void LoadSettings()
        {
            base.LoadSettings();
            Service = "WSITM_Proyect";
            Name = "Users";
        }

        public async Task<Dictionary<string, object>> Login(Dictionary<string, object> data)
        {
            LoadSettings();
            data = BuildUrl(data, "Login");
            return await client.ExecutePost(data);
        }
    }
}