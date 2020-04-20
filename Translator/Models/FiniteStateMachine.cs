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
    /*Класс объект которого отражает одно состояние детерминированного конечного автомата */
    public class FiniteStateMachine
    {
        public string[] conversion;//массив отобажающий таблицу перехода для конкретного состояния
        public DataGridTextColumn nameOfColumn;//объект для создания колонки
        public Binding dataColumn;//объект для привязки данных


        /*Конструктор класса*/
        /*public FiniteStateMachine(int command, string name)
        {
            this.conversion = new string[command];
            for (int i = 0; i < this.conversion.Length; i++)
            {
                this.conversion[i] = Convert.ToString(i + 1);
            }
            this.nameOfColumn = new DataGridTextColumn();
            this.dataColumn = new Binding(Convert.ToString(conversion));//Привязка массива к объекту типа Binding 
            this.nameOfColumn.Binding = dataColumn;//Привязка данных к объекту колонки
            this.nameOfColumn.Header = Convert.ToString(name);//Привязка названия колонки

        }*/

        public FiniteStateMachine(int command, string name)
        {
            this.conversion = new string[command];
            for (int i = 0; i < this.conversion.Length; i++)
            {
                this.conversion[i] = Convert.ToString(i + 1);
            }
            this.nameOfColumn = new DataGridTextColumn();
            this.dataColumn = new Binding(Convert.ToString(conversion));//Привязка массива к объекту типа Binding 
            this.nameOfColumn.Binding = dataColumn;//Привязка данных к объекту колонки
            this.nameOfColumn.Header = Convert.ToString(name);//Привязка названия колонки

        }

        /*?????*/
        public void setConversion(int index, char nextState)
        {
            this.conversion[index] = Convert.ToString(nextState);
        }

        public int convertSymbolToInt(string symbol)
        {
            int result = Convert.ToInt32(symbol) - 97;

            return result;
        }
    }
}
