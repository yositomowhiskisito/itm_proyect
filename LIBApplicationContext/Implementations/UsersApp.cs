using LIBDataContext.Implementations;
using LIBDomainEntities.Entities;
using LIBInfrastructure.Core;
using LIBInfrastructure.Implementation;
using System;
using System.Collections.Generic;

namespace LIBApplicationContext.Implementations
{
    public class UsersApp
    {
        protected UsersRepository UsersRepository;
        protected IParser<Users> IParser;
        protected List<Users> list = new List<Users>();

        public UsersApp()
        {
            IParser = new UsersParser();
            UsersRepository = new UsersRepository();
        }

        public Dictionary<string, object> GetList(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                response = UsersRepository.GetList(data);
                if (response == null)
                {
                    response = new Dictionary<string, object>();
                    response["Error"] = "lbErrorWasHappened";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                list = (List<Users>)response["Entities"];
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
                var current = (Users)data["Current"];
                response = UsersRepository.SaveEntity(data);
                if (response == null)
                {
                    response = new Dictionary<string, object>();
                    response["Response"] = "lbInfoWasntSaved";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                response["Message"] = "lbInfoWasSaved";
                current = (Users)response["Current"];
                list.Add(current);
                if (IParser != null)
                    response["Current"] = IParser.ToDictionary((Users)response["Current"]);
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
                response = UsersRepository.UpdateEntity(data);
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
                    response["Current"] = IParser.ToDictionary((Users)response["Current"]);
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
                var current = (Users)data["Current"];
                if (current == null || current.Id == 0)
                {
                    response["Message"] = "lbItemCantProcess";
                    return response;
                }
                response = UsersRepository.DeleteEntity(data);
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
                    response["Current"] = IParser.ToDictionary((Users)response["Current"]);
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                return response;
            }
        }

        public Dictionary<string, object> Login(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                if (!data.ContainsKey("Email") ||
                    !data.ContainsKey("Password"))
                {
                    response["Error"] = "lbIncomeData";
                    return response;
                }
                response = UsersRepository.Login(data);
                if (response == null)
                {
                    response["Error"] = "lbItemNotFound";
                    return response;
                }
                if (response.ContainsKey("Error"))
                    return response;
                var user = (Users)response["Current"];
                response.Remove("Current");
                response.Remove("List");
                response["User"] = user;
                if (IParser != null)
                    response["User"] = IParser.ToDictionary(user);
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