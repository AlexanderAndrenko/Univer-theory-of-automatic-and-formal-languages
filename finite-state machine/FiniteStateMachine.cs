using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

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
        public string addedRules = "";

        private Stack<string> store; //Магазин для НКА
        private int quantityFinalState;//Количество финальных состояний
        private bool deterministic;//Переменная отображающая НКА или ДКА создаваемый автомат
        private List<string> finalStateArr;//массив финальных состояний
        private int _startNonterminal;
        private List<List<string>> Terminal;
        private List<List<string>> Nonterminal;


        #endregion //Daclaration

        #region Constructor
        public FiniteStateMachine(ObservableCollection<ObservableCollection<string>> ptr, int nonterminalAlphabet, int terminalAlphabet, int quantityFinState)// Реализация для лабораторной 2 конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            ObservableCollection<ObservableCollection<string>> finiteStateMachine = ptr;
            quantityFinalState = quantityFinState;
            deterministic = true;

            finalStateArr = new List<string>();

            for (int i = 0; i < quantityFinalState; i++)
            {
                finalStateArr.Add(convertNumberToString(CountQuantityNonterminal() - 1 - i));
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

            finalStateArr = new List<string>();

            for (int i = 0; i < quantityFinalState; i++)
            {
                finalStateArr.Add(convertNumberToString(CountQuantityNonterminal() - 1 - i));
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
            finiteStateMachine = new ObservableCollection<ObservableCollection<string>>();
            finiteStateMachine.Clear();
            Terminal = new List<List<string>>();//Инициализируем список терминалов
            Nonterminal = new List<List<string>>();//Инициализация списка нетерминалов
                        
            PrepareRulesString(rules);
            CheckForDeterministic();
            _startNonterminal = Convert.ToInt32(Nonterminal[1][0]);//По умолчанию берётся первый нетерминал
        }

        #endregion //Constructor

        #region Public method

        public int CountQuantityNonterminal()
        {
            //int quantityOfNonterminal = finiteStateMachine[0].Count; //ПОДЛЕЖИТ УДАЛЕНИЮ
            int quantityOfNonterminal = Nonterminal.Count;

            return quantityOfNonterminal;
        }
        public int CountQuantityTerminal()
        {
            //int quantityOfTerminal = finiteStateMachine.Count; //ПОДЛЕЖИТ УДАЛЕНИЮ
            int quantityOfTerminal = Terminal.Count;

            return quantityOfTerminal;
        }

        //public bool ParseWord(string parseWord, ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой.
        //{
        //    int nonterminal = _startNonterminal;//Стартовый нетерминал в котором находится КА или находится в данный момент
        //    int terminal = indexOfTerminalEncode(Convert.ToString(chain[0]));//Первый считанный терминал из цепочки
        //    string potentialNonterminal = finiteStateMachine[terminal][nonterminal];//Нетерминал в который есть потенциальный переход 
        //    bool popStack = false; //Условная переменная, отображающая был ли poр из стэка в прошлой итерации
        //    store = new Stack<string>();//Инициализируем новый стэк (магазин) КА
        //    store.Clear();//Очищаем стэк от мусора

        //    for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
        //    {
        //        terminal = indexOfTerminalEncode(Convert.ToString(chain[numberOfChain]));//последний считанный терминал из цепочки

        //        if (!popStack)//Если был возврат к состояния из стэка, то заново вносить возможные нетерминалы в стэк не надо, они уже были внесены в первый раз. Иначе будет зацикливание.
        //        {
        //            if (finiteStateMachine[terminal][nonterminal].Length > 1)
        //            {
        //                potentialNonterminal = Convert.ToString(finiteStateMachine[terminal][nonterminal].First());
        //                setStackElement(GetNameNonterminal(nonterminal) ,finiteStateMachine[terminal][nonterminal].Substring(1), numberOfChain);
        //                lineOfWorkProcess.Add("Добавление в стэк возможного перехода: " + potentialNonterminal + finiteStateMachine[terminal][nonterminal].Substring(1) + numberOfChain);
        //            }
        //            else if (checkFinalState(Convert.ToString(nonterminal)))//Проверка является ли следующий нетерминал финальным
        //            {     
        //                potentialNonterminal = GetNameNonterminal(nonterminal);
        //            }
        //            else
        //            {
        //                potentialNonterminal = finiteStateMachine[terminal][nonterminal];
        //            }
        //        }
        //        else
        //        {
        //            popStack = false;
        //        }


        //        if ((potentialNonterminal != "-") && indexOfNonterminalEncode(potentialNonterminal) < finiteStateMachine[terminal].Count)//Проверка что след. нетерминал присутствует в алфавите
        //        {
        //            if (checkFinalState(potentialNonterminal))
        //            {
        //                if (numberOfChain + 1 == chain.Length)
        //                {
        //                    lineOfWorkProcess.Add(showWorkProcess(GetNameNonterminal(nonterminal), "t", GetNameTerminal(terminal)));
        //                    lineOfWorkProcess.Add("Автомат достиг финального состояния!");
        //                    lineOfWorkProcess.Add("Работа окончена.");

        //                    return true;
        //                }
        //                else if(deterministic)
        //                {
        //                    lineOfWorkProcess.Add(showWorkProcess(GetNameNonterminal(nonterminal), "t", GetNameTerminal(terminal)));
        //                    lineOfWorkProcess.Add("Автомат достиг финального состояния!");
        //                    nonterminal = indexOfNonterminalEncode(potentialNonterminal);

        //                    return true;
        //                }
        //                else
        //                {
        //                    if (!stackIsEmpty())
        //                    {
        //                        #region StackOutput
        //                        string pack = getStackElement();
        //                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
        //                        nonterminal = indexOfNonterminalEncode(Convert.ToString(pack[0]));
        //                        potentialNonterminal = Convert.ToString(pack[1]);
        //                        pack = pack.Substring(2);
        //                        numberOfChain = Convert.ToInt32(pack) - 1;
        //                        popStack = true;
        //                        continue;
        //                        #endregion //StackOutput
        //                    }
        //                    else
        //                    {
        //                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
        //                        lineOfWorkProcess.Add("Следующий нетерминал при терминале " + terminal + " не определен.");
        //                        break;
        //                    }
        //                }
        //            }
        //            else //potentialNonterminal != ""
        //            {
        //                lineOfWorkProcess.Add(showWorkProcess(GetNameNonterminal(nonterminal), potentialNonterminal, GetNameTerminal(terminal)));
        //                nonterminal = indexOfNonterminalEncode(potentialNonterminal);


        //                if (numberOfChain + 1 == chain.Length && checkFinalState(GetNameNonterminal(nonterminal)))
        //                {
        //                    if (finiteStateMachine[CountQuantityTerminal() - 1][nonterminal] != "-")
        //                    {
        //                        lineOfWorkProcess.Add(showWorkProcess(GetNameNonterminal(nonterminal), potentialNonterminal, GetNameTerminal(terminal)));
        //                        lineOfWorkProcess.Add("Автомат достиг финального состояния!");

        //                        return true;
        //                    }
        //                    else if (!stackIsEmpty())
        //                    {
        //                        #region StackOutput
        //                        string pack = getStackElement();
        //                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
        //                        nonterminal = indexOfNonterminalEncode(Convert.ToString(pack[0]));
        //                        potentialNonterminal = Convert.ToString(pack[1]);
        //                        pack = pack.Substring(2);
        //                        numberOfChain = Convert.ToInt32(pack) - 1;
        //                        popStack = true;
        //                        continue;
        //                        #endregion //StackOutput
        //                    }
        //                    else
        //                    {
        //                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
        //                        lineOfWorkProcess.Add("Не достигнуто финальное состояние");
        //                    }                            
        //                }
        //            }
        //        }
        //        else if (finiteStateMachine[terminal][nonterminal] == "-")
        //        {
        //            if (!stackIsEmpty())
        //            {
        //                #region StackOutput
        //                string pack = getStackElement();
        //                lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
        //                nonterminal = indexOfNonterminalEncode(Convert.ToString(pack[0]));
        //                potentialNonterminal = Convert.ToString(pack[1]);
        //                pack = pack.Substring(2);
        //                numberOfChain = Convert.ToInt32(pack) - 1;
        //                popStack = true;
        //                continue;
        //                #endregion //StackOutput
        //            }
        //            else
        //            {
        //                lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
        //                lineOfWorkProcess.Add("Следующий нетерминал при терминале " + terminal + " не определен.");
        //                break;
        //            }                    
        //        }
        //        else
        //        {
        //            if (!stackIsEmpty())
        //            {
        //                #region StackOutput
        //                string pack = getStackElement();
        //                lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
        //                nonterminal = indexOfNonterminalEncode(Convert.ToString(pack[0]));
        //                potentialNonterminal = Convert.ToString(pack[1]);
        //                pack = pack.Substring(2);
        //                numberOfChain = Convert.ToInt32(pack) - 1;
        //                popStack = true;
        //                continue;
        //                #endregion //StackOutput
        //            }
        //            else
        //            {
        //                lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
        //                lineOfWorkProcess.Add("Нетерминал отсутствует в алфавите.");
        //                break;
        //            }
        //        }
        //    }

        //    return false;
        //}

        public bool ParseWord(string parseWord, ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой.
        {
            int nonterminal = _startNonterminal;//Стартовый нетерминал в котором находится КА или находится в данный момент
            int terminal = indexOfTerminalEncode(Convert.ToString(chain[0]));//Первый считанный терминал из цепочки
            string potentialNonterminal = finiteStateMachine[terminal][nonterminal];//Нетерминал в который есть потенциальный переход 
            bool popStack = false; //Условная переменная, отображающая был ли poр из стэка в прошлой итерации
            store = new Stack<string>();//Инициализируем новый стэк (магазин) КА
            store.Clear();//Очищаем стэк от мусора

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                terminal = indexOfTerminalEncode(Convert.ToString(chain[numberOfChain]));//последний считанный терминал из цепочки

                if (!TerminalIsExist(GetNameTerminal(terminal)))//Проверка существует ли терминал
                {
                    return false;
                }

                if (!popStack && !deterministic)//Если был возврат к состояния из стэка, то заново вносить возможные нетерминалы в стэк не надо, они уже были внесены в первый раз. Иначе будет зацикливание.
                {
                    if (finiteStateMachine[terminal][nonterminal].Length > 1)
                    {
                        potentialNonterminal = Convert.ToString(finiteStateMachine[terminal][nonterminal].First());
                        setStackElement(GetNameNonterminal(nonterminal), finiteStateMachine[terminal][nonterminal].Substring(1), numberOfChain);
                        lineOfWorkProcess.Add("Добавление в стэк возможного перехода: " + GetNameNonterminal(nonterminal) + finiteStateMachine[terminal][nonterminal].Substring(1) + numberOfChain);
                    }
                    else
                    {
                        potentialNonterminal = finiteStateMachine[terminal][nonterminal];
                    }
                }
                else
                {
                    popStack = false;
                }


                if (potentialNonterminal != "-")//Проверка существует ли потенциальный нетерминал для перехода
                {
                    lineOfWorkProcess.Add(showWorkProcess(GetNameNonterminal(nonterminal), potentialNonterminal, GetNameTerminal(terminal)));
                    nonterminal = indexOfNonterminalEncode(potentialNonterminal);

                    if (numberOfChain + 1 == chain.Length)
                    {
                        if (checkFinalState(GetNameNonterminal(nonterminal)) || checkFinalState(finiteStateMachine[indexOfTerminalEncode("E")][nonterminal]))
                        {
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            return true;
                        }
                        else if (!stackIsEmpty())
                        {
                            #region StackOutput
                            string pack = getStackElement();
                            lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                            nonterminal = indexOfNonterminalEncode(Convert.ToString(pack[0]));
                            potentialNonterminal = Convert.ToString(pack[1]);
                            pack = pack.Substring(2);
                            numberOfChain = Convert.ToInt32(pack) - 1;
                            popStack = true;
                            continue;
                            #endregion //StackOutput
                        }
                        else
                        {
                            lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                            lineOfWorkProcess.Add("Автомат не достиг финального состояния!");
                        }

                    }
                }
                else if (potentialNonterminal == "-")
                {
                    if (!stackIsEmpty())
                    {
                        #region StackOutput
                        string pack = getStackElement();
                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                        nonterminal = indexOfNonterminalEncode(Convert.ToString(pack[0]));
                        potentialNonterminal = Convert.ToString(pack[1]);
                        pack = pack.Substring(2);
                        numberOfChain = Convert.ToInt32(pack) - 1;
                        popStack = true;
                        continue;
                        #endregion //StackOutput
                    }
                    else
                    {
                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                        lineOfWorkProcess.Add("Следующий нетерминал при терминале " + GetNameTerminal(terminal) + " не определен.");
                        break;
                    }
                }
            }

            return false;
        }
        public void ConvertRuleTransition(string rule)
        {
            int preNonterminal = indexOfNonterminalEncode(Convert.ToString(rule[0]));
            int terminal = indexOfTerminalEncode(Convert.ToString(rule[1]));

            if (finiteStateMachine[terminal][preNonterminal] == "-")
            {
                if (rule.Length == 2)
                {
                    finiteStateMachine[terminal][preNonterminal] = finalStateArr[0];
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
                    finiteStateMachine[terminal][preNonterminal] += finalStateArr[0];
                }
                else
                {
                    finiteStateMachine[terminal][preNonterminal] += rule.Substring(2, 1);
                }
            }
        }
        public void PrepareRulesString(string rules)//Обработка строки правил перехода подкачаенных из файла
        {
            while (rules != null)
            {
                if (rules[0] != Convert.ToChar(" "))//Если первый символ не пробел
                {
                    int indexOfGap = rules.IndexOf(" ");//Ищем первый проблем после символов

                    if (indexOfGap >= 0)
                    {
                        if (AddRule(rules.Substring(0, indexOfGap)))//Добавляем правило перехода, если добавилось метод вернёт true, иначе false
                        {
                            addedRules += (rules.Substring(0, indexOfGap) + " ");//Добавляем правило, которое прошло конвертацию во внутреннее представление для дальнейшего отображения для пользователя
                        }

                        rules = rules.Substring(indexOfGap);//Вырезаем правило, которое уже прошло обработку                    
                    }
                    else
                    {
                        if (AddRule(rules))
                        {
                            addedRules += (rules.Substring(0));
                        }
                        rules = null;//Вырезаем правило, которое уже прошло обработку                    
                    }
                    
                }
                else
                {
                    for (int indexOfLastGap = 0; indexOfLastGap < rules.Length; indexOfLastGap++)//Поиск первого символа не пробела
                    {
                        if (rules[indexOfLastGap] != Convert.ToChar(" "))
                        {
                            rules = rules.Substring(indexOfLastGap);//Вырезаем символы пробелов
                            break;//Выход из цикла поиска первого символа не пробела
                        }
                    }                    
                }
            }
        }
        public bool SetStartNonterminal(string startNonterminal)
        {
            if (NonterminalIsExist(startNonterminal))
            {
                _startNonterminal = indexOfNonterminalEncode(startNonterminal);

                return true;
            }

            return false;
        }
        public string GetNameNonterminal(int nonterminal)//Параметр - это код нетерминала в минимальной кодировке
        {
            return Nonterminal[nonterminal][1];
        }
        public string GetNameTerminal(int terminal)//Параметр - это код терминала в минимальной кодировке
        {
            return Terminal[terminal][1];
        }
        public void SetFinalState(string FinalState)
        {
            if (NonterminalIsExist(FinalState) && !checkFinalState(FinalState))
            {
                finalStateArr.Add(FinalState);
            }
        }

        #endregion //Public method

        #region Private method
        private bool AddRule(string rule)
        {
            if (rule.Length < 4 && rule.Length > 1)
            {
                if (!TerminalIsExist(Convert.ToString(rule[1])))
                {
                    AddTerminal(Convert.ToString(rule[1]));
                }

                if (!NonterminalIsExist(Convert.ToString(rule[0])))
                {
                    AddNonterminal(Convert.ToString(rule[0]));
                }                

                if (rule.Length != 2 && !NonterminalIsExist(Convert.ToString(rule[2])))
                {
                    AddNonterminal(Convert.ToString(rule[2]));
                }

                ConvertRuleTransition(rule);

                return true;
            }
            else
            {
                return false;
            }
        }

        #region Functions working with list of Nonterminal and Terminal
        private void AddNonterminal(string nonterminal)//Добавляение нового терминала в список терминалов List<List<string> Nonterminal
        {
            if (Nonterminal.Count == 0)//Если список нетерминалов пуст (при первом вызове функции, то резервируем первый нетерминал для финального
            {
                Nonterminal = new List<List<string>>();//Инициализируем список нетерминалов            
                Nonterminal.Add(new List<string>());//Инициализируем новый нетерминал, резервируем первый нетерминал под финальный
                Nonterminal[0].Add("0");//Задаем минимальную кодировку для нетерминалов для внутреннего представления КА (начинается с нуля)
                Nonterminal[0].Add("t");//Условное обозначение финального нетерминала
                finalStateArr = new List<string>();
                finalStateArr.Add(Nonterminal[0][1]);//Заносим в список финальных состояний, что финальное состояние в минимальной кодировке имеет номер "0"
                /*finiteStateMachine.Add(new ObservableCollection<string>());*/
                finiteStateMachine[0].Add("-");
            }

            Nonterminal.Add(new List<string>());//Инициализируем новый нетерминал
            Nonterminal[Nonterminal.Count - 1].Add(Convert.ToString(Nonterminal.Count - 1));//Задаем минимальную кодировку для нетерминалов для внутреннего представления КА (так как добавляется в конец, берётся длинна списка нетерминалов минус единица)
            Nonterminal[Nonterminal.Count - 1].Add(nonterminal);//Заносим значение вводимого нетерминала, для дальнейшей раскодировки

            for (int indexTerminal = 0; indexTerminal < CountQuantityTerminal(); indexTerminal++)//При появлении нового нетерминала, к каждому терминалу добавляется дополнительное поле и по умолчанию значение ячейки устанвливается "-"
            {
                finiteStateMachine[indexTerminal].Add("-");
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

            finiteStateMachine.Add(new ObservableCollection<string>());

            for (int indexNonterminal = 0; indexNonterminal < CountQuantityNonterminal(); indexNonterminal++)//При добавлении нового терминала, добавляем ему все возможные нетерминалы и по умолчанию значение ячейки устанвливается "-"
            {                
                finiteStateMachine[CountQuantityTerminal() - 1].Add("-");
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
                if (Terminal[index][1] == terminal)
                {
                    return true;
                }
            }

            return false;
        }
        private int indexOfNonterminalEncode(string nonterminal)
        {
            for (int index = 0; index < Nonterminal.Count(); index++)
            {
                if (Nonterminal[index][1] == nonterminal)
                {
                    return Convert.ToInt32(Nonterminal[index][0]);
                }
            }

            return -1;
        }
        private int indexOfTerminalEncode(string terminal)
        {
            for (int index = 0; index < Terminal.Count(); index++)
            {
                if (Terminal[index][1] == terminal)
                {
                    return Convert.ToInt32(Terminal[index][0]);
                }
            }

            return -1;
        }        

        #endregion //Functions working with list of Nonterminal and Terminal

        #region Function working with Stack
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
        #endregion //Function working with Stack

        private bool CheckForDeterministic()//Проверка на детерминированность КА
        {
            for (int terminal = 0; terminal < CountQuantityTerminal(); terminal++)
            {
                for (int nonterminal = 0; nonterminal < CountQuantityNonterminal(); nonterminal++)
                {
                    if (finiteStateMachine[terminal][nonterminal].Length > 1)
                    {
                        deterministic = false;
                        return false;
                    }
                }
            }

            return true;
        }

        private string showWorkProcess(string previousNonterminal, string nextNonterminal, string terminal)
        {
            string result = previousNonterminal + " -> " + terminal + " -> " + nextNonterminal;

            return result;
        }

        private bool checkFinalState(string letterOfState)
        {
            for (int i = 0; i < finalStateArr.Count; i++)
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

        #endregion //Private method
    }
}
