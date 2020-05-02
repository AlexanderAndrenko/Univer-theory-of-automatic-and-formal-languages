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
using finite_state_machine;

namespace Translator
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Lab3 : Page
    {
        public Lab3()
        {
            InitializeComponent();
        }

        private void propertiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                ListBoxItem listBoxItem = e.AddedItems[0] as ListBoxItem;
                Binding datagrid2dBinding = new Binding();
                datagrid2dBinding.Path = new PropertyPath(listBoxItem.Content.ToString());
                dataGrid2D.SetBinding(DataGrid2D.ItemsSource2DProperty, datagrid2dBinding);
            }
        }

        public ObservableCollection<ObservableCollection<string>> finiteMachine { get; set; }
        public ObservableCollection<string> lineOfWorkProcess { get; set; }

        public void createObjectsForTable()
        {
            int numberOfState = int.Parse(SymbolOfState.Text);//Определяет количество состояний
            int numberOfCommand = int.Parse(TerminalSymbol.Text);//Определяет количество возможных команд
            int numberFinState = int.Parse(countFinalState.Text);//Определяет количество финальных состояний

            numberOfState++;
            numberOfCommand++;

            finiteMachine = new ObservableCollection<ObservableCollection<string>>();

            FiniteStateMachine fm = new FiniteStateMachine(finiteMachine, numberOfState, numberOfCommand, numberFinState);

            updateData();

            string senten = "Доступные терминалы: ";

            for (int i = 0; i < finiteMachine[0].Count - 1; i++)
            {
                if (i + 1 != finiteMachine[0].Count - 1)
                {
                    senten += myConverter.visualState(i + 32) + ", ";
                }
                else
                {
                    senten += myConverter.visualState(i + 32) + ".";
                }
            }

            listSymbol.Text = senten;
            Start.Text = "Стартовый нетерминал: A";
            Final.Text = "Финальный нетерминал:  " + myConverter.visualState(numberOfState - 1) + " = t";

            transition.Text = "Терминал " + myConverter.visualState(finiteMachine[0].Count - 1) + " объявляется пустой цепочкой. Нетерминалы берутся в <> скобки." + 
                              " Отсутствие правила перехода обозначается пустой ячейкой" + 
                              " Пример: правило А->aB и A->а, записывается текст <Bt> в пересечении колонки А и строки а.";
            transition.Visibility = Visibility;
        }

        public void updateData()
        {
            dataGrid2D.DataContext = this;
        }

        #region Events Handler

        private void setChainOfCommand_Click(object sender, RoutedEventArgs e)
        {
            lineOfWorkProcess = new ObservableCollection<string>();
            workProcess.ItemsSource = lineOfWorkProcess;
        }


        #endregion //Events Handler

        private void SetSymbolOfAlphabet_Click(object sender, RoutedEventArgs e)
        {
            createObjectsForTable();
        }
    }
}
