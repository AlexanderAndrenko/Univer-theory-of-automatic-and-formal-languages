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
        private int quantityFinalState;

        #endregion //Daclaration

        #region Constructor
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet, int quantityFinState)// конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            ObservableCollection<ObservableCollection<string>> finiteStateMachine = ptr;
            quantityFinalState = quantityFinState;

            for (int i = 0; i < nonterminalAlphabet; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());

                for (int j = 0; j < terminalAlphabet; j++)
                {
                    finiteStateMachine[i].Add((convertNumberToString(i)).ToString());
                }
            }
        }
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet)// конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            finiteStateMachine = ptr;
            quantityFinalState = 1;

            for (int i = 0; i < terminalAlphabet; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());//Добавляем терминалы

                for (int j = 0; j < nonterminalAlphabet; j++)
                {
                    finiteStateMachine[i].Add((convertNumberToString(i)).ToString());//добавляем нетерминалы
                }
            }
        }

        #endregion //Constructor

        #region Public method

        public int CountQuantityNonterminal()
        {
            int quantityOfNonterminal = finiteStateMachine[0].Count;

            return quantityOfNonterminal;
        }

        public int CountQuantityTerminal()
        {
            int quantityOfTerminal = finiteStateMachine.Count;

            return quantityOfTerminal;
        }
        
        /*Терминальные символы - это по сути тоже самое, что и команды из прошлой лабораторной работы.
         * Поэтому имитация работы конечного автомата по сути и есть парсер цепочки терминальных символов.
         * Только необходимо вместо цифр задавать какие-то буквы.
         * Осталось придумать как трансформировать прошлый код под новую задачу*/

        public bool ParseWord(string parseWord, ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой. Парсить начинаем с конца
        {
            int state = 0;
            int command = 0;
            int finalState = CountQuantityNonterminal() - 1;

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                command = Convert.ToInt32(chain[numberOfChain]) - 48;

                if ((finiteStateMachine[command][state] != "") && convertStringToNumber(finiteStateMachine[command][state]) < finiteStateMachine[command].Count)//Проверка что след. состояние присутствует в алфавите
                {
                    if (checkFinalState(finiteStateMachine[command][state]))
                    {
                        if (numberOfChain + 1 == chain.Length)
                        {
                            lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), finiteStateMachine[command][state], command));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            lineOfWorkProcess.Add("Работа окончена.");
                        }
                        else
                        {
                            lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), finiteStateMachine[command][state], command));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            state = convertStringToNumber(finiteStateMachine[command][state]);
                        }
                    }
                    else if (finiteStateMachine[command][state] != "")
                    {
                        lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), finiteStateMachine[command][state], command));
                        state = convertStringToNumber(finiteStateMachine[command][state]);

                        if (numberOfChain + 1 == chain.Length && state != finalState)
                        {
                            lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                            lineOfWorkProcess.Add("Не достигнуто финальное состояние");
                        }
                    }
                }
                else if (finiteStateMachine[command][state] == "")
                {
                    lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                    lineOfWorkProcess.Add("Следующее состояние при команде " + command + " не определено.");
                    break;
                }
                else
                {
                    lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                    lineOfWorkProcess.Add("Состояние отсутствует в алфавите.");
                    break;
                }
            }

            return true;
        }


        public void ConvertRuleTransition(string rule)
        {
            int preNonterminal = convertStringToNumber(Convert.ToString(rule[0]));
            int terminal;
            if (!(rule[1] == 'E'))
            {
                terminal = convertStringToNumber(Convert.ToString(rule[1])) - 32;
            }
            else
            {
                terminal = CountQuantityTerminal() - 1;
            }
            

            if (rule.Length == 2)
            {
                finiteStateMachine[terminal][preNonterminal] = convertNumberToString(CountQuantityNonterminal() - 1);
            }
            else
            {
                finiteStateMachine[terminal][preNonterminal] = rule.Substring(2);
                //finiteStateMachine[terminal].Insert(preNonterminal, rule.Substring(2));
            }            
        }

        #endregion //Public method

        #region Private method

        #region For future maybe
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

        #endregion //For future maybe

        private string showWorkProcess(string previous, string next, int command)
        {
            string result = previous + " -> " + Convert.ToString(command) + " -> " + next;

            return result;
        }

        private bool checkFinalState(string letterOfState)
        {
            string[] finalStateArr = new string[quantityFinalState];
            int numberOfLetter = CountQuantityNonterminal() - 1 - quantityFinalState;

            for (int i = 0; i < finalStateArr.Length; i++)
            {
                finalStateArr[i] = convertNumberToString(CountQuantityNonterminal() - 1 - i);
            }

            for (int i = 0; i < finalStateArr.Length; i++)
            {
                if (finalStateArr[i] == letterOfState)
                {
                    return true;
                }
            }

            return false;
        }

        private string convertNumberToString(int valueOfCommand)
        {
            string result = Convert.ToString(Convert.ToChar(valueOfCommand + 65));

            return result;
        }

        private int convertStringToNumber(string letter)
        {
            int result = Convert.ToInt32(Convert.ToChar(letter)) - 65; ;

            return result;
        }

        #endregion //Public method
    }
}
