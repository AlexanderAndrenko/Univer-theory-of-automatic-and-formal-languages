using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Translator;

namespace Translator.Models
{
    public class FiniteStateMachine
    {
        public string[] conversion;
        public DataGridTextColumn nameOfColumn;
        public Binding dataColumn;
        public FiniteStateMachine(int command, string name)
        {
            conversion = new string[command];

            for (int i = 0; i < command; i++)
            {
                conversion[i] = Convert.ToString(i);
            }

            nameOfColumn = new DataGridTextColumn();
            dataColumn = new Binding(Convert.ToString(conversion));
            nameOfColumn.Binding = dataColumn;
            nameOfColumn.Header = Convert.ToString(name);
        }   
    }
}
