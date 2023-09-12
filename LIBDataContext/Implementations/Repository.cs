using LIBDataContext.Core;
using LIBInfrastructure.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace LIBDataContext.Implementations
{
    public class Repository<T>
    {
        protected IParser<T> parser;
        protected ConnectionSQL connection;

        protected void LoadConnection(Dictionary<string, object> data)
        {
            try
            {
                var settings = data["connectionString"];
                connection = new ConnectionSQL(settings.ToString());
                
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
                throw new Exception("", ex);
            }
        }

        public Dictionary<string, object> Execute(Dictionary<string, object> data, int count = 0)
        {
            var response = new Dictionary<string, object>();
            try
            {
                LoadConnection(data);
                if (!data.ContainsKey("Open") || (bool)data["Open"])
                    connection.Open();

                var command = connection.CreateCommand(data["Query"].ToString());
                var outParameters = new List<DbParameter>();
                var parameters = (List<Parameters>)data["Parameters"];
                foreach (var item in parameters)
                {
                    var parameter = connection.CreateParameter(item.Name, item.Type, item.Value, item.Direction);
                    if (item.Direction == ParameterDirection.InputOutput)
                        outParameters.Add(parameter);
                }

                var dataset = connection.ExecuteQuery(connection.CreateAdapter());

                foreach (var item in outParameters)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Name == item.ParameterName);
                    if (parameter == null)
                        continue;
                    parameter.Value = item.Value;
                }

                var result = outParameters.FirstOrDefault(x => x.ParameterName == "Result");
                if (result != null && Convert.ToInt32(result.Value) <= 0)
                    return ErrorHelper.GetError("lbDBValidation");

                var list = new List<T>();
                foreach (DataRow item in dataset.Tables[0].Rows)
                    list.Add(parser.CreateEntity(item.ItemArray));

                if (!data.ContainsKey("Close") || (bool)data["Close"])
                    connection.Close();

                response["Entities"] = list;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
                //if (count < 2 && ex.HResult == -2146232060)
                //{
                //    System.Threading.Thread.Sleep(1000 * 10);
                //    return Execute(data, count++);
                //}
                return ErrorHelper.GetError("", ex);
            }
        }

        public Dictionary<string, object> ExecuteNonQuery(Dictionary<string, object> data, int count = 0)
        {
            var response = new Dictionary<string, object>();
            try
            {
                LoadConnection(data);
                if (!data.ContainsKey("Open") || (bool)data["Open"])
                    connection.Open();
                var command = connection.CreateCommand(data["Query"].ToString());

                var outParameters = new List<DbParameter>();
                var parameters = (List<Parameters>)data["Parameters"];
                foreach (var item in parameters)
                {
                    var parameter = connection.CreateParameter(item.Name, item.Type, item.Value, item.Direction);
                    if (parameter.Direction == ParameterDirection.InputOutput)
                        outParameters.Add(parameter);
                }

                connection.ExecuteNonQuery();

                foreach (var item in outParameters)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Name == item.ParameterName);
                    if (parameter == null)
                        continue;
                    parameter.Value = item.Value;
                }

                if (!data.ContainsKey("Close") || (bool)data["Close"])
                    connection.Close();

                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
                //if (count < 2 && ex.HResult == -2146232060)
                //{
                //    System.Threading.Thread.Sleep(1000 * 40);
                //    return ExecuteNonQuery(data, count++);
                //}
                return ErrorHelper.GetError("", ex);
            }
        }

        public Dictionary<string, object> DataTables(Dictionary<string, object> data, int count = 0)
        {
            var response = new Dictionary<string, object>();
            try
            {
                LoadConnection(data);
                if (!data.ContainsKey("Open") || (bool)data["Open"])
                    connection.Open();
                var command = connection.CreateCommand(data["Query"].ToString());

                var outParameters = new List<DbParameter>();
                var parameters = (List<Parameters>)data["Parameters"];
                foreach (var item in parameters)
                {
                    var parameter = connection.CreateParameter(item.Name, item.Type, item.Value, item.Direction);
                    if (item.Direction == ParameterDirection.InputOutput)
                        outParameters.Add(parameter);
                }

                var dataset = connection.ExecuteQuery(connection.CreateAdapter());

                foreach (var item in outParameters)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Name == item.ParameterName);
                    if (parameter == null)
                        continue;
                    parameter.Value = item.Value;
                }

                var result = outParameters.FirstOrDefault(x => x.ParameterName == "Result");
                if (result != null && Convert.ToInt32(result.Value) <= 0)
                    return ErrorHelper.GetError("lbDBValidation");

                response["DataTables"] = dataset.Tables;
                response["Response"] = "OK";
                return response;
            }
            catch (Exception ex)
            {
                if (connection != null &&
                    connection.State != ConnectionState.Closed)
                    connection.Close();
                //if (count < 2 && ex.HResult == -2146232060)
                //{
                //    System.Threading.Thread.Sleep(1000 * 10);
                //    return Execute(data, count++);
                //}
                return ErrorHelper.GetError("", ex);
            }
        }
    }
}