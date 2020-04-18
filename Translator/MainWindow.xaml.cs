using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Translator.Models;

namespace Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void SetSymbolOfAlphabet_Click(object sender, RoutedEventArgs e)
        {

            int numberOfState =  int.Parse(SymbolOfState.Text);
            int numberOfCommand = int.Parse(TerminalSymbol.Text);

            FiniteStateMachine[] textcol = new FiniteStateMachine[numberOfState];

            for (int i = 0; i < numberOfState; i++)
            {
                textcol[i] = new FiniteStateMachine(numberOfCommand, "a");                
                ConversionTableDataGrid.Columns.Add(textcol[i].nameOfColumn);

                ObservableCollection<FiniteStateMachine> dataForColumn = GetData(textcol[i].conversion);
                ConversionTableDataGrid.DataContext = textcol[i].conversion;
            }         

        }
    }
}
