using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace finite_state_machine
{
    public class FiniteStateMachine
    {
        #region Daclaration
        /* данные в элементе коллекции хранятся в виде <A>::='a'<A> 
         * где 'a' терминальный символ, <A> -нетерминал, который следует за терминалом. 
         * Например B -> aA, тогда в столбец В на пересечении со строкой "а" (1) пишем 'a'::=<A>. Так как автомат недерминированный, то определяем два нетерминала <A><B>
         * Пользователь может определить любой терминал*/
        public ObservableCollection<ObservableCollection<string>> finiteStateMachine;//Таблица переходов конечного автомата 
        private Stack<string> store;
        public string finalState;

        #endregion //Daclaration

        #region Constructor
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int numberOfState, int numberOfCommand)// конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            ObservableCollection<ObservableCollection<string>> finiteStateMachine = ptr;
            finalState = visualState(numberOfState);

            for (int i = 0; i < numberOfState; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());

                for (int j = 0; j < numberOfCommand; j++)
                {
                    finiteStateMachine[i].Add((visualState(i)).ToString());
                }
            }
        }

        #endregion //Constructor

        #region Public method

        public int CountQuantityState()
        {
            int quantityOfState = finiteStateMachine[0].Count;

            return quantityOfState;
        }

        //public int CountTerminalSymbol

        /*Терминальные символы - это по сути тоже самое, что и команды из прошлой лабораторной работы.
         * Поэтому имитация работы конечного автомата по сути и есть парсер цепочки терминальных символов.
         * Только необходимо вместо цифр задавать какие-то буквы.
         * Осталось придумать как трансформировать прошлый код под новую задачу*/

        public bool ParseWord(string parseWord)//Парсинг строки с праволинейной грамматикой. Парсить начинаем с конца
        {
            bool result = false;

            for (int i = 0; i < finiteStateMachine.Count; i++)
            {
                for (int j = 0; j < length; j++)
                {

                }
            }

            while(parseWord.Length > 0)
            {
               
            }

            return result;
        }

        #endregion //Public method

        #region Private method

        private void setStackElement(string parseWord)
        {


        }

        private string getStackElement()
        {

            return "";
            
        }

        private string getTerminalSymbol(string rule)//Правило хранится в формате <A>::='a'<AB>
        {
            rule = rule.Remove(0,7);
            int index = rule.IndexOf("'",0,rule.Length);
            rule = rule.Remove(index);

            return rule;
        }

        private string getFirstNontemiinalSymbol(string rule)
        {
            rule = Convert.ToString(rule[1]);

            return rule;
        }

        private string getNextNonterminalSymbols(string rule)
        {
            rule = rule.Remove(0, 7);
            int index = rule.IndexOf("'", 0, rule.Length);
            rule = rule.Remove(0, index + 1);
            rule = rule.Remove(rule.Length - 1);

            return rule;
        }


        private string visualState(int valueOfCommand)
        {
            string result = Convert.ToString(Convert.ToChar(valueOfCommand + 97));

            return result;
        }

        private int convertToNumber(string letter)
        {
            int result = Convert.ToInt32(Convert.ToChar(letter)) - 97; ;

            return result;
        }

        #endregion //Public method
    }
}
