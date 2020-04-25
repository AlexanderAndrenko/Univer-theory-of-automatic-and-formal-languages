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
using DataGrid2DLibrary;

namespace Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        #region Declaration

        bool isCreated = false;//Переменная состояния - созданы ли объекты представляющие конечный автомат
        

        #endregion //Declaration

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion //Constructor

        /*Функция обработки нажатия на кнопку SetSymbolOfAlphabet*/
        private void SetSymbolOfAlphabet_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                createObjectsForTable();
            }

            isCreated = true;
        }


        public void updateData()
        {
            dataGrid2D.DataContext = this;
        }

        /*Функция обработки нажатия на кнопку SetConversionTable*/
        

        /*Функция создаёт объекты отображающие внутренее представление детерминированого конечного автомата (один объект - одно состояние*/
        public void createObjectsForTable()
        {
            int numberOfState = int.Parse(SymbolOfState.Text);//Определяет количество состояний
            int numberOfCommand = int.Parse(TerminalSymbol.Text);//Определяет количество возможных команд

            finiteMachine = new ObservableCollection<ObservableCollection<string>>();

            for (int i = 0; i < numberOfState; i++)
            {
                finiteMachine.Add(new ObservableCollection<string>());

                for (int j = 0; j < numberOfCommand; j++)
                {
                    finiteMachine[i].Add((visualState(i)).ToString());
                }

            }

            updateData();


            /*визуализация сотояния и команды для строки заполнения таблицы переходов 
            Command.Text = Convert.ToString(command + 1);
            State.Text = visualState(state);*/
        }
        public ObservableCollection<ObservableCollection<string>> finiteMachine { get; set; }

        /*функция конвертации номера состояния в строку*/
        public string visualState(int valueOfCommand)
        {
            string result = Convert.ToString(Convert.ToChar(valueOfCommand + 97));

            return result;
        }

        public int convertToNumber(char letter)
        {
            int result = Convert.ToInt32(letter) - 97; ;

            return result;
        }

        #region EventsHandlers
        private void SetConversionTable_Click(object sender, RoutedEventArgs e)
        {
            // isClick = true;

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

        private void showWorkProcess(string previous, string next, int command)
        {
            string result = previous + " -> " + Convert.ToString(command) + " -> " + next;
            workProcess.ItemsSource = result;
        }

        #endregion //EventsHandlers

        private void setChainOfCommand_Click(object sender, RoutedEventArgs e)
        {
            string chain = chainOfcommand.Text;

            int state = 0;
            int command = 0;
            int finalState = finiteMachine.Count - 1;

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                command = chain[numberOfChain];

                if (command < finiteMachine[state].Count)
                {
                    if (convertToNumber(Convert.ToChar(finiteMachine[state][command])) == finalState)
                    {
                        workProcess.ItemsSource = "автомат достиг финального состояния!";
                        workProcess.ItemsSource = "Работа окончена.";
                    }
                    else if (finiteMachine[state][command] != "")
                    {
                        showWorkProcess(visualState(state), finiteMachine[state][command], command);
                        state = convertToNumber(Convert.ToChar(finiteMachine[state][command]));
                    }
                    
                }
                else
                {
                    workProcess.ItemsSource = "Команда отсутствует "
                }


                for (int findNextState = 0; findNextState < finiteMachine[state].Count; findNextState++)
                {
                    
                }
            }
        }
    }
}
