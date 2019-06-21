using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Tool
    {
        public static bool checkInt(string input)
        {
            bool parse = Int32.TryParse(input, out int output);
            if (parse)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
