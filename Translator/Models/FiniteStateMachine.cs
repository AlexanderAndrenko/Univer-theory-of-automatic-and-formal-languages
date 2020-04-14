using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Models
{
    public class FiniteStateMachine
    {
        public StateOfMachine[] conversionTable;

        public FiniteStateMachine(int n, int m)
        {
            conversionTable = new StateOfMachine[n];

            for (int index = 0; index < n; index++)
            {
                conversionTable[index] = new StateOfMachine(m);
            }
        }

            public class StateOfMachine
            {
                public char[] nextState;
                public StateOfMachine(int m)
                {
                    nextState = new char[m];
                }

                public void setValue(int state, char number)
                {
                    nextState[state] = number;
                }
            }

        public void setConversion(int index, int state, char number)
        {
            conversionTable[index].setValue(state, number);
        }

        
    }
}
