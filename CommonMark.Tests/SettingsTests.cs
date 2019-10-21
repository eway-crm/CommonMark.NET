using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonMark.Tests
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        [TestCategory("Inlines - Soft line break")]
        public void RenderSoftLineBreakAsLineBreak()
        {
            var settings = CommonMarkSettings.Default.Clone();
            settings.RenderSoftLineBreaksAsLineBreaks = true;
            Helpers.ExecuteTest("A\nB\nC", "<p>A<br />\nB<br />\nC</p>", settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Soft line break")]
        public void RenderEmptyLines()
        {
            var settings = CommonMarkSettings.Default.Clone();
            settings.RenderSoftLineBreaksAsLineBreaks = true;
            settings.RenderEmptyLines = true;
            Helpers.ExecuteTest("A\n\nB\nC", "<p>A<br />\n<br />\nB<br />\nC</p>", settings);

            settings.RenderSoftLineBreaksAsLineBreaks = false;
            settings.RenderEmptyLines = true;
            Helpers.ExecuteTest("A\n\nB\nC\n\n* A\n* B\n\n\n", "<p>A</p>\n<p></p>\n<p>B</p>\n<p>C</p>\n<p></p>\n<ul>\n<li>A</li>\n<li>B</li>\n</ul>\n<p></p>\n<p></p>", settings);
            Helpers.ExecuteTest("* A\n* B\n\n1. One\n2. Two", "<ul>\n<li>A</li>\n<li>B</li>\n</ul>\n<p></p>\n<ol>\n<li>One</li>\n<li>Two</li>\n</ol>", settings);
        }
    }
}
