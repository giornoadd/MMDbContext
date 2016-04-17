using System;
using System.Data;
using System.Collections.Generic;

namespace Mimo.Dbcontext.DataTransferObjects.Mapper
{
    public abstract class BaseDataTransferObject<TModel> : IDataTransferObject<TModel>
    {
        protected Boolean IsInitialized = false;
        protected virtual void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            IsInitialized = true;
        }

        protected abstract void PopulateOrdinals(IDataReader reader);
        public abstract TModel EntityMapping(IDataReader dataReader);

        public virtual IEnumerable<TModel> GetList(IDataReader reader)
        {
            while (reader.Read())
            {
                var obj = EntityMapping(reader);
                yield return obj;
            }
            while (reader.NextResult()) reader.Read();
        }


    }
}
