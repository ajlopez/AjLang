using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Commands;
using AjLang.Expressions;
using AjLang.Methods;

namespace AjLang.Tests.Expressions
{
    [TestClass]
    public class CallExpressionTests
    {
        [TestMethod]
        public void ExecuteCallExpression()
        {
            Context context = new Context();
            IList<ICommand> commands = new List<ICommand>();

            commands.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commands.Add(new SetVariableCommand("b", new ConstantExpression(2)));
            commands.Add(new ExpressionCommand(new VariableExpression("b")));

            DefinedMethod method = new DefinedMethod(commands);

            CallExpression callexpr = new CallExpression(new ConstantExpression(method), null);

            object result = callexpr.Evaluate(context);

            Assert.AreEqual(2, result);
        }
    }
}
