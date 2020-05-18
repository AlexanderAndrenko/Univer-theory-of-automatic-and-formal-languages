using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Policy;

namespace finite_state_machine
{
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
        private List<List<string>> NewNonterminal;

        struct DFA_Properties
        {
            #region Declaration
            public ObservableCollection<ObservableCollection<string>> DFA_finiteStateMachine;//Таблица переходов ДКА
            public List<string> DFA_finalStateArr;//массив финальных состояний ДКА
            public int DFA_startNonterminal;
            public List<List<string>> DFA_Terminal;
            public List<List<string>> DFA_Nonterminal;
            public List<List<string>> Nonterminal_copy;//Копия нетерминалов НКА
            public List<List<string>> NewNonterminal;//Новые нетерминалы
            #endregion //Declaration

            #region Constructor
            public DFA_Properties(int _startNonterminal, List<List<string>> nonterminal_copy)
            {
                DFA_finiteStateMachine = new ObservableCollection<ObservableCollection<string>>();//Таблица переходов ДКА
                DFA_finalStateArr = new List<string>();//массив финальных состояний ДКА
                DFA_startNonterminal = _startNonterminal;
                DFA_Terminal = new List<List<string>>();
                DFA_Nonterminal = new List<List<string>>();
                Nonterminal_copy = nonterminal_copy;
                NewNonterminal = new List<List<string>>();
            }
            #endregion //Constructor

            #region Finctions for convert NFA to DFA

            public void CopyTerminal(List<List<string>> terminalList)//Копирование одного списка в другой
            {
                for (int index = 0; index < terminalList.Count; index++)
                {
                    DFA_Terminal.Add(new List<string>());

                    for (int position = 0; position < terminalList[index].Count; position++)
                    {
                        DFA_Terminal[index].Add(terminalList[index][position]);
                    }
                }
            }
            public void CreateTerminalsInTable()//Создание терминалов в таблице переходов
            {
                for (int index = 0; index < DFA_Terminal.Count; index++)
                {
                    DFA_finiteStateMachine.Add(new ObservableCollection<string>());
                }
            }
            public void CreateNewNonterminalInTable()//Добавление нового нетерминала в таблицу переходов ДКА
            {
                for (int index = 0; index < DFA_finiteStateMachine.Count; index++)
                {
                    DFA_finiteStateMachine[index].Add("-");
                }                
            }
            public string SortNameNonterminal(string name)//Сортировка нетерминалов в ячейке таблицы переходов по возрастанию индексов и удаление повторяющихся нетерминалов
            {
                #region Sort from min to max
                char[] sortNonterminal = new char[name.Length];

                for (int index = 0; index < name.Length; index++)//Конвертация строки в массив char
                {
                    sortNonterminal[index] = name[index];
                }

                char temp;

                for (int i = 0; i < sortNonterminal.Length - 1; i++)
                {
                    for (int j = 0; j < sortNonterminal.Length - i - 1; j++)
                    {
                        if (NonterminalIsExist(Convert.ToString(sortNonterminal[j + 1]), Nonterminal_copy) &&
                            NonterminalIsExist(Convert.ToString(sortNonterminal[j]), Nonterminal_copy) &&
                            indexOfNonterminal_copyEncode(Convert.ToString(sortNonterminal[j + 1]), Nonterminal_copy) > indexOfNonterminal_copyEncode(Convert.ToString(sortNonterminal[j]), Nonterminal_copy))
                        {
                            temp = sortNonterminal[j + 1];
                            sortNonterminal[j + 1] = sortNonterminal[j];
                            sortNonterminal[j] = temp;
                        }
                    }
                }
                #endregion //Sort from min to max

                #region Delete the same character
                List<char> deleteSame = new List<char>();

                for (int index = 0; index < sortNonterminal.Length; index++)
                {
                    deleteSame.Add(sortNonterminal[index]);
                }

                for (int i = 0; i < deleteSame.Count; i++)
                {
                    for (int j = i + 1; j < deleteSame.Count; j++)
                    {
                        if (deleteSame[i] == deleteSame[j])
                        {
                            deleteSame.RemoveAt(j);
                        }
                    }
                }

                #endregion //Delete the same character

                #region Build finish nonterminal

                string finishName = "";

                for (int index = 0; index < deleteSame.Count; index++)//Конвертация списка char в строку
                {
                    finishName += deleteSame[index];
                }

                #endregion //Build finish nonterminal

                return finishName;
            }

