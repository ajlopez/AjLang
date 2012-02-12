using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Methods;
using System.IO;

namespace AjLang.Tests.Methods
{
    [TestClass]
    public class PutTests
    {
        [TestMethod]
        public void WriteLine()
        {
            StringWriter writer = new StringWriter();
            Puts puts = new Puts(writer);
            var result = puts.Call(null, new object[] { "hello" });
            Assert.IsNull(result);
            writer.Close();
            Assert.AreEqual("hello\r\n", writer.ToString());
        }

        [TestMethod]
        public void WriteTwoLines()
        {
            StringWriter writer = new StringWriter();
            Puts puts = new Puts(writer);
            var result = puts.Call(null, new object[] { "hello", "world" });
            Assert.IsNull(result);
            writer.Close();
            Assert.AreEqual("hello\r\nworld\r\n", writer.ToString());
        }

        [TestMethod]
        public void WriteEmptyLine()
        {
            StringWriter writer = new StringWriter();
            Puts puts = new Puts(writer);
            var result = puts.Call(null, null);
            Assert.IsNull(result);
            writer.Close();
            Assert.AreEqual("\r\n", writer.ToString());
        }
    }
}
