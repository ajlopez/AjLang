using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Compiler;
using AjLang.Expressions;
using AjLang.Commands;

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

        [TestMethod]
        public void ParseSimpleString()
        {
            Parser parser = new Parser("\"foo\"");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)expr;

            Assert.AreEqual("foo", cexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseVariable()
        {
            Parser parser = new Parser("foo");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(VariableExpression));

            VariableExpression vexpr = (VariableExpression)expr;

            Assert.AreEqual("foo", vexpr.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleCall()
        {
            Parser parser = new Parser("puts 1");
            IExpression expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(CallExpression));

            CallExpression cexpr = (CallExpression)expr;

            Assert.IsInstanceOfType(cexpr.Expression, typeof(NameExpression));
            Assert.AreEqual(1, cexpr.Arguments.Count());
            Assert.IsInstanceOfType(cexpr.Arguments.First(), typeof(ConstantExpression));
        }

        [TestMethod]
        public void ParseSimpleCallAsCommand()
        {
            Parser parser = new Parser("puts 1");
            ICommand cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            ExpressionCommand ccmd = (ExpressionCommand) cmd;

            Assert.IsInstanceOfType(ccmd.Expression, typeof(CallExpression));
        }

        [TestMethod]
        public void ParseSimpleCallAsCommandPrecededByNewLine()
        {
            Parser parser = new Parser("\r\nputs 1");
            ICommand cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            ExpressionCommand ccmd = (ExpressionCommand)cmd;

            Assert.IsInstanceOfType(ccmd.Expression, typeof(CallExpression));
        }

        [TestMethod]
        public void ParseTwoSimpleCallsAsCommands()
        {
            Parser parser = new Parser("puts 1\r\nputs 2\r\n");
            ICommand cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            ExpressionCommand ccmd = (ExpressionCommand)cmd;

            Assert.IsInstanceOfType(ccmd.Expression, typeof(CallExpression));

            cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            ccmd = (ExpressionCommand)cmd;

            Assert.IsInstanceOfType(ccmd.Expression, typeof(CallExpression));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleAssignmentCommand()
        {
            Parser parser = new Parser("a=1");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseTwoLineSimpleAssignmentCommands()
        {
            Parser parser = new Parser("a=1\r\nb=1");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            scommand = (SetVariableCommand)command;

            Assert.AreEqual("b", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSeparatedSimpleAssignmentCommands()
        {
            Parser parser = new Parser("a=1;b=1");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            scommand = (SetVariableCommand)command;

            Assert.AreEqual("b", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseTwoCommands()
        {
            Parser parser = new Parser("a=1\r\nb=1");
            IList<ICommand> commands = parser.ParseCommands();

            Assert.IsNotNull(commands);
            Assert.AreEqual(2, commands.Count);

            ICommand command = commands[0];

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            SetVariableCommand scommand = (SetVariableCommand)command;

            Assert.AreEqual("a", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            ConstantExpression cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            command = commands[1];

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));

            scommand = (SetVariableCommand)command;

            Assert.AreEqual("b", scommand.Name);
            Assert.IsInstanceOfType(scommand.Expression, typeof(ConstantExpression));

            cexpr = (ConstantExpression)scommand.Expression;

            Assert.AreEqual(1, cexpr.Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleDefineCommand()
        {
            Parser parser = new Parser("def foo\r\na=1\r\nb=1\r\nend\r\n");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(DefineCommand));

            DefineCommand defcommand = (DefineCommand)command;

            Assert.AreEqual("foo", defcommand.Name);

            IEnumerable<ICommand> commands = defcommand.Commands;

            Assert.IsNotNull(commands);
            Assert.AreEqual(2, commands.Count());

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleDefineCommandWithArguments()
        {
            Parser parser = new Parser("def foo(x, y)\r\na=x\r\nb=y\r\nend\r\n");
            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(DefineCommand));

            DefineCommand defcommand = (DefineCommand)command;

            Assert.AreEqual("foo", defcommand.Name);
            Assert.IsNotNull(defcommand.ArgumentNames);
            Assert.AreEqual(2, defcommand.ArgumentNames.Count());

            IEnumerable<ICommand> commands = defcommand.Commands;

            Assert.IsNotNull(commands);
            Assert.AreEqual(2, commands.Count());

            Assert.IsNull(parser.ParseCommand());
        }
    }
}
