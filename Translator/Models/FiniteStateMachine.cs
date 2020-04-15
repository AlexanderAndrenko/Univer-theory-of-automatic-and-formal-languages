using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            nameOfColumn = new DataGridTextColumn();
            nameOfColumn.Binding = conversion;
            nameOfColumn.Header = Convert.ToString(name);
            
        }   
    }
}
