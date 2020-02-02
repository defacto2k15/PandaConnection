using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public static class RandomUtils
    {
        public static bool NextBool(this Random @this)
        {
            return @this.Next(2) == 1;
        }
    }
}
