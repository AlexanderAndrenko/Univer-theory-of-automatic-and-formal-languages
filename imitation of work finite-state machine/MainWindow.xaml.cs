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
using DataGrid2DLibrary;
using Translator;
using finite_state_machine;

namespace Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion //Constructor

        private void Laboratory2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Lab2());
        }

        private void Laboratory3_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Lab3());
        }
    }
}
