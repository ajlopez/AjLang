﻿namespace AjLang.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Language;
    using System.IO;

    public class Puts : ICallable
    {
        private TextWriter writer;

        public Puts(TextWriter writer)
        {
            this.writer = writer;
        }

        public object Call(Context context, IList<object> arguments)
        {
            if (arguments != null && arguments.Count > 0)
            {
                foreach (var argument in arguments)
                    writer.WriteLine(argument);

                return null;
            }

            writer.WriteLine();
            return null;
        }
    }
}
