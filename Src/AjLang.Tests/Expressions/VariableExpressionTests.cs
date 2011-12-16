using System.Text;
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
            Environment environment = new Environment();
            VariableExpression expr = new VariableExpression("Foo");

            Assert.IsNull(expr.Evaluate(environment));
        }

        [TestMethod]
        public void EvaluateDefinedVariable()
        {
            Environment environment = new Environment();
            environment.SetValue("One", 1);
            VariableExpression expr = new VariableExpression("One");

            Assert.AreEqual(1, expr.Evaluate(environment));
        }
    }
}
