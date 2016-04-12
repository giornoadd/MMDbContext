using Mimo.Dbcontext.DataItemConversion;
using System;
using System.Data;

namespace Mimo.Dbcontext.Extensions
{
    public static class DataReaderExtension
    {

        public static Int32 ReadInt(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return 0;
            return reader.GetInt32(columnOrdinal);

        }

        public static Int32? ReadIntNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetInt32(columnOrdinal);
        }

        public static Int64 ReadLong(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return 0;
            return reader.GetInt64(columnOrdinal);
        }

        public static Int64? ReadLongNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetInt64(columnOrdinal);
        }

        public static float ReadFloat(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return 0;
            return reader.GetFloat(columnOrdinal);
        }

        public static float? ReadFloatNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetFloat(columnOrdinal);
        }

        public static Double ReadDouble(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return 0;
            return reader.GetDouble(columnOrdinal);
        }

        public static Double? ReadDoubleNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetDouble(columnOrdinal);
        }

        public static Boolean ReadBoolean(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return NullValues.NullBoolean;
            return reader.GetBoolean(columnOrdinal);
        }

        public static Boolean? ReadBoolNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetBoolean(columnOrdinal);
        }

        public static String ReadString(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return String.Empty;
            return reader.GetString(columnOrdinal);
        }

        public static Guid ReadGuid(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return Guid.Empty;
            return reader.GetGuid(columnOrdinal);
        }

        

        public static Decimal ReadDecimal(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return 0;
            return reader.GetDecimal(columnOrdinal);
        }


        public static Decimal? ReadDecimalNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetDecimal(columnOrdinal);
        }

        public static DateTime ReadDateTime(this IDataReader reader, Int32 datecolumnOrdinal, Int32 timecolumnOrdinal, String format = "dd/MM/yyyy HH:mm:ss")
        {
            if (reader.IsDBNull(datecolumnOrdinal) || reader.IsDBNull(timecolumnOrdinal)) return NullValues.NullDateTime;

            try
            {
                return DateTime.ParseExact((reader.GetString(datecolumnOrdinal)) + " " + (reader.GetString((timecolumnOrdinal))), format, System.Globalization.CultureInfo.CurrentCulture);

            }
            catch (Exception)
            {
                return NullValues.NullDateTime;
            }
        }

        public static DateTime? ReadDateTimeFormatNullable(this IDataReader reader, Int32 datecolumnOrdinal, Int32 timecolumnOrdinal, String format = "dd/MM/yyyy HH:mm:ss")
        {
            if (reader.IsDBNull(datecolumnOrdinal) || reader.IsDBNull(timecolumnOrdinal)) return null;

            try
            {
                return DateTime.ParseExact((reader.GetString(datecolumnOrdinal)) + " " + (reader.GetString((timecolumnOrdinal))), format, System.Globalization.CultureInfo.CurrentCulture);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime ReadDateTime(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return NullValues.NullDateTime;
            return reader.GetDateTime(columnOrdinal);
        }

        public static DateTime ReadStringDate(this IDataReader reader, Int32 columnOrdinal, String format = "dd/MM/yyyy")
        {
            if (reader.IsDBNull(columnOrdinal)) return NullValues.NullDateTime;
            return DateTime.ParseExact(reader.GetString((columnOrdinal)), format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public static DateTime? ReadStringDateTimeNullable(this IDataReader reader, Int32 columnOrdinal, String format = "dd/MM/yyyy")
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return DateTime.ParseExact(reader.GetString((columnOrdinal)), format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public static DateTime? ReadDateTimeNullable(this IDataReader reader, Int32 columnOrdinal)
        {
            if (reader.IsDBNull(columnOrdinal)) return null;
            return reader.GetDateTime(columnOrdinal);
           
        }
    }
}
