using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Models
{
    class FiniteStateMachine
    {
        public char[,] conversionTable;

        public FiniteStateMachine(int n, int m)
        {
            conversionTable = new char[n, m];
        }

    }
}