            #region Functions for NewNonterminal list
            public void SetNewNonterminal(string nonterminal)//Добавление нового нетерминала в список новых (неотрбаотанных нетерминалов)
            {
                if (!NonterminalIsExist(nonterminal, NewNonterminal))
                {
                    NewNonterminal.Add(new List<string>());
                    NewNonterminal[NewNonterminal.Count() - 1].Add(Convert.ToString(NewNonterminal.Count() - 1));
                    NewNonterminal[NewNonterminal.Count() - 1].Add(nonterminal);
                }
            }
            public void DeleteNewNonterminal(string nonterminal)//Удаление элемента из списка новых терминалов, после его обработки
            {
                int indexEncode = GetEncodeNonterminal(nonterminal);
                
                for (int index = 0; index < NewNonterminal.Count; index++)
                {
                    if (Convert.ToInt32(NewNonterminal[index][0]) == indexEncode)
                    {
                        NewNonterminal.RemoveAt(index);
                    }
                }                
            }

            #endregion //Functions for NewNonterminal list

            #region Functions for DFA_Nonterminal list
            public void SetNonterminal(string nonterminal)
            {
                DFA_Nonterminal.Add(new List<string>());
                DFA_Nonterminal[DFA_Nonterminal.Count() - 1].Add(Convert.ToString(DFA_Nonterminal.Count() - 1));
                DFA_Nonterminal[DFA_Nonterminal.Count() - 1].Add(nonterminal);
            }
            public string GetNameNonterminal(int encodeIndex)
            {
                for (int i = 0; i < DFA_Nonterminal.Count; i++)
                {
                    if (DFA_Nonterminal[i][0] == Convert.ToString(encodeIndex))
                    {
                        return DFA_Nonterminal[i][1];
                    }                    
                }

                return null;
            }
            public int GetEncodeNonterminal(string name)
            {
                for (int i = 0; i < DFA_Nonterminal.Count; i++)
                {
                    if (DFA_Nonterminal[i][1] == name)
                    {
                        return Convert.ToInt32(DFA_Nonterminal[i][0]);
                    }
                }

                return -1;
            }

            #endregion //Functions for DFA_Nonterminal list            

            #region Functions for outside ancestor list nonterminal 
            private bool NonterminalIsExist(string nonterminal, List<List<string>> myList)//Проверка, существует ли уже данный нетерминал
            {
                for (int index = 0; index < myList.Count; index++)
                {
                    if (myList[index][1] == nonterminal)
                    {
                        return true;
                    }
                }

                return false;
            }
            private int indexOfNonterminal_copyEncode(string nonterminal, List<List<string>> myList)
            {
                for (int index = 0; index < myList.Count; index++)
                {
                    if (myList[index][1] == nonterminal)
                    {
                        return Convert.ToInt32(myList[index][0]);
                    }
                }

                return -1;
            }

            #endregion Functions for outside ancestor list nonterminal 

