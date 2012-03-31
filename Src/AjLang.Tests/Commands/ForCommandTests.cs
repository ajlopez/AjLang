using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Commands;
using AjLang.Expressions;
using AjLang.Methods;

namespace AjLang.Tests.Commands
{
    [TestClass]
    public class ForCommandTests
    {
        [TestMethod]
        public void CreateAndExecuteForCommand()
        {
            var command = new SetVariableCommand("a", new VariableExpression("k"));
            var forcommand = new ForCommand("k", new ConstantExpression(new int[] { 1, 2, 3 }), command);

            Context context = new Context();

            var result = forcommand.Execute(context);

            Assert.IsNull(result);
            Assert.AreEqual("k", forcommand.VariableName);
            Assert.IsNotNull(forcommand.Expression);
            Assert.IsNotNull(forcommand.Command);
            Assert.AreEqual(3, context.GetValue("k"));
            Assert.AreEqual(3, context.GetValue("a"));
        }
    }
}
