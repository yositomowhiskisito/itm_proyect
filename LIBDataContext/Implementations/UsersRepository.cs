using System;
using System.Collections.Generic;
using System.Linq;
using LIBInfrastructure.Implementation;
using System.Data;
using LIBDomainEntities.Entities;
using LIBDataContext.Core;

namespace LIBDataContext.Implementations
{
    public class UsersRepository : Repository<Users>
    {
        public UsersRepository() { parser = new UsersParser(); }

        public Dictionary<string, object> GetList(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_GET_Users";
                data["Parameters"] = parameters;
                response = base.Execute(data);

                if (response == null)
                    return null;

                if (response["Response"].ToString() == "ERROR")
                    return response;

                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                response["Response"] = "ERROR";
                return response;
            }
        }

        public Dictionary<string, object> SaveEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var parameters = new List<Parameters>();
                var current = (Users)data["Current"];
                parameters.Add(new Parameters("Person", SqlDbType.Int, current.Person));
                parameters.Add(new Parameters("State", SqlDbType.Bit, current.State));
                parameters.Add(new Parameters("Email", SqlDbType.NVarChar, current.Email));
                parameters.Add(new Parameters("Password", SqlDbType.NVarChar, current.Password));
                parameters.Add(new Parameters("User", SqlDbType.Int, null));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_ADD_Users";
                data["Parameters"] = parameters;
                response = base.ExecuteNonQuery(data);

                if (response == null)
                    return null;

                if (response["Response"].ToString() == "ERROR")
                    return response;

                var parameter = parameters.FirstOrDefault(x => x.Name == "Result");
                var responseValue = Convert.ToInt32(parameter.Value.ToString());
                if (responseValue < 1)
                {
                    response["Response"] = "ERROR";
                    switch (parameter.Value)
                    {
                        case 0: response["Error"] = "See Audit"; break;
                        case -1: response["Error"] = "User already exists"; break;
                        case -2: response["Error"] = "Person no exists"; break;
                        default: response["Error"] = "Call admin"; break;
                    }
                    return response;
                }

                response["Current"] = current;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                response["Response"] = "ERROR";
                return response;
            }
        }

        public Dictionary<string, object> UpdateEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var current = (Users)data["Current"];
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Id", SqlDbType.Int, current.Id));
                parameters.Add(new Parameters("Person", SqlDbType.Int, current.Person));
                parameters.Add(new Parameters("State", SqlDbType.Bit, current.State));
                parameters.Add(new Parameters("Email", SqlDbType.NVarChar, current.Email));
                parameters.Add(new Parameters("Password", SqlDbType.NVarChar, current.Password));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_UPDATE_Users";
                data["Parameters"] = parameters;
                response = base.ExecuteNonQuery(data);

                if (response == null)
                    return null;

                if (response["Response"].ToString() == "ERROR")
                    return response;

                var parameter = parameters.FirstOrDefault(x => x.Name == "Result");
                if (parameter.Value.ToString() != "1")
                {
                    response["Response"] = "ERROR";
                    switch (parameter.Value)
                    {
                        case 0: response["Error"] = "See Audit"; break;
                        case -1: response["Error"] = "User no exist"; break;
                        case -2: response["Error"] = "Person no exist"; break;
                        default: response["Error"] = "Call admin"; break;
                    }
                    return response;
                }

                response["Current"] = current;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                response["Response"] = "ERROR";
                return response;
            }
        }

        public Dictionary<string, object> DeleteEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var current = (Users)data["Current"];
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Id", SqlDbType.Int, current.Id));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_DELETE_Users";
                data["Parameters"] = parameters;
                response = base.ExecuteNonQuery(data);

                if (response == null)
                    return null;

                if (response["Response"].ToString() == "ERROR")
                    return response;

                var parameter = parameters.FirstOrDefault(x => x.Name == "Result");
                if (parameter.Value.ToString() != "1")
                {
                    response["Response"] = "ERROR";
                    switch (parameter.Value)
                    {
                        case 0: response["Error"] = "See Audit"; break;
                        case -1: response["Error"] = "Item no exist"; break;
                        default: response["Error"] = "Call admin"; break;
                    }
                    return response;
                }

                response["Current"] = current;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                response["Response"] = "ERROR";
                return response;
            }
        }

        public Dictionary<string, object> Login(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Email", SqlDbType.NVarChar, data["Email"].ToString()));
                parameters.Add(new Parameters("Password", SqlDbType.NVarChar, data["Password"].ToString()));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_Login";
                data["Parameters"] = parameters;
                response = base.Execute(data);

                if (response == null)
                    return null;

                if (response["Response"].ToString() == "ERROR")
                    return response;

                var parameter = parameters.FirstOrDefault(x => x.Name == "Result");
                if (parameter.Value.ToString() != "1")
                {
                    response["Response"] = "ERROR";
                    switch (parameter.Value)
                    {
                        case 0: response["Error"] = "lbSeeAudit"; break;
                        case -1: response["Error"] = "lbNoExistUserOrPassword"; break;
                    }
                    return response;
                }

                response["Current"] = ((List<Users>)response["Entities"]).FirstOrDefault();
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                response["Error"] = ex.ToString();
                response["Response"] = "ERROR";
                return response;
            }
        }
    }
}