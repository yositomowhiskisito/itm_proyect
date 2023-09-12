using LIBUtilities.Core;
using LIBUtilities.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LIBCommunicationContext.Core
{
    public class CallerServices
    {
        public async Task<Dictionary<string, object>> ExecutePost(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                if (response == null || response.ContainsKey("Error"))
                    return response;
                response.Clear();

                var url = data["Url"].ToString();
                data.Remove("Url");
                var stringData = JsonHelper.ConvertToString(data);
                stringData.Replace("'{'", "{'").Replace("\"", "");

                HelperHttpClient httpClient;
                httpClient = new HelperHttpClient();
               
                //httpClient.MaxResponseContentBufferSize = 16384000;
                httpClient.Timeout = new TimeSpan(0, 4, 0);

                var message = await httpClient.PostAsync(url, new StringContent(stringData));
                if (!message.IsSuccessStatusCode)
                {
                    LogHelper.Log(new Exception(message.RequestMessage.ToString()));
                    response.Add("Error", "lbCommunicationError");
                    return response;
                }

                var resp = string.Empty;
                if (message.Content is HelperFakeContent)
                    resp = await ((HelperFakeContent)message.Content).ReadAsStringAsync();
                else
                    resp = await message.Content.ReadAsStringAsync();

                httpClient.Dispose(); httpClient = null;

                if (string.IsNullOrEmpty(resp))
                {
                    response.Add("Error", "Error Authentication");
                    return response;
                }

                resp = resp.Replace("\\\\r\\\\n", "").Replace("\\r\\n", "").Replace("\\", "")
                             .Replace("\\\"", "\"").Replace("\"", "'").Replace("'[", "[").Replace("]'", "]")
                             .Replace("'{'", "{'").Replace("\\\\", "\\").Replace("'}'", "'}").Replace("}'", "}")
                             .Replace("\\n", "").Replace("\\r", "").Replace("    ", "").Replace("'{", "{")
                             .Replace("\"", "").Replace("  ", "").Replace("null", "''");
                
                response = JsonHelper.ConvertToObject(resp);
                if (response.ContainsKey("Error"))
                {
                    LogHelper.Log(new Exception(response["Error"].ToString()));
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                response["Error"] = "lbCallService";
                response["Response"] = "ERROR";
                return response;
            }
        }
    }
}