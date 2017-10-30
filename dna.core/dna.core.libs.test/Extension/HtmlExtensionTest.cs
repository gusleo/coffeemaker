using dna.core.libs.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace dna.core.libs.test.Html
{
    public class HtmlExtensionTest
    {
        [Fact]
        public void RemoveHtmlTag_Success()
        {
            string html = "<b>Just Test</b>";
            string expected = "Just Test";

            Assert.Equal(expected, html.RemoveHtmlTag());
        }

    }
}
