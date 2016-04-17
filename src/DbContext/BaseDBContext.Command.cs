using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mimo.Dbcontext.DatabaseContext
{
    public abstract partial class BaseDBContext : IDisposable
    {

        #region Create SqlCommand
        public abstract IDbCommand GetSqlCommand(string sqlQuery);
        public abstract IDbCommand GetSprocCommand(string sprocName);
        #endregion  Create SqlCommand
        
        #region Sync Command
        public abstract Int32 ExecuteNonQuery(IDbCommand command);
        public abstract Object ExecuteScalar(IDbCommand command);
        public abstract T GetSingleValue<T>(IDbCommand command) where T : class, new();
        public abstract Int32 GetSingleInt32(IDbCommand command);
        public abstract string GetSingleString(IDbCommand command);
        public abstract List<string> GetStringList(IDbCommand command);
        public abstract List<int> GetInt32List(IDbCommand command);
        public abstract T GetSingle<T>(IDbCommand command) where T : class, new();
        public abstract List<T> GetList<T>(IDbCommand command) where T : class, new();
        #endregion Sync Command
        
        #region Sync Command
        public abstract Task<Int32> ExecuteNonQueryAsync(IDbCommand command);
        public abstract Task<Object> ExecuteScalarAsync(IDbCommand command);
        public abstract Task<T> GetSingleValueAsync<T>(IDbCommand command) where T : class, new();
        public abstract Task<Int32> GetSingleInt32Async(IDbCommand command);
        public abstract Task<String> GetSingleStringAsync(IDbCommand command);
        public abstract Task<List<string>> GetStringListAsync(IDbCommand command);
        public abstract Task<List<int>> GetInt32ListAsync(IDbCommand command);
        public abstract Task<T> GetSingleAsync<T>(IDbCommand command) where T : class, new();
        public abstract Task<List<T>> GetListAsync<T>(IDbCommand command) where T : class, new();
        #endregion Sync Command
        
        public abstract void Dispose();

    }
}
