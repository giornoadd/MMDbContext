using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Mimo.Dbcontext.DatabaseContext
{
    public abstract partial class BaseDBContext : IDisposable
    {

        public abstract IDbCommand GetSqlCommand(string sqlQuery);

        public abstract IDbCommand GetSprocCommand(string sprocName);

        public abstract Int32 ExecuteNonQuery(IDbCommand command);
        public abstract Int32 ExecuteNonQuery(String sqlCommand, object parameter);
        public abstract Task<Int32> ExecuteNonQueryAsync(String sqlCommand, object parameter);


        public abstract Object ExecuteScalar(IDbCommand command);
        public abstract Object ExecuteScalar(String sqlCommand, object parameter);
        public abstract Task<Object> ExecuteScalarAsync(String sqlCommand, object parameter);


        public abstract T GetSingleValue<T>(IDbCommand command) where T : class, new();

        public abstract Int32 GetSingleInt32(IDbCommand command);

        public abstract Int32 GetSingleInt32(String sqlCommand, object parameter);


        public abstract string GetSingleString(IDbCommand command);

        public abstract List<string> GetStringList(IDbCommand command);

        public abstract List<int> GetInt32List(IDbCommand command);

        public abstract T GetSingle<T>(IDbCommand command) where T : class, new();
        public abstract T GetSingle<T>(String sqlCommand, object parameter) where T : class, new();
        public abstract Task<T> GetSingleAsync<T>(string sqlCommand, object parameter) where T : class, new();

        public abstract List<T> GetList<T>(IDbCommand command) where T : class, new();
        public abstract List<T> GetList<T>(String sqlCommand, object parameter) where T : class, new();
        public abstract Task<IEnumerable<T>> GetListAsync<T>(string sqlCommand, object parameter) where T : class, new();
        public abstract void Dispose();

    }
}
