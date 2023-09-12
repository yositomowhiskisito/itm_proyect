using LIBApplicationContext.Implementations;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

namespace WSITM_Proyect.Controllers
{
    public class UsersController : ApiController
    {
        private UsersApp UsersApp;

        public async Task<Dictionary<string, object>> LoadSettings(Dictionary<string, object> data)
        {
            data["connectionString"] = ConfigurationManager.ConnectionStrings["Default"].ToString();
            LIBUtilities.Core.DBNull.Value = System.DBNull.Value;
            UsersApp = new UsersApp();
            return data;
        }

        protected string GetValue()
        {
            var content = Request.Content;
            Request.Content.LoadIntoBufferAsync().Wait();
            return content.ReadAsStringAsync().Result;
        }

        [HttpPost]
        [Route("Users/Delete")]
        public string Delete()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = UsersApp.DeleteEntity(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Users/Insert")]
        public string Insert()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = UsersApp.SaveEntity(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Users/List")]
        public string List()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = UsersApp.GetList(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Users/Update")]
        public string Update()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = UsersApp.UpdateEntity(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Users/Login")]
        public string Login()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = UsersApp.Login(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }
    }
}