using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class SysEnhance
    {
        public static int ToInt(this String str)
        {
            int ret = 0;

            ret = Convert.ToInt32(str);

            return ret;
        }
        public static float ToFloat(this String str)
        {
            float ret = 0;

            ret = Convert.ToSingle(str);

            return ret;
        }
    }
}

