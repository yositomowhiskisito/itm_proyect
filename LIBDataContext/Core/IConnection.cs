using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace LIBDataContext.Core
{
    public class ConnectionSQL
    {
        private SqlConnection connection;
        private DbCommand command;
        private string stringConnection;

        public ConnectionSQL(string stringConnection)
        {
            this.stringConnection = stringConnection;
            connection = new SqlConnection(this.stringConnection);
        }

        public int ConnectionTimeout { get { return connection.ConnectionTimeout; } }
        public ConnectionState State { get { return connection.State; } }
        public string StringConnection { get => stringConnection; set => stringConnection = value; }

        public DbCommand CreateCommand(string name)
        {
            command = new SqlCommand(name, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = connection.ConnectionTimeout;
            return command;
        }

        public DbParameter CreateParameter(string name, SqlDbType type, object value, ParameterDirection direction)
        {
            var parameter = new SqlParameter(name, type);
            parameter.Value = value == null ? DBNull.Value : value;
            parameter.Direction = direction;
            command.Parameters.Add(parameter);
            return parameter;
        }

        public DbDataAdapter CreateAdapter()
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        public DataSet ExecuteQuery(DbDataAdapter adapter)
        {
            var dataset = new DataSet();
            adapter.Fill(dataset);
            return dataset;
        }

        public int ExecuteNonQuery()
        {
            return command.ExecuteNonQuery();
        }

        public void Open()
        {
            if (connection == null)
                return;
            connection.Open();
        }

        public void Close()
        {
            if (connection == null)
                return;
            connection.Close();
            connection = null;
        }
    }

    public class Parameters
    {
        public string Name;
        public SqlDbType Type;
        public object Value;
        public ParameterDirection Direction;

        public Parameters(string name, SqlDbType type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            Name = name;
            Type = type;
            Value = value;
            Direction = direction;
        }
    }
}