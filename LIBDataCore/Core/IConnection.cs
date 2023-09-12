using System;
using System.Data;
using System.Data.Common;

namespace LIBDataCore.Core
{
    public interface IConnection
    {
        int ConnectionTimeout { get; }
        ConnectionState State { get; }
        string StringConnection { set; get; }

        void Open();
        void Close();
        DataSet ExecuteQuery(DbDataAdapter adapter);
        DbCommand CreateCommand(string name);
        DbParameter CreateParameter(string name, SqlDbType type, object value, ParameterDirection direction);
        DbDataAdapter CreateAdapter();
        int ExecuteNonQuery();
    }
}