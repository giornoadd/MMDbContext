using System;
using System.Data;
using System.Data.Common;

namespace Mimo.Dbcontext.DatabaseContext
{
    public abstract partial class BaseDBContext : IDisposable
    {
        
        public abstract DbParameter CreateNullParameter(string name, SqlDbType paramType);
         
        public abstract DbParameter CreateNullParameter(string name, SqlDbType paramType, int size);
         
        public abstract DbParameter CreateOutputParameter(string name, SqlDbType paramType);
         
        public abstract DbParameter CreateOutputParameter(string name, SqlDbType paramType, int size);
         
        public abstract DbParameter CreateParameter(string name, Guid value);
         
        public abstract DbParameter CreateParameter(string name, int value);
         
        public abstract DbParameter CreateParameter(string name, DateTime value);
         
        public abstract DbParameter CreateParameter(string name, bool value);
         
        public abstract DbParameter CreateParameter(string name, bool? value);
         
        public abstract DbParameter CreateParameter(string name, string value, int size);
         
        public abstract DbParameter CreateParameter(string name, long value);
         
        public abstract DbParameter CreateParameter(string name, double value);
         
        public abstract DbParameter CreateParameter(string name, decimal value);
         
        public abstract DbParameter CreateParameter(String name, Decimal? value);

    }
}
