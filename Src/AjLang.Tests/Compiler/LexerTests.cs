﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjLang.Compiler;

namespace AjLang.Tests.Compiler
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("foo");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  foo   ");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSimpleAssignmentOperator()
        {
            Lexer lexer = new Lexer("=");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("=", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
