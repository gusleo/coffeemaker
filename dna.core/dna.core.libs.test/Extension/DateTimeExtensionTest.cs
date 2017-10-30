using dna.core.libs.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace dna.core.libs.test
{
    public class DateTimeExtensionTest
    {
        [Fact]
        public void Read_StartOfDay()
        {
            DateTime _date = DateTime.Parse("2017-05-13 20:18:00");
            DateTime _excepeted = DateTime.Parse("2017-05-13 12:00:00 AM");
           
            Assert.Equal(_excepeted, _date.StartOfDay());
        }

        [Fact]
        public void Read_EndOfDay()
        {
            DateTime _date = DateTime.Parse("2017-05-13 20:18:00");
            DateTime _excepeted = DateTime.Parse("2017-05-13 23:59:59.9990000");                  
            Assert.Equal(_excepeted, _date.EndOfDay());
        }
    }
}
