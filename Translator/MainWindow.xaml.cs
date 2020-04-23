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
    /// 
    
    public partial class MainWindow : Window
    {
        bool isCreated = false;//Переменная состояния - созданы ли объекты представляющие конечный автомат
        //bool isClick = false;
        int state = 0;//Отражает состояние для которого задаётся таблица перехода
        int command = 0;//Отражает комануд для которого задаётся таблица перехода

        List<FiniteStateMachine> textcol = new List<FiniteStateMachine>();
        GridView MyGV = new GridView();
        public MainWindow()
        {
            InitializeComponent();            
        }

        /*Функция обработки нажатия на кнопку SetSymbolOfAlphabet*/
        private void SetSymbolOfAlphabet_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                createObjectsForTable();
            }

            isCreated = true;
        }

        /*Функция обработки нажатия на кнопку SetConversionTable*/
        private void SetConversionTable_Click(object sender, RoutedEventArgs e)
        {
            // isClick = true;

        }

        /*Функция создаёт объекты отображающие внутренее представление детерминированого конечного автомата (один объект - одно состояние*/
        public void createObjectsForTable()
        {
            int numberOfState = int.Parse(SymbolOfState.Text);//Определяет количество состояний
            int numberOfCommand = int.Parse(TerminalSymbol.Text);//Определяет количество возможных команд

            //инициализация объектов (состояний)
            for (int i = 0; i < numberOfState; i++)
           {
                textcol.Add(new FiniteStateMachine(numberOfCommand, visualState(i)));
                               
                MyGV.Columns.Add(textcol[i].nameOfColumn);//Создание колонки для данного состояния с именем "nameOfColumn"
                
           }

            for (int i = 0; i < numberOfState; i++)
            {
                MyLV.ItemsSource = textcol[i].listState;
            }
            

            /*визуализация сотояния и команды для строки заполнения таблицы переходов */
            Command.Text = Convert.ToString(command + 1);
            State.Text = visualState(state);
        }

        /*функция конвертации номера состояния в строку
         (проверить надо ли вообще?)
         */
        public string visualState(int valueOfCommand)
        {
            string result = Convert.ToString(Convert.ToChar(valueOfCommand + 97));

            return result;
        }

        private void ConversionTableDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            int index = ConversionTableDataGrid.SelectedIndex;
            check.Text = Convert.ToString(index);
        }
    }
}




//while (true)
//{
//    if (isClick)
//    {
//        isClick = false;

//        command++;
//        Command.Text = Convert.ToString(command + 1);
//        if (command > textcol[state].conversion.Length)
//        {
//            state++;
//            State.Text = visualCommand(state);
//        }                    

//        textcol[state].conversion[command] = NextState.Text;
//    }
//}