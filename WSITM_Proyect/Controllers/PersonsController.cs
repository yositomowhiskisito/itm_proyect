using LIBApplicationContext.Implementations;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WSITM_Proyect.Controllers
{
    public class PersonsController : ApiController
    {
        private PersonsApp PersonsApp;

        public async Task<Dictionary<string, object>> LoadSettings(Dictionary<string, object> data)
        {
            data["connectionString"] = ConfigurationManager.ConnectionStrings["Default"].ToString();
            LIBUtilities.Core.DBNull.Value = System.DBNull.Value;
            PersonsApp = new PersonsApp(data);
            return data;
        }

        protected string GetValue()
        {
            var content = Request.Content;
            Request.Content.LoadIntoBufferAsync().Wait();
            return content.ReadAsStringAsync().Result;
        }

        [HttpPost]
        [Route("Persons/Delete")]
        public string Delete()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = PersonsApp.DeleteEntity(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Persons/Insert")]
        public string Insert()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = PersonsApp.SaveEntity(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Persons/List")]
        public string List()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = PersonsApp.GetList(data);
                return JsonHelper.ConvertToString(response);
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return JsonHelper.ConvertToString(response);
            }
        }

        [HttpPost]
        [Route("Persons/Update")]
        public string Update()
        {
            var response = new Dictionary<string, object>();
            try
            {
                var data = JsonHelper.ConvertToObject(GetValue());
                LoadSettings(data);
                response = PersonsApp.UpdateEntity(data);
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