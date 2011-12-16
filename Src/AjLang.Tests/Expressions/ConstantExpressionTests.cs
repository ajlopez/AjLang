using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Expressions;

namespace AjLang.Tests.Expressions
{
    [TestClass]
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateInteger()
        {
            ConstantExpression expr = new ConstantExpression(1);

            Assert.AreEqual(1, expr.Evaluate());
        }
    }
}
