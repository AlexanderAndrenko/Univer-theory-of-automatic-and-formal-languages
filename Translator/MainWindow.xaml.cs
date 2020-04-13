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
            FiniteStateMachine a = new FiniteStateMachine(2, 2);

            int [] array = new int[2] { 2, 1 };

            //ConversionTableDataGrid.ItemsSource = a.conversionTable;
        }
    }
}
