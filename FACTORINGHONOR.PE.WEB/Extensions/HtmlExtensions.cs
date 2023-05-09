using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.Extensions
{
    public static class HtmlExtensions
    {
        public static string ToPercentage(this double number)
        {
            return number.ToString("#.00");
        }
    }
}
