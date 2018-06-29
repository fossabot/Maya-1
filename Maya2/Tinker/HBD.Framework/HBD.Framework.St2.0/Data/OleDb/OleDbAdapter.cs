﻿#if !NETSTANDARD2_0
#region using

using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using HBD.Framework.Data.Base;

#endregion

namespace HBD.Framework.Data.OleDb
{
    public class OleDbAdapter : DataClientAdapter
    {
        public OleDbAdapter(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public OleDbAdapter(IDbConnection connection) : base(connection)
        {
        }

        protected override IDbConnection CreateConnection() => new OleDbConnection(ConnectionString.ConnectionString);

        protected override DbConnectionStringBuilder CreateConnectionString(string connectionString)
            => new OleDbConnectionStringBuilder(connectionString);

        protected override IDataParameter CreateParameter(string name, object value) => new OleDbParameter(name, value);
    }
}
#endif