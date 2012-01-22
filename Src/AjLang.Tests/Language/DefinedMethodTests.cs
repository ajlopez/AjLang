using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Methods;
using AjLang.Commands;
using AjLang.Expressions;

namespace AjLang.Tests.Language
{
    [TestClass]
    public class DefinedMethodTests
    {
        [TestMethod]
        public void ExecuteDefinedMethod()
        {
            Context context = new Context();
            IList<ICommand> commands = new List<ICommand>();

            commands.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commands.Add(new SetVariableCommand("b", new ConstantExpression(2)));
            commands.Add(new ExpressionCommand(new VariableExpression("b")));

            DefinedMethod method = new DefinedMethod(commands);

            object result = method.Call(context, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);

            Assert.IsNull(context.GetValue("a"));
            Assert.IsNull(context.GetValue("b"));
        }
    }
}
