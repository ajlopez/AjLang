namespace AjLang.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum TokenType
    {
        Name = 0,
        Integer = 1,
        Operator = 2,
        Separator = 3,
        EndOfLine = 4
    }
}
