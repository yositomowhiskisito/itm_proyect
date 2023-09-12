using System;
using System.Collections.Generic;
using System.Linq;
using LIBInfrastructure.Implementation;
using System.Data;
using LIBDomainEntities.Entities;
using LIBDataContext.Core;

namespace LIBDataContext.Implementations
{
    public class PersonsRepository : Repository<Persons>
    {
        public PersonsRepository(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                parser = new PersonsParser();
                LoadConnection(data);
            }
            catch (Exception ex)
            {
            }
        }

        public Dictionary<string, object> GetList(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_GET_Persons";
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
                var current = (Persons)data["Current"];
                parameters.Add(new Parameters("SSN", SqlDbType.Int, current.SSN));
                parameters.Add(new Parameters("Name", SqlDbType.NVarChar, current.Name));
                parameters.Add(new Parameters("Born", SqlDbType.DateTime, current.Born));
                parameters.Add(new Parameters("State", SqlDbType.Bit, current.State));
                parameters.Add(new Parameters("File", SqlDbType.VarBinary, current.File));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_ADD_Persons";
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
                var current = (Persons)data["Current"];
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Id", SqlDbType.Int, current.Id));
                parameters.Add(new Parameters("SSN", SqlDbType.Int, current.SSN));
                parameters.Add(new Parameters("Name", SqlDbType.NVarChar, current.Name));
                parameters.Add(new Parameters("Born", SqlDbType.DateTime, current.Born));
                parameters.Add(new Parameters("State", SqlDbType.Bit, current.State));
                parameters.Add(new Parameters("File", SqlDbType.VarBinary, current.File));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_UPDATE_Persons";
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

        public Dictionary<string, object> DeleteEntity(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                var current = (Persons)data["Current"];
                var parameters = new List<Parameters>();
                parameters.Add(new Parameters("Id", SqlDbType.Int, current.Id));
                parameters.Add(new Parameters("Result", SqlDbType.Int, 0, ParameterDirection.InputOutput));

                data["Query"] = "SP_DELETE_Persons";
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
    }
}