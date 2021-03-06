﻿using System;
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
        private CommonMarkSettings GetSettings()
        {
            var settings = CommonMarkSettings.Default.Clone();
            settings.HtmlEntityEncode = true;
            settings.RenderSoftLineBreaksAsLineBreaks = false;
            settings.RenderEmptyLines = true;
            settings.AllowWhiteSpace = true;
            settings.AllowCodeIndent = false;

            return settings;
        }

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
            settings.AllowWhiteSpace = true;

            Helpers.ExecuteTest("A\n\nB\nC\n\n* A\n* B\n\n\n", "<p>A</p>\n<p></p>\n<p>B</p>\n<p>C</p>\n<p></p>\n<ul>\n<li>A</li>\n<li>B</li>\n</ul>\n<p></p>\n<p></p>", settings);
            Helpers.ExecuteTest("* A\n* B\n\n1. One\n2. Two", "<ul>\n<li>A</li>\n<li>B</li>\n</ul>\n<p></p>\n<ol>\n<li>One</li>\n<li>Two</li>\n</ol>", settings);
        }

        [TestMethod]
        public void HtmlEntityEncode()
        {
            var settings = this.GetSettings();

            Helpers.ExecuteTest("This is <b>bold</b> text.", "<p>This is &lt;b&gt;bold&lt;/b&gt; text.</p>", settings);

            Helpers.ExecuteTest("<b>Text</b>", "<p>&lt;b&gt;Text&lt;/b&gt;</p>", settings);

            Helpers.ExecuteTest(@"<hr>
<b>**text**</b>
<h1>nadpis</h1>",

@"<p>&lt;hr&gt;</p>
<p>&lt;b&gt;<strong>text</strong>&lt;/b&gt;</p>
<p>&lt;h1&gt;nadpis&lt;/h1&gt;</p>", settings);
        }

        [TestMethod]
        public void TabTest()
        {
            var settings = this.GetSettings();

            Helpers.ExecuteTest(" \t\r\n\r\n\tText", "<p> \t</p>\r\n<p></p>\r\n<p>\tText</p>", settings);
        }

        [TestMethod]
        public void NumberingListTest()
        {
            var settings = this.GetSettings();

            Helpers.ExecuteTest("1. One\r\n2. Two", "<ol>\r\n<li>\r\nOne\r\n</li>\r\n<li>\r\nTwo\r\n</li>\r\n</ol>", settings);
        }

        [TestMethod]
        public void PreTest()
        {
            var settings = this.GetSettings();

            Helpers.ExecuteTest(@"       wfdergedgedge
dgfdgfdgd
", "<p>       wfdergedgedge</p>\r\n<p>dgfdgfdgd</p>", settings);
        }

        [TestMethod]
        public void BulletListTest()
        {
            var settings = this.GetSettings();

            Helpers.ExecuteTest("* A\r\n    * B\r\n* C", "<ul>\r\n<li>A\r\n<ul>\r\n<li>B</li>\r\n</ul>\r\n</li>\r\n<li>C</li>\r\n</ul>", settings);
        }
    }
}
