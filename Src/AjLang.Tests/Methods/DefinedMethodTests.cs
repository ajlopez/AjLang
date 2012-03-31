using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Methods;
using AjLang.Commands;
using AjLang.Expressions;

namespace AjLang.Tests.Methods
{
    [TestClass]
    public class DefinedMethodTests
    {
        [TestMethod]
        public void ExecuteDefinedMethod()
        {
            Context context = new Context();
            IList<ICommand> commandlist = new List<ICommand>();

            commandlist.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commandlist.Add(new SetVariableCommand("b", new ConstantExpression(2)));
            commandlist.Add(new ExpressionCommand(new VariableExpression("b")));

            CompositeCommand commands = new CompositeCommand(commandlist);

            DefinedMethod method = new DefinedMethod(null, commands);

            object result = method.Call(context, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);

            Assert.IsNull(context.GetValue("a"));
            Assert.IsNull(context.GetValue("b"));
        }
    }
}
