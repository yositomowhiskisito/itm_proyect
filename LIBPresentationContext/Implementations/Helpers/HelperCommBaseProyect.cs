using LIBDomainContext;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBPresentationContext.Implementations.Helpers
{
    public abstract class HelperCommBaseProyect<T> : Helper<T>, IHelper
    {
        protected ICommunication ICommunication;

        protected override void LoadImplementation(Dictionary<string, object> data)
        {
            base.LoadImplementation(data);
        }

        public override Dictionary<string, object> LoadDefault(Dictionary<string, object> data)
        {
            data = base.LoadDefault(data);
            return data;
        }

        public virtual async Task<Dictionary<string, object>> DeleteEntity(Dictionary<string, object> data)
        {
            try
            {
                data = LoadDefault(data);
                LoadImplementation(data);
                if (IParser != null && !IParser.Validate((T)data["Current"]))
                {
                    await MessagesHelper.AsyncShow("RsErrors.lbMissingInfo");
                    return new Dictionary<string, object>() { { "Error", "lbMissingInfo" } };
                }
                if (IParser != null && data.ContainsKey("Current"))
                    data["Entity"] = IParser.ToDictionary((T)data["Current"]);
                Dictionary<string, object> response = null;
                if (data != null && !data.ContainsKey("Syncronized"))
                    response = await Task.Run(() => ICommunication.DeleteEntity(data));
                else
                    response = await ICommunication.DeleteEntity(data);
                if (response.ContainsKey("Error"))
                {
                    await MessagesHelper.AsyncShow("MessageHelper.GetMessage(response)");
                    return response;
                }
                if (IParser != null && response.ContainsKey("Current"))
                    response["Entity"] = IParser.ToEntity((Dictionary<string, object>)response["Current"]);

                await MessagesHelper.AsyncShow("Info Deleted");

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return new Dictionary<string, object>() { { "Error", ex.ToString() } };
            }
        }

        public virtual async Task<Dictionary<string, object>> GetList(Dictionary<string, object> data)
        {
            try
            {
                data = LoadDefault(data);
                LoadImplementation(data);
                Dictionary<string, object> response = null;
                if (data != null && !data.ContainsKey("Syncronized"))
                    response = await Task.Run(() => ICommunication.GetList(data));
                else
                    response = await ICommunication.GetList(data);
                if (response.ContainsKey("Error"))
                {
                    await MessagesHelper.AsyncShow("MessageHelper.GetMessage(response)");
                    return response;
                }
                if (IParser != null && response.ContainsKey("List"))
                {
                    var list = new List<T>();
                    foreach (var item in (Dictionary<string, object>)response["List"])
                        list.Add(IParser.ToEntity((Dictionary<string, object>)item.Value));
                    response["Entities"] = list;
                }

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return new Dictionary<string, object>() { { "Error", ex.ToString() } };
            }
        }

        public virtual async Task<Dictionary<string, object>> SaveEntity(Dictionary<string, object> data)
        {
            try
            {
                data = LoadDefault(data);
                LoadImplementation(data);
                if (IParser != null && !IParser.Validate((T)data["Current"]))
                {
                    await MessagesHelper.AsyncShow("RsErrors.lbMissingInfo");
                    return new Dictionary<string, object>() { { "Error", "lbMissingInfo" } };
                }
                if (IParser != null && data.ContainsKey("Current"))
                    data["Current"] = IParser.ToDictionary((T)data["Current"]);
                Dictionary<string, object> response = null;
                if (data != null && !data.ContainsKey("Syncronized"))
                    response = await Task.Run(() => ICommunication.SaveEntity(data));
                else
                    response = await ICommunication.SaveEntity(data);
                if (response.ContainsKey("Error"))
                {
                    await MessagesHelper.AsyncShow("MessageHelper.GetMessage(response)");
                    return response;
                }
                if (IParser != null && response.ContainsKey("Current"))
                    response["Current"] = IParser.ToEntity((Dictionary<string, object>)response["Entity"]);

                await MessagesHelper.AsyncShow("Info Saved");

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return new Dictionary<string, object>() { { "Error", ex.ToString() } };
            }
        }

        public virtual async Task<Dictionary<string, object>> UpdateEntity(Dictionary<string, object> data)
        {
            try
            {
                data = LoadDefault(data);
                LoadImplementation(data);
                if (IParser != null && !IParser.Validate((T)data["Current"]))
                {
                    await MessagesHelper.AsyncShow("RsErrors.lbMissingInfo");
                    return new Dictionary<string, object>() { { "Error", "lbMissingInfo" } };
                }
                if (IParser != null && data.ContainsKey("Current"))
                    data["Entity"] = IParser.ToDictionary((T)data["Current"]);
                Dictionary<string, object> response = null;
                if (data != null && !data.ContainsKey("Syncronized"))
                    response = await Task.Run(() => ICommunication.UpdateEntity(data));
                else
                    response = await ICommunication.UpdateEntity(data);
                if (response.ContainsKey("Error"))
                {
                    await MessagesHelper.AsyncShow("MessageHelper.GetMessage(response)");
                    return response;
                }
                if (IParser != null && response.ContainsKey("Current"))
                    response["Current"] = IParser.ToEntity((Dictionary<string, object>)response["Entity"]);

                await MessagesHelper.AsyncShow("Info Updated");

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return new Dictionary<string, object>() { { "Error", ex.ToString() } };
            }
        }
    }
}