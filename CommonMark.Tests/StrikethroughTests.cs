﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonMark.Tests
{
    [TestClass]
    public class StrikethroughTests
    {
        private static CommonMarkSettings _settings;
        private static CommonMarkSettings Settings
        {
            get
            {
                var s = _settings;
                if (s == null)
                {
                    s = CommonMarkSettings.Default.Clone();
                    s.AdditionalFeatures |= CommonMarkAdditionalFeatures.StrikethroughTilde;
                    _settings = s;
                }
                return s;
            }
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughDisabledByDefault()
        {
            Helpers.ExecuteTest("foo ~~bar~~", "<p>foo ~~bar~~</p>");
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughInvalid1()
        {
            Helpers.ExecuteTest("foo ~bar~", "<p>foo ~bar~</p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample1()
        {
            Helpers.ExecuteTest("foo ~~bar~~", "<p>foo <s>bar</s></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample2()
        {
            Helpers.ExecuteTest("foo ~~~bar~~", "<p>foo ~<s>bar</s></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample3()
        {
            Helpers.ExecuteTest("foo ~~~~bar~~ asd~~", "<p>foo <s><s>bar</s> asd</s></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample4()
        {
            Helpers.ExecuteTest("foo ~~*bar~~*", "<p>foo <s>*bar</s>*</p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample5()
        {
            Helpers.ExecuteTest("foo *~~bar~~*", "<p>foo <em><s>bar</s></em></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample6()
        {
            Helpers.ExecuteTest("foo **~~bar**~~", "<p>foo <strong>~~bar</strong>~~</p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample7()
        {
            Helpers.ExecuteTest("~~bar~~~", "<p><s>bar</s>~</p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample8()
        {
            // make sure that the fenced code blocks are not broken because of this feature
            Helpers.ExecuteTest("~~~foo\n~~", "<pre><code class=\"language-foo\">~~\n</code></pre>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample9()
        {
            Helpers.ExecuteTest("foo ~~~~bar~~~~", "<p>foo <s><s>bar</s></s></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample10()
        {
            // '[' char in the middle will delay the ~~ match to the post-process phase.
            Helpers.ExecuteTest("foo ~~~~ba[r~~~~", "<p>foo <s><s>ba[r</s></s></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample10a()
        {
            // '[' char in the middle will delay the ~~ match to the post-process phase.
            Helpers.ExecuteTest("foo ~~~~ba[r~~", "<p>foo ~~<s>ba[r</s></p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample10b()
        {
            // '[' char in the middle will delay the ~~ match to the post-process phase.
            Helpers.ExecuteTest("foo ~~ba[r~~~~", "<p>foo <s>ba[r</s>~~</p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample10c()
        {
            // '[' char in the middle will delay the ~~ match to the post-process phase.
            Helpers.ExecuteTest("foo ~~ba[r~~~", "<p>foo <s>ba[r</s>~</p>", Settings);
        }

        [TestMethod]
        [TestCategory("Inlines - Strikethrough")]
        public void StrikethroughExample10d()
        {
            // '[' char in the middle will delay the ~~ match to the post-process phase.
            Helpers.ExecuteTest("~~~~[foo~~ bar", "<p>~~<s>[foo</s> bar</p>", Settings);
        }
    }
}
