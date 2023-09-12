using LIBDataContext.Implementations;
using LIBDomainEntities.Entities;
using LIBInfrastructure.Implementation;
using System;
using System.Collections.Generic;

namespace LIBApplicationContext.Implementations
{
    public class PersonsApp
    {
        protected PersonsRepository PersonsRepository;
        protected PersonsParser IParser;
        protected List<Persons> list = new List<Persons>();

        public PersonsApp(Dictionary<string, object> data)
        {
            IParser = new PersonsParser();
            PersonsRepository = new PersonsRepository(data);
        }

        public Dictionary<string, object> GetList(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                response = PersonsRepository.GetList(data);
                if (response == null)
                {
                    response = new Dictionary<string, object>();
                    response["Error"] = "lbErrorWasHappened";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                list = (List<Persons>)response["Entities"];
                if (IParser != null)
                {
                    var dic = new Dictionary<string, object>();
                    var count = 0;
                    foreach (var item in list)
                    {
                        dic[count.ToString()] = IParser.ToDictionary(item);
                        count++;
                    }
                    response["List"] = dic;
                }
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }

        public virtual Dictionary<string, object> SaveEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                if (IParser != null)
                    data["Current"] = IParser.ToEntity((Dictionary<string, object>)data["Current"]);
                var current = (Persons)data["Current"];
                response = PersonsRepository.SaveEntity(data);
                if (response == null)
                {
                    response = new Dictionary<string, object>();
                    response["Response"] = "lbInfoWasntSaved";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                response["Message"] = "lbInfoWasSaved";
                current = (Persons)response["Current"];
                list.Add(current);
                if (IParser != null)
                    response["Current"] = IParser.ToDictionary((Persons)response["Current"]);
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }

        public virtual Dictionary<string, object> UpdateEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                if (IParser != null)
                    data["Current"] = IParser.ToEntity((Dictionary<string, object>)data["Current"]);
                var current = (Persons)data["Current"];
                if (current == null || current.Id == 0)
                {
                    response["Message"] = "lbItemCantProcess";
                    return response;
                }
                response = PersonsRepository.UpdateEntity(data);
                if (response == null)
                {
                    response = new Dictionary<string, object>();
                    response["Message"] = "lbInfoWasntUpdated";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                response["Message"] = "lbInfoWasUpdated";
                if (IParser != null)
                    response["Current"] = IParser.ToDictionary((Persons)response["Current"]);
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }

        public Dictionary<string, object> DeleteEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                if (IParser != null)
                    data["Current"] = IParser.ToEntity((Dictionary<string, object>)data["Current"]);
                var current = (Persons)data["Current"];
                if (current == null || current.Id == 0)
                {
                    response["Message"] = "lbItemCantProcess";
                    return response;
                }
                response = PersonsRepository.DeleteEntity(data);
                if (response == null)
                {
                    response = new Dictionary<string, object>();
                    response["Message"] = "lbInfoWasntDeleted";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                response["Message"] = "lbInfoWasDeleted";
                list.Remove(current);
                if (IParser != null)
                    response["Current"] = IParser.ToDictionary((Persons)response["Current"]);
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }
    }
}