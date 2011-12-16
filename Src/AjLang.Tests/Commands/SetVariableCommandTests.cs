using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Expressions;
using AjLang.Commands;

namespace AjLang.Tests.Commands
{
    [TestClass]
    public class SetVariableCommandTests
    {
        [TestMethod]
        public void SetVariable()
        {
            Context context = new Context();
            SetVariableCommand command = new SetVariableCommand("One", new ConstantExpression(1));
            command.Execute(context);

            Assert.AreEqual(1, context.GetValue("One"));
        }
    }
}
