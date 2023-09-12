using LIBDomainContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBCommunicationContext
{
    public abstract class Communication : ICommunication
    {
        protected Core.CallerServices client = new Core.CallerServices();
        protected string Name = string.Empty,
                         Service = string.Empty;
        public static string Protocol = "http://",
                             Base = "localhost",
                             End = string.Empty;
        public static bool IsLoaded = false;

        protected virtual void LoadSettings(){ }

        protected Dictionary<string, object> BuildUrl(Dictionary<string, object> data, string Method)
        {
            data["Url"] = Protocol + Base + "/" + Service + "/" + Name + "/" + Method + End;
            return data;
        }

        public virtual async Task<Dictionary<string, object>> GetList(Dictionary<string, object> data)
        {
            LoadSettings();
            data = BuildUrl(data, "List");
            return await client.ExecutePost(data);
        }

        public virtual async Task<Dictionary<string, object>> SaveEntity(Dictionary<string, object> data)
        {
            LoadSettings();
            data = BuildUrl(data, "Insert");
            return await client.ExecutePost(data);
        }

        public virtual async Task<Dictionary<string, object>> UpdateEntity(Dictionary<string, object> data)
        {
            LoadSettings();
            data = BuildUrl(data, "Update");
            return await client.ExecutePost(data);
        }

        public virtual async Task<Dictionary<string, object>> DeleteEntity(Dictionary<string, object> data)
        {
            LoadSettings();
            data = BuildUrl(data, "Delete");
            return await client.ExecutePost(data);
        }
    }
}