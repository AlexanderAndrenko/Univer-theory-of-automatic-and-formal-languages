using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{    
    public class myConverter
    {
        public static string visualState(int valueOfCommand)
        {
            string result = Convert.ToString(Convert.ToChar(valueOfCommand + 65));

            return result;
        }

        public static int convertToNumber(string letter)
        {
            int result = Convert.ToInt32(Convert.ToChar(letter)) - 65; ;

            return result;
        }

        public static bool checkExistSymbol(string symbol, int size, bool terminal)
        {
            for (int i = 0; i < size; i++)
            {
                if (!terminal)
                {
                    if (symbol == visualState(i))
                    {
                        return true;
                    }
                }
                else
                {
                    if (symbol == visualState(i + 32) || symbol == "E")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /*public bool ParseWord(string parseWord, ObservableCollection<string> lineOfWorkProcess, string chain)//Парсинг строки с праволинейной грамматикой.
        {
            int nonterminal = 1;//Нетерминал в котором КА находится в данный момент
            int terminal;//Последний считанный терминал из цепочки
            string potentialNonterminal;//Нетерминал в который есть потенциальный переход
            bool popStack = false; //Условная переменная, отображающая был ли poр из стэка в прошлой итерации
            store = new Stack<string>();//Инициализируем новый стэк (магазин) КА
            store.Clear();//Очищаем стэк от мусора

            for (int numberOfChain = 0; numberOfChain < chain.Length; numberOfChain++)
            {
                terminal = Convert.ToInt32(chain[numberOfChain]) - 97;

                if (!popStack)
                {
                    if (finiteStateMachine[terminal][nonterminal].Length > 1)
                    {
                        potentialNonterminal = Convert.ToString(finiteStateMachine[terminal][nonterminal].First());
                        setStackElement(convertNumberToString(nonterminal), finiteStateMachine[terminal][nonterminal].Substring(1), numberOfChain);
                        lineOfWorkProcess.Add("Добавление в стэк возможного перехода: " + potentialNonterminal + finiteStateMachine[terminal][nonterminal].Substring(1) + numberOfChain);
                    }
                    else if (nonterminal == CountQuantityNonterminal() - 1)
                    {
                        potentialNonterminal = convertNumberToString(nonterminal);
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


                if ((potentialNonterminal != "-") && convertStringToNumber(potentialNonterminal) < finiteStateMachine[terminal].Count)//Проверка что след. нетерминал присутствует в алфавите
                {
                    if (checkFinalState(potentialNonterminal))
                    {
                        if (numberOfChain + 1 == chain.Length)
                        {
                            lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(nonterminal), "t", terminal));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            lineOfWorkProcess.Add("Работа окончена.");

                            return true;
                        }
                        else if (deterministic)
                        {
                            lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(nonterminal), "t", terminal));
                            lineOfWorkProcess.Add("Автомат достиг финального состояния!");
                            nonterminal = convertStringToNumber(potentialNonterminal);

                            return true;
                        }
                        else
                        {
                            if (!stackIsEmpty())
                            {
                                string pack = getStackElement();
                                lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                                nonterminal = convertStringToNumber(Convert.ToString(pack[0]));
                                potentialNonterminal = Convert.ToString(pack[1]);
                                pack = pack.Substring(2);
                                numberOfChain = Convert.ToInt32(pack) - 1;
                                popStack = true;
                                continue;
                            }
                            else
                            {
                                lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                                lineOfWorkProcess.Add("Следующий нетерминал при терминале " + terminal + " не определен.");
                                break;
                            }
                        }
                    }
                    else //potentialNonterminal != ""
                    {
                        lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(nonterminal), potentialNonterminal, terminal));
                        //nonterminal = convertStringToNumber(potentialNonterminal);
                        nonterminal = indexOfNonterminalEncode(potentialNonterminal);


                        if (numberOfChain + 1 == chain.Length && checkFinalState(Convert.ToString(nonterminal)))
                        {
                            if (finiteStateMachine[CountQuantityTerminal() - 1][nonterminal] != "-")
                            {
                                lineOfWorkProcess.Add(showWorkProcess(convertNumberToString(nonterminal), "t", CountQuantityTerminal() - 1));
                                lineOfWorkProcess.Add("Автомат достиг финального состояния!");

                                return true;
                            }
                            else if (!stackIsEmpty())
                            {
                                string pack = getStackElement();
                                lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                                nonterminal = convertStringToNumber(Convert.ToString(pack[0]));
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
                else if (finiteStateMachine[terminal][nonterminal] == "-")
                {
                    if (!stackIsEmpty())
                    {
                        string pack = getStackElement();
                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                        nonterminal = convertStringToNumber(Convert.ToString(pack[0]));
                        potentialNonterminal = Convert.ToString(pack[1]);
                        pack = pack.Substring(2);
                        numberOfChain = Convert.ToInt32(pack) - 1;
                        popStack = true;
                        continue;
                    }
                    else
                    {
                        lineOfWorkProcess.Add("Ошибка. Аварийная остановка!");
                        lineOfWorkProcess.Add("Следующий нетерминал при терминале " + terminal + " не определен.");
                        break;
                    }
                }
                else
                {
                    if (!stackIsEmpty())
                    {
                        string pack = getStackElement();
                        lineOfWorkProcess.Add("Тупик. Возврат к значению из стэка: " + pack);
                        nonterminal = convertStringToNumber(Convert.ToString(pack[0]));
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
        }*/
    }
}
