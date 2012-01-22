using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Commands;
using AjLang.Expressions;

namespace AjLang.Tests.Commands
{
    [TestClass]
    public class ExpressionCommandTests
    {
        [TestMethod]
        public void ExecuteConstantExpression()
        {
            ExpressionCommand command = new ExpressionCommand(new ConstantExpression(1));
            Assert.AreEqual(1, command.Execute(null));
        }
    }
}
