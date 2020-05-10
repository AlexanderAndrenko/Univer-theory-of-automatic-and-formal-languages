using System;
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
        string[] finalStateArr;//массив финальных состояний
        private List<List<string>> Terminal;
        private List<List<string>> Nonterminal;

        #endregion //Daclaration

        #region Constructor
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet, int quantityFinState)// Реализация для лабораторной 2 конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            ObservableCollection<ObservableCollection<string>> finiteStateMachine = ptr;
            quantityFinalState = quantityFinState;
            deterministic = true;

            finalStateArr = new string[quantityFinalState];

            for (int i = 0; i < finalStateArr.Length; i++)
            {
                finalStateArr[i] = convertNumberToString(CountQuantityNonterminal() - 1 - i);
            }

            for (int i = 0; i < nonterminalAlphabet; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());

                for (int j = 0; j < terminalAlphabet; j++)
                {
                    finiteStateMachine[i].Add((convertNumberToString(i)).ToString());
                }
            }

            Terminal = new List<List<string>>();//Инициализация кодировочной таблицы для терминалов
            Nonterminal = new List<List<string>>();//Инициализация кодировочной таблицы для нетерминалов

            /*Конечный автомат должен содержать хотя бы один нетерминал*/
            Nonterminal.Add(new List<string>());
            Nonterminal[0].Add("A");
        }
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet)// Реализация для лабораторной 3 если не определено, что он детерминированный, то вызывается этот конструктор. конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
    {
            finiteStateMachine = ptr;
            quantityFinalState = 1;
            deterministic = false;

            finalStateArr = new string[quantityFinalState];

            for (int i = 0; i < finalStateArr.Length; i++)
            {
                finalStateArr[i] = convertNumberToString(CountQuantityNonterminal() - 1 - i);
            }

            for (int i = 0; i < terminalAlphabet; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());//Добавляем терминалы

                for (int j = 0; j < nonterminalAlphabet; j++)
                {
                    finiteStateMachine[i].Add("-");//добавляем нетерминалы
                }
            }

            Terminal = new List<List<string>>();//Инициализация кодировочной таблицы для терминалов
            Nonterminal = new List<List<string>>();//Инициализация кодировочной таблицы для нетерминалов

            /*Конечный автомат должен содержать хотя бы один нетерминал*/
            Nonterminal.Add(new List<string>());
            Nonterminal[0].Add("A");
        }
        public FiniteStateMachine(string rules)//Реализация для лабораторной 4. Конструктор класса, задающийся только перечнем правил перехода.
        {
            Nonterminal = new List<List<string>>();//Инициализируем список нетерминалов
            Terminal = new List<List<string>>();//Инициализируем список терминалов
            PrepareRulesString(rules);
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
        
        public bool ParseWord(string parseWord, ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой.
        {
            int state = 0;
            int command;
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

        /*public bool CheckRule(string rule)
        {
            for (int index = 0; index < rule.Length; index++)
            {
                if (index != 1)
                {
                    if (!(convertNumberToString(Convert.ToString(rule[index]), finiteMachine[0].Count, false)))
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
        }*/

        #endregion //Public method

        #region Private method
        
        private void PrepareRulesString(string rules)
        {
            

            while (rules != null)
            {
                if (rules[0] != Convert.ToChar(" "))
                {
                    int indexOfGap = rules.IndexOf(" ");

                    if (AddRule(rules.Substring(0, indexOfGap + 1)))
                    {
                        rules.Substring(0, indexOfGap + 1);
                    }
                    
                    
                }
            }
        }

        private bool AddRule(string rule)
        {
            if (rule.Length < 3)
            {
                if (!NonterminalIsExist(Convert.ToString(rule[0])))
                {
                    AddNonterminal(Convert.ToString(rule[0]));
                }

                if (!TerminalIsExist(Convert.ToString(rule[1])))
                {
                    AddTerminal(Convert.ToString(rule[1]));
                }

                if (!NonterminalIsExist(Convert.ToString(rule[2])))
                {
                    AddNonterminal(Convert.ToString(rule[2]));
                }
                
                return true;
            }
            else
            {
                return false;
            }
        }
        private void AddNonterminal(string nonterminal)//Добавляение нового терминала в список терминалов List<List<string> Nonterminal
        {
            if (Nonterminal.Count != 0)
            {
                Nonterminal.Add(new List<string>());//Инициализируем новый нетерминал
                Nonterminal[Nonterminal.Count - 1].Add(Convert.ToString(Nonterminal.Count - 1));//Задаем минимальную кодировку для нетерминалов для внутреннего представления КА (так как добавляется в конец, берётся длинна списка нетерминалов минус единица)
                Nonterminal[Nonterminal.Count - 1].Add(nonterminal);//Заносим значение вводимого нетерминала, для дальнейшей раскодировки
            }
            else
            {
                Nonterminal.Add(new List<string>());//Инициализируем новый нетерминал
                Nonterminal[0].Add("0");//Задаем минимальную кодировку для нетерминалов для внутреннего представления КА (начинается с нуля)
                Nonterminal[0].Add(nonterminal);//Заносим значение вводимого нетерминала, для дальнейшей раскодировки
            }            
        }
        private void AddTerminal(string terminal)//Добавляение нового терминала в список терминалов List<List<string> Terminal
        {
            if (Terminal.Count != 0)
            {
                Terminal.Add(new List<string>());//Инициализируем новый нетерминал
                Terminal[Terminal.Count - 1].Add(Convert.ToString(Terminal.Count - 1));//Задаем минимальную кодировку для нетерминалов для внутреннего представления КА (так как добавляется в конец, берётся длинна списка нетерминалов минус единица)
                Terminal[Terminal.Count - 1].Add(terminal);//Заносим значение вводимого нетерминала, для дальнейшей раскодировки
            }
            else
            {
                Terminal.Add(new List<string>());//Инициализируем новый нетерминал
                Terminal[0].Add("0");//Задаем минимальную кодировку для нетерминалов для внутреннего представления КА (начинается с нуля)
                Terminal[0].Add(terminal);//Заносим значение вводимого нетерминала, для дальнейшей раскодировки
            }
        }
        private bool NonterminalIsExist(string nonterminal)//Проверка, существует ли уже данный нетерминал
        {
            for (int index = 0; index < Nonterminal.Count; index++)
            {
                if (Nonterminal[index][1] == nonterminal)
                {
                    return true;
                }
            }

            return false;
        }
        private bool TerminalIsExist(string terminal)//Проверка существует ли данный терминал
        {
            for (int index = 0; index < Terminal.Count; index++)
            {
                if (Nonterminal[index][1] == terminal)
                {
                    return true;
                }
            }

            return false;
        }

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

        #endregion //Private method
    }
}
