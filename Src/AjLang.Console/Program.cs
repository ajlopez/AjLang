﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjLang.Compiler;
using AjLang.Commands;

namespace AjLang.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer(System.Console.In);
            Context context = new Context();
            Parser parser = new Parser(lexer);

            for (ICommand cmd = parser.ParseCommand(); cmd != null; cmd = parser.ParseCommand())
                ;
        }
    }
}
