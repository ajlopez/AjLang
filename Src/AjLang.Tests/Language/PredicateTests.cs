using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Language;

namespace AjLang.Tests.Language
{
    [TestClass]
    public class PredicateTests
    {
        [TestMethod]
        public void IsFalse()
        {
            Assert.IsTrue(Predicates.IsFalse(null));
            Assert.IsTrue(Predicates.IsFalse(false));
            Assert.IsFalse(Predicates.IsFalse(true));
            Assert.IsFalse(Predicates.IsFalse(string.Empty));
            Assert.IsFalse(Predicates.IsFalse(0));
            Assert.IsFalse(Predicates.IsFalse("foo"));
            Assert.IsFalse(Predicates.IsFalse(10));
        }

        [TestMethod]
        public void IsTrue()
        {
            Assert.IsFalse(Predicates.IsTrue(null));
            Assert.IsFalse(Predicates.IsTrue(false));
            Assert.IsTrue(Predicates.IsTrue(true));
            Assert.IsTrue(Predicates.IsTrue(string.Empty));
            Assert.IsTrue(Predicates.IsTrue(0));
            Assert.IsTrue(Predicates.IsTrue("foo"));
            Assert.IsTrue(Predicates.IsTrue(10));
        }
    }
}
