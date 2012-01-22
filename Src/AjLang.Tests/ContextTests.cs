using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjLang.Tests
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void GetUndefinedValue()
        {
            Context environment = new Context();
            Assert.IsNull(environment.GetValue("Foo"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Context environment = new Context();
            environment.SetValue("One", 1);
            Assert.AreEqual(1, environment.GetValue("One"));
        }

        [TestMethod]
        public void SetAndGetValueWithParent()
        {
            Context parent = new Context();
            Context environment = new Context(parent);
            environment.SetValue("One", 1);
            Assert.AreEqual(1, environment.GetValue("One"));
            Assert.IsNull(parent.GetValue("One"));
        }

        [TestMethod]
        public void GetValueFromParent()
        {
            Context parent = new Context();
            Context environment = new Context(parent);
            parent.SetValue("One", 1);
            Assert.AreEqual(1, environment.GetValue("One"));
        }
    }
}

