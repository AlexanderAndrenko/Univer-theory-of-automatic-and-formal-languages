﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace finite_state_machine
{
    /*метод checkFinalstate необходимо инициализацию финальных состояний вынести отдельный метод, массив типа string хранящий финальные состояния сделать локальным для всего класса.
     * Необходимо реализовать возможность задавать финальные состояния не только с конца, но и в любом месте множества всех состояний
     * По умолчанию при создании КА, скрытно от пользователя, в конец множества состояний добавляется ещё одно состояние (финальное) "t"
     * Возможно имеет смысл продублировать таблицу перехода и массив финальных состояний, чтобы не потерять изначальную структуру, а после окончания работы алгоритма перезаписать изначальные на новые
     * Алгоритм проверки входной цепочки, уже написан и по сути должен работать и для НКА и для ДКА. (Надо тестить) 
     * Необходимо реализовать функция конвертации НКА в ДКА.*/
    public class FiniteStateMachine
    {
        #region Daclaration
        public ObservableCollection<ObservableCollection<string>> finiteStateMachine;//Таблица переходов конечного автомата 
        private Stack<string> store; //Магазин для НКА
        private int quantityFinalState;//Количество финальных состояний
        private bool deterministic;//Переменная отображающая НКА или ДКА создаваемый автомат

        #endregion //Daclaration

        #region Constructor
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet, int quantityFinState)// конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            ObservableCollection<ObservableCollection<string>> finiteStateMachine = ptr;
            quantityFinalState = quantityFinState;
            deterministic = true;

            for (int i = 0; i < nonterminalAlphabet; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());

                for (int j = 0; j < terminalAlphabet; j++)
                {
                    finiteStateMachine[i].Add((convertNumberToString(i)).ToString());
                }
            }
        }
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet)// если не определено, что он детерминированный, то вызывается этот конструктор. конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            finiteStateMachine = ptr;
            quantityFinalState = 1;
            deterministic = false;

            for (int i = 0; i < terminalAlphabet; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());//Добавляем терминалы

                for (int j = 0; j < nonterminalAlphabet; j++)
                {
                    finiteStateMachine[i].Add("-");//добавляем нетерминалы
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
        
        public bool ParseWord(string parseWord, ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой. Парсить начинаем с конца
        {
            int state = 0;
            int command = 0;
            int finalState = CountQuantityNonterminal() - 1;
            string potentialNonterminal = "A";
            bool popStack = false;
            store = new Stack<string>();
            store.Clear();

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                command = Convert.ToInt32(chain[numberOfChain]) - 97;

                if (!popStack)
                {
                    if (finiteStateMachine[command][state].Length > 1)
                    {
                        potentialNonterminal = Convert.ToString(finiteStateMachine[command][state].First());
                        setStackElement(convertNumberToString(state) ,finiteStateMachine[command][state].Substring(1), numberOfChain);
                        lineOfWorkProcess.Add("Добавление в стэк возможного перехода: " + potentialNonterminal + finiteStateMachine[command][state].Substring(1) + numberOfChain);
                    }
                    else if (state == CountQuantityNonterminal() - 1)
                    {
                        potentialNonterminal = convertNumberToString(state);
                    }
                    else
                    {
                        potentialNonterminal = finiteStateMachine[command][state];
                    }
                }
                else
                {
                    popStack = false;
                }
                

                if ((potentialNonterminal != "-") && convertStringToNumber(potentialNonterminal) < finiteStateMachine[command].Count)//Проверка что след. нетерминал присутствует в алфавите
                {
                    if (checkFinalState(potentialNonterminal))
                    {
                        if (numberOfChain + 1 == chain.Length)
                        {
                            lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), "t", command));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            lineOfWorkProcess.Add("Работа окончена.");

                            return true;
                        }
                        else if(deterministic)
                        {
                            lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), "t", command));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            state = convertStringToNumber(potentialNonterminal);

                            return true;
                        }
                        else
                        {
                            if (!stackIsEmpty())
                            {
                                string pack = getStackElement();
                                lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                                state = convertStringToNumber(Convert.ToString(pack[0]));
                                potentialNonterminal = Convert.ToString(pack[1]);
                                pack = pack.Substring(2);
                                numberOfChain = Convert.ToInt32(pack) - 1;
                                popStack = true;
                                continue;
                            }
                            else
                            {
                                lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                                lineOfWorkProcess.Add("Следующий нетерминал при терминале " + command + " не определен.");
                                break;
                            }
                        }
                    }
                    else //potentialNonterminal != ""
                    {
                        lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), potentialNonterminal, command));
                        state = convertStringToNumber(potentialNonterminal);

                        if (numberOfChain + 1 == chain.Length && state != finalState)
                        {
                            if (finiteStateMachine[CountQuantityTerminal() - 1][state] != "-")
                            {
                                lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(state), "t", CountQuantityTerminal() - 1));
                                lineOfWorkProcess.Add("Автомат достиг финального состояния!");

                                return true;
                            }
                            else if (!stackIsEmpty())
                            {
                                string pack = getStackElement();
                                lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                                state = convertStringToNumber(Convert.ToString(pack[0]));
                                potentialNonterminal = Convert.ToString(pack[1]);
                                pack = pack.Substring(2);
                                numberOfChain = Convert.ToInt32(pack) - 1;
                                popStack = true;
                                continue;
                            }
                            else
                            {
                                lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                                lineOfWorkProcess.Add("Не достигнуто финальное состояние");
                            }                            
                        }
                    }
                }
                else if (finiteStateMachine[command][state] == "-")
                {
                    if (!stackIsEmpty())
                    {
                        string pack = getStackElement();
                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                        state = convertStringToNumber(Convert.ToString(pack[0]));
                        potentialNonterminal = Convert.ToString(pack[1]);
                        pack = pack.Substring(2);
                        numberOfChain = Convert.ToInt32(pack) - 1;
                        popStack = true;
                        continue;
                    }
                    else
                    {
                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                        lineOfWorkProcess.Add("Следующий нетерминал при терминале " + command + " не определен.");
                        break;
                    }                    
                }
                else
                {
                    if (!stackIsEmpty())
                    {
                        string pack = getStackElement();
                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                        state = convertStringToNumber(Convert.ToString(pack[0]));
                        potentialNonterminal = Convert.ToString(pack[1]);
                        pack = pack.Substring(2);
                        numberOfChain = Convert.ToInt32(pack) - 1;
                        popStack = true;
                        continue;
                    }
                    else
                    {
                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                        lineOfWorkProcess.Add("Нетерминал отсутствует в алфавите.");
                        break;
                    }
                }
            }

            return false;
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

            if (finiteStateMachine[terminal][preNonterminal] == "-")
            {
                if (rule.Length == 2)
                {
                    finiteStateMachine[terminal][preNonterminal] = convertNumberToString(CountQuantityNonterminal() - 1);
                }
                else
                {
                    finiteStateMachine[terminal][preNonterminal] = rule.Substring(2, 1);
                }
            }
            else
            {
                if (rule.Length == 2)
                {
                    finiteStateMachine[terminal][preNonterminal] += convertNumberToString(CountQuantityNonterminal() - 1);
                }
                else
                {
                    finiteStateMachine[terminal][preNonterminal] += rule.Substring(2, 1);
                }
            }            
        }

        #endregion //Public method

        #region Private method
               
        private void setStackElement(string prenonterminal ,string nextNonterminal, int position)
        {
            string pack = prenonterminal;

            for (int index = 0; index < nextNonterminal.Length; index++)
            {
                pack += Convert.ToString(nextNonterminal[index]);
                pack += Convert.ToString(position);
                store.Push(pack);
            }
        }

        private string getStackElement()
        {
            if (store.Count != 0)
            {
                return store.Pop();
            }
            else
            {
                return null;
            }
                      
        }

        private bool stackIsEmpty()
        {
            if (store.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }      

        private string showWorkProcess(string previous, string next, int command)
        {
            string result = previous + " -> " + Convert.ToString(command) + " -> " + next;

            return result;
        }

        private bool checkFinalState(string letterOfState)
        {
            string[] finalStateArr = new string[quantityFinalState];

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
