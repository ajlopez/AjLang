using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjLang.Tests
{
    [TestClass]
    public class EnvironmentTests
    {
        [TestMethod]
        public void GetUndefinedValue()
        {
            Environment environment = new Environment();
            Assert.IsNull(environment.GetValue("Foo"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Environment environment = new Environment();
            environment.SetValue("One", 1);
            Assert.AreEqual(1, environment.GetValue("One"));
        }
    }
}

