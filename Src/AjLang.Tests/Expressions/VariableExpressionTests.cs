﻿using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Expressions;

namespace AjLang.Tests.Expressions
{
    [TestClass]
    public class VariableExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedVariable()
        {
            Context context = new Context();
            VariableExpression expr = new VariableExpression("Foo");

            Assert.IsNull(expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedVariable()
        {
            Context context = new Context();
            context.SetValue("One", 1);
            VariableExpression expr = new VariableExpression("One");

            Assert.AreEqual(1, expr.Evaluate(context));
        }
    }
}