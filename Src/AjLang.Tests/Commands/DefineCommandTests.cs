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
            IList<ICommand> commandlist = new List<ICommand>();

            commandlist.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commandlist.Add(new SetVariableCommand("b", new ConstantExpression(2)));

            CompositeCommand commands = new CompositeCommand(commandlist);

            DefineCommand command = new DefineCommand("foo", null, commands);

            Assert.AreEqual("foo", command.Name);
            Assert.AreEqual(commands, command.Command);
        }

        [TestMethod]
        public void ExecuteDefineCommand()
        {
            Context context = new Context();
            IList<ICommand> commandlist = new List<ICommand>();

            commandlist.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commandlist.Add(new SetVariableCommand("b", new ConstantExpression(2)));
            commandlist.Add(new ExpressionCommand(new VariableExpression("b")));

            CompositeCommand commands = new CompositeCommand(commandlist);

            DefineCommand command = new DefineCommand("foo", null, commands);

            object result = command.Execute(context);

            Assert.IsNull(result);
            Assert.IsInstanceOfType(context.GetValue("foo"), typeof(DefinedMethod));
        }
    }
}
