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
    public class DefineCommandTests
    {
        [TestMethod]
        public void DefineCommand()
        {
            IList<ICommand> commands = new List<ICommand>();

            commands.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commands.Add(new SetVariableCommand("b", new ConstantExpression(2)));

            DefineCommand command = new DefineCommand("foo", commands);

            Assert.AreEqual("foo", command.Name);
            Assert.AreEqual(commands, command.Commands);
        }

        [TestMethod]
        public void ExecuteDefineCommand()
        {
            Context context = new Context();
            IList<ICommand> commands = new List<ICommand>();

            commands.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commands.Add(new SetVariableCommand("b", new ConstantExpression(2)));
            commands.Add(new ExpressionCommand(new VariableExpression("b")));

            DefineCommand command = new DefineCommand("foo", commands);

            object result = command.Execute(context);

            Assert.IsNull(result);
            Assert.IsInstanceOfType(context.GetValue("foo"), typeof(DefinedMethod));
        }
    }
}