            #endregion //Finctions for convert NFA to DFA
        }

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
            return Nonterminal.Count;
        }
        public int CountQuantityTerminal()
        {
            return Terminal.Count;
        }
        public bool ParseWord(ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой.
        {
            int nonterminal = _startNonterminal;//Стартовый нетерминал в котором находится КА или находится в данный момент
            int terminal = indexOfElementListEncode(Convert.ToString(chain[0]), Terminal);//Первый считанный терминал из цепочки
            string potentialNonterminal = finiteStateMachine[terminal][nonterminal];//Нетерминал в который есть потенциальный переход 
            bool popStack = false; //Условная переменная, отображающая был ли poр из стэка в прошлой итерации
            store = new Stack<string>();//Инициализируем новый стэк (магазин) КА
            store.Clear();//Очищаем стэк от мусора

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                terminal = indexOfElementListEncode(Convert.ToString(chain[numberOfChain]), Terminal);//последний считанный терминал из цепочки

                if (!ElementListIsExist(GetNameTerminal(terminal), Terminal))//Проверка существует ли терминал
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
                    nonterminal = indexOfElementListEncode(potentialNonterminal, Nonterminal);

                    if (numberOfChain + 1 == chain.Length)
                    {
                        if (checkFinalState(GetNameNonterminal(nonterminal)) || checkFinalState(finiteStateMachine[indexOfElementListEncode("E", Terminal)][nonterminal]))
                        {
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            return true;
                        }
                        else if (!stackIsEmpty())
                        {
                            #region StackOutput
                            string pack = getStackElement();
                            lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                            nonterminal = indexOfElementListEncode(Convert.ToString(pack[0]), Nonterminal);
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
                        nonterminal = indexOfElementListEncode(Convert.ToString(pack[0]), Nonterminal);
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
        public bool ConvertNFAtoDFA()//Метод конвертации НКА в ДКА
        {
            if (!deterministic)//Проверка, может КА уже детерминированный
            {
                DFA_Properties dfa = new DFA_Properties(_startNonterminal, Nonterminal);//Объявление структуры будущего ДКА

                dfa.CopyTerminal(Terminal);//Так как терминалы для ДКА не изменятся, то копируем список терминалов НКА в список терминалов ДКА
                dfa.CreateTerminalsInTable();
                dfa.SetNonterminal(GetNameNonterminal(dfa.DFA_startNonterminal));//Создание стартового нетерминала
                dfa.SetNewNonterminal(dfa.GetNameNonterminal(dfa.DFA_startNonterminal));//Добавление стартового нетерминала в список новых нетерминалов
                dfa.CreateNewNonterminalInTable();//Создание нетерминало в таблице под стартовый нетерминал

                string currentNonterminal = GetNameNonterminal(dfa.DFA_startNonterminal);// Текущий нетерминал

                do
                {
                    for (int indexTerminal = 0; indexTerminal < dfa.DFA_Terminal.Count; indexTerminal++)
                    {
                        string newNonterminal = "";//Строка всех нетерминалов в которые возможет переход             

                        for (int nonterminal = 0; nonterminal < currentNonterminal.Length; nonterminal++)
                        {
                            string oneOfManyNonterminal = Convert.ToString(currentNonterminal[nonterminal]);//один нетерминал в которые возможен переход

                            if (ElementListIsExist(oneOfManyNonterminal, Nonterminal) && finiteStateMachine[indexTerminal][indexOfElementListEncode(oneOfManyNonterminal, Nonterminal)] != "-")//Если нетерминал существует, то копируем все возможные переходы из него по данному терминалу 
                            {
                                newNonterminal += finiteStateMachine[indexTerminal][indexOfElementListEncode(oneOfManyNonterminal, Nonterminal)];
                            }                
                        }

                        if (newNonterminal == "")
                        {
                            newNonterminal = "-";
                        }
                        else
                        {
                            newNonterminal = dfa.SortNameNonterminal(newNonterminal);//Сортировка нетерминалов в которые возможен переход для дальнейшего сравнения

                            if (dfa.GetEncodeNonterminal(newNonterminal) == -1)//Если данного нетерминала нет в списке, то метод вернёт -1
                            {
                                dfa.SetNonterminal(newNonterminal);//созданин нового нетерминала в списке нетерминалов
                                dfa.CreateNewNonterminalInTable();//добавление нетерминала в таблицу переходов
                                dfa.SetNewNonterminal(newNonterminal);//добавление нового нетерминала в список новых (неотбраотанных нетерминалов)
                            }
                        }
                        
                        dfa.DFA_finiteStateMachine[indexTerminal][dfa.GetEncodeNonterminal(currentNonterminal)] = newNonterminal;//Присваивание в таблицу переходов возможных нетерминалов
                    }

                    dfa.DeleteNewNonterminal(currentNonterminal);//удаление нетерминала из списка новых нетерминалов

                    if (dfa.NewNonterminal.Count != 0)
                    {
                        currentNonterminal = dfa.NewNonterminal[0][1];
                    }                    

                    /*ЗАДАЧИ*/
                    /* Реализовать учёт финальных состояний. 
                     * Сделать вывод новых правил перехода на экран*/
                }
                while (dfa.NewNonterminal.Count() != 0);             
                
                return true;
            }
            else
            {
                return false;
            }           
        }
        public void ConvertRuleTransition(string rule)
        {
            int preNonterminal = indexOfElementListEncode(Convert.ToString(rule[0]), Nonterminal);
            int terminal = indexOfElementListEncode(Convert.ToString(rule[1]), Terminal);

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
            if (ElementListIsExist(startNonterminal, Nonterminal))
            {
                _startNonterminal = indexOfElementListEncode(startNonterminal, Nonterminal);

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
        
        #endregion //Public method

        #region Private method
        private bool AddRule(string rule)
        {
            if (rule.Length < 4 && rule.Length > 1)
            {
                if (!ElementListIsExist(Convert.ToString(rule[1]), Terminal))
                {
                    AddTerminal(Convert.ToString(rule[1]));
                }

                if (!ElementListIsExist(Convert.ToString(rule[0]), Nonterminal))
                {
                    AddNonterminal(Convert.ToString(rule[0]));
                }                

                if (rule.Length != 2 && !ElementListIsExist(Convert.ToString(rule[2]), Nonterminal))
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
        private bool ElementListIsExist(string nonterminal, List<List<string>> myList)//Проверка, существует ли уже данный нетерминал
        {
            for (int index = 0; index < myList.Count; index++)
            {
                if (myList[index][1] == nonterminal)
                {
                    return true;
                }
            }

            return false;
        }
        private int indexOfElementListEncode(string nonterminal, List<List<string>> myList)
        {
            for (int index = 0; index < myList.Count; index++)
            {
                if (myList[index][1] == nonterminal)
                {
                    return Convert.ToInt32(myList[index][0]);
                }
            }

            return -1;
        }
        private int QuantityElementList(List<List<string>> myList)
        {
            return myList.Count;
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
