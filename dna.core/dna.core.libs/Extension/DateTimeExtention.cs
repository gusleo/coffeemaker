using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.libs.Extension
{
    public static class DateTimeExtention
    {
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddMilliseconds(-1);
        }
    }
}
