namespace AjLang.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Predicates
    {
        public static bool IsFalse(object obj)
        {
            if (obj == null)
                return true;
            if (obj is bool && ((bool)obj) == false)
                return true;

            return false;
        }

        public static bool IsTrue(object obj)
        {
            return !IsFalse(obj);
        }
    }
}

