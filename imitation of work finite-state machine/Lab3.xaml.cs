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
        FiniteStateMachine fm;
        string errormessage = "Правила грамматики: ";
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
            int nonterminalAlphabet = int.Parse(NonterminalSymbol.Text);//Определяет количество состояний
            int terminalAlphabet = int.Parse(TerminalSymbol.Text);//Определяет количество возможных команд
            //int numberFinState = int.Parse(countFinalState.Text);//Определяет количество финальных состояний

            nonterminalAlphabet++;
            terminalAlphabet++;

            finiteMachine = new ObservableCollection<ObservableCollection<string>>();
            fm = new FiniteStateMachine(finiteMachine, nonterminalAlphabet, terminalAlphabet);
            updateData();            
        }

        private bool checkRule()
        {
            string rule = RuleTransition.Text;

            for (int index = 0; index < rule.Length; index++)
            {
                if (index != 1)
                {
                    if (!(myConverter.checkExistSymbol(Convert.ToString(rule[index]), finiteMachine[0].Count, false)))
                    {
                        ErrorMessage.Text += "Извините данного нетерминала " + Convert.ToString(rule[index]) + " нет в алфавите. Введите правило заново.";
                        return false;
                    }
                }
                else if (index == 1)
                {
                    if (!(myConverter.checkExistSymbol(Convert.ToString(rule[index]), finiteMachine.Count, true)))
                    {
                        ErrorMessage.Text += "Извините данного терминала " + Convert.ToString(rule[index]) + " нет в алфавите. Введите правило заново.";
                        return false;
                    }
                }
            }

            return true;
        }

        public void updateData()
        {
            dataGrid2D.DataContext = this;
        }

        private void showHelp()
        {
            string senten = "Доступные терминалы: ";

            for (int i = 0; i < finiteMachine.Count - 1; i++)
            {
                if (i + 1 != finiteMachine.Count - 1)
                {
                    senten += myConverter.visualState(i + 32) + ", ";
                }
                else
                {
                    senten += myConverter.visualState(i + 32) + ".";
                }
            }

            listTerminal.Text = senten;

            senten = "Доступные нетерминалы: ";

            for (int i = 0; i < finiteMachine[0].Count - 1; i++)
            {
                if (i + 1 != finiteMachine[0].Count - 1)
                {
                    senten += myConverter.visualState(i) + ", ";
                }
                else
                {
                    senten += myConverter.visualState(i) + ".";
                }
            }

            listNonterminal.Text = senten;
            Start.Text = "Стартовый нетерминал: A";
            info.Text = "Правила вводятся в форме AaB (A -> aB), Aa (A -> a), AE (A -> E)";
        }

        #region Events Handler

        private void setChainOfCommand_Click(object sender, RoutedEventArgs e)
        {
            lineOfWorkProcess = new ObservableCollection<string>();
            workProcess.ItemsSource = lineOfWorkProcess;
            if (fm.ParseWord( lineOfWorkProcess, chainOfcommand.Text))
            {
                lineOfWorkProcess.Add("Цепочка относится к заданной регулярной грамматике");
            }           
        }

        private void SetSymbolOfAlphabet_Click(object sender, RoutedEventArgs e)
        {
            createObjectsForTable();
            showHelp();
        }

        private void SetRuleTransition_Click(object sender, RoutedEventArgs e)
        {
            if (checkRule())
            {
                fm.ConvertRuleTransition(RuleTransition.Text);
                errormessage += "  " + RuleTransition.Text + "  ";
                ErrorMessage.Text = errormessage;
            }
        }

        #endregion //Events Handler


    }
}
