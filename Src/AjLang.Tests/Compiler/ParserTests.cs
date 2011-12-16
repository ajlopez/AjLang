using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Compiler;
using AjLang.Expressions;

namespace AjLang.Tests.Compiler
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("123");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)expr;

            Assert.AreEqual(123, cexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
