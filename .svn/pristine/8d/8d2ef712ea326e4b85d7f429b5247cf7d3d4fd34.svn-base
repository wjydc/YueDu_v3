using Autofac;
using DapperExtension.Core;
using Service.Core;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Service
{
    public enum DbOperation
    {
        Read, Write
    };
}

namespace Service.Base
{
    public class BaseService : IBaseService
    {
        private static readonly string readConnString = ConfigurationManager.ConnectionStrings["ReadConnString"].ConnectionString;
        private static readonly string writeConnString = ConfigurationManager.ConnectionStrings["WriteConnString"].ConnectionString;

        protected IDbConnection DbConnection(DbOperation state)
        {
            IDbConnection dbConnection = null;

            switch (state)
            {
                case DbOperation.Read:
                    dbConnection = new SqlConnection(readConnString);
                    break;

                case DbOperation.Write:
                    dbConnection = new SqlConnection(writeConnString);
                    break;
            }

            return dbConnection;
        }

        protected IDbTransaction BeginTransaction(IDbConnection dbConnection, IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            if (dbConnection == null)
                throw new NullReferenceException("DbConnection 值不可为空……");

            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();

            return dbConnection.BeginTransaction();
        }
    }
}