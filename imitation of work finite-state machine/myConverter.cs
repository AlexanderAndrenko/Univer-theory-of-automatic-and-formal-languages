using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{    
    public class myConverter
    {
        public static string visualState(int valueOfCommand)
        {
            string result = Convert.ToString(Convert.ToChar(valueOfCommand + 65));

            return result;
        }

        public static int convertToNumber(string letter)
        {
            int result = Convert.ToInt32(Convert.ToChar(letter)) - 65; ;

            return result;
        }

        public static bool checkExistSymbol(string symbol, int size, bool terminal)
        {
            for (int i = 0; i < size; i++)
            {
                if (!terminal)
                {
                    if (symbol == visualState(i))
                    {
                        return true;
                    }
                }
                else
                {
                    if (symbol == visualState(i + 32))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
