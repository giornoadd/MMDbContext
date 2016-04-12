using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mimo.Dbcontext.DataItemConversion;

namespace Mimo.Dbcontext.DataTransferObjects.Mapper
{
    public abstract class BaseDataTransferObject<TModel> : IDataTransferObject<TModel>
    {
        #region attribute type
        protected virtual Int32 GetInt(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToInt32(objectValue);
            }
            catch
            {
                return 0;
            }
        }

        protected virtual Int32? GetIntNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToInt32(objectValue);
            }
            catch
            {
                return null;
            }
        }

        protected virtual Int64 GetLong(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToInt64(objectValue);
            }
            catch
            {
                return 0;
            }
        }

        protected virtual Int64? GetLongNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToInt64(objectValue);
            }
            catch
            {
                return null;
            }
        }

        protected virtual float GetFloat(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToSingle(objectValue);
            }
            catch
            {
                return 0;
            }
        }

        protected virtual float? GetFloatNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToSingle(objectValue);
            }
            catch
            {
                return null;
            }
        }

        protected virtual Double GetDouble(object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return (Double)objectValue;
            }
            catch
            {
                return 0;
            }
        }

        protected virtual Double? GetDoubleNullable(object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return (Double)objectValue;
            }
            catch
            {
                return null;
            }
        }

        protected virtual Boolean GetBoolean(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToBoolean(objectValue);
            }
            catch
            {
                return NullValues.NullBoolean;
            }
        }

        protected virtual bool? GetBoolNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToBoolean(objectValue);
            }
            catch
            {
                return null;
            }
        }

        protected virtual String GetString(Object objectValue)
        {
            try
            {
                if (objectValue == null) return String.Empty;
                return Convert.ToString(objectValue);
            }
            catch
            {
                return String.Empty;
            }
        }

        protected virtual Decimal GetDecimal(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToDecimal(objectValue);
            }
            catch
            {
                return 0;
            }
        }


        protected virtual Decimal? GetDecimalNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToDecimal(objectValue);
            }
            catch
            {
                return null;
            }
        }

        protected virtual DateTime GetDateTime(object date, object time, String format = "dd/MM/yyyy HH:mm:ss")
        {
            try
            {
                return DateTime.ParseExact(((String)date) + " " + ((String)time), format, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return NullValues.NullDateTime;
            }
        }

        protected virtual DateTime? GetDateTimeFormatNullable(object date, object time, String format = "dd/MM/yyyy HH:mm:ss")
        {
            try
            {
                return DateTime.ParseExact(((String)date) + " " + ((String)time), format, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return null;
            }
        }

        protected virtual DateTime GetDateTime(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToDateTime(objectValue);
            }
            catch
            {
                return NullValues.NullDateTime;
            }
        }

        protected virtual DateTime? GetDateTimeFromConvertNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToDateTime(objectValue);
            }
            catch
            {
                return null;
            }
        }

        protected virtual DateTime GetDateTimeFormat(Object objectValue, String format = "dd/MM/yyyy")
        {
            try
            {
                return DateTime.ParseExact((String)objectValue, format, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return NullValues.NullDateTime;
            }
        }

        protected virtual DateTime? GetDateTimeFormatNullable(Object objectValue, String format = "dd/MM/yyyy")
        {
            try
            {
                return DateTime.ParseExact((String)objectValue, format, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                return null;
            }
        }

        protected virtual DateTime? GetDateTimeNullable(Object objectValue)
        {
            try
            {
                if (objectValue == null) throw new Exception();
                return Convert.ToDateTime(objectValue);
            }
            catch
            {
                return null;
            }
        }
        #endregion

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

        protected Boolean IsInitialized = false;
        protected virtual void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            IsInitialized = true;
        }


    }
}
