using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Expressions;
using AjLang.Commands;
using AjLang.Methods;

namespace AjLang.Tests.Expressions
{
    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedVariable()
        {
            Context context = new Context();
            NameExpression expr = new NameExpression("Foo");

            Assert.IsNull(expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedVariable()
        {
            Context context = new Context();
            context.SetValue("One", 1);
            NameExpression expr = new NameExpression("One");

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedMethod()
        {
            Context context = new Context();
            IList<ICommand> commands = new List<ICommand>();

            commands.Add(new SetVariableCommand("a", new ConstantExpression(1)));
            commands.Add(new SetVariableCommand("b", new ConstantExpression(2)));
            commands.Add(new ExpressionCommand(new NameExpression("b")));

            DefinedMethod method = new DefinedMethod(null, commands);

            context.SetValue("foo", method);

            NameExpression expr = new NameExpression("foo");

            object result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(method, result);
        }
    }
}
