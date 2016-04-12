using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mimo.Dbcontext.DataItemConversion
{
    public class NullValues
    {
        public static DateTime NullDateTime = DateTime.MinValue;
        public static Guid NullGuid = Guid.Empty;
        public static Int32 NullInt = Int32.MinValue;
        public static Single NullFloat = Single.MinValue;
        public static Decimal NullDecimal = Decimal.MinValue;
        public static Int64 NullLong = Int64.MinValue;
        public static String NullString = null;
        public static Double NullDouble = Double.MinValue;
        public static Boolean NullBoolean = false;
    }
}
