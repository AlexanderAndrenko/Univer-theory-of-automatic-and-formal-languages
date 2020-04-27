﻿using System;
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
        
        public ObservableCollection<ObservableCollection<string>> finiteMachine {get;set;}
        public ObservableCollection<string> lineOfWorkProcess { get; set; }

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

        public void updateData()
        {
            dataGrid2D.DataContext = this;
        }

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
        }

        #region EventsHandlers

        /*Функция обработки нажатия на кнопку SetSymbolOfAlphabet*/
        private void SetSymbolOfAlphabet_Click(object sender, RoutedEventArgs e)
        {
            if (!isCreated)
            {
                createObjectsForTable();
            }

            isCreated = true;
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

        public string showWorkProcess(string previous, string next, int command)
        {
            string result = previous + " -> " + Convert.ToString(command) + " -> " + next;

            return result;
        }
        
        private void setChainOfCommand_Click(object sender, RoutedEventArgs e)
        {
            lineOfWorkProcess = new ObservableCollection<string>();
            workProcess.ItemsSource = lineOfWorkProcess;

            string chain = chainOfcommand.Text;

            int state = 0;
            int command = 0;
            int finalState = finiteMachine[state].Count - 1;

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                command = Convert.ToInt32(chain[numberOfChain]) - 48;

                if (convertToNumber(Convert.ToChar(finiteMachine[command][state])) < finiteMachine[command].Count)//Проверка что след. состояние присутствует в алфавите
                {
                    if (convertToNumber(Convert.ToChar(finiteMachine[command][state])) == finalState)
                    {
                        if (numberOfChain + 1 == chain.Length)
                        {
                            lineOfWorkProcess.Add(showWorkProcess(visualState(state), finiteMachine[command][state], command));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            lineOfWorkProcess.Add("Работа окончена.");
                        }
                        else
                        {
                            lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                            lineOfWorkProcess.Add("Цепочка команд не закончилась, но автомат достиг финального состояния!.");
                            break;
                        }
                    }
                    else if (finiteMachine[command][state] != "")
                    {
                        lineOfWorkProcess.Add(showWorkProcess(visualState(state), finiteMachine[command][state], command));
                        state = convertToNumber(Convert.ToChar(finiteMachine[command][state]));
                    }
                    else if (finiteMachine[command][state] == "")
                    {
                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                        lineOfWorkProcess.Add("Следующее состояние при команде " + command + " не определено.");
                        break;
                    }
                }                
                else
                {
                    lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                    lineOfWorkProcess.Add("Состояние отсутствует в алфавите.");
                    break;
                }
            }
        }
        #endregion //EventsHandlers
    }
}
