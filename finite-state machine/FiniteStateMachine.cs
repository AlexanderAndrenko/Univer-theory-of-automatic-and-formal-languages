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
        public ObservableCollection<ObservableCollection<string>> finiteStateMachine;//Таблица переходов конечного автомата

        #endregion //Daclaration

        #region Constructor
        FiniteStateMachine(int numberOfState, int numberOfCommand)// конструктор класса, создаётся таблица перехода наполненная содержимым по умолчанию
        {
            finiteStateMachine = new ObservableCollection<ObservableCollection<string>>();

            for (int i = 0; i<numberOfState; i++)
            {
                finiteStateMachine.Add(new ObservableCollection<string>());

                for (int j = 0; j<numberOfCommand; j++)
                {
                    finiteStateMachine[i].Add((visualState(i)).ToString());
                }
            }
        }

        #endregion //Constructor

        #region Private method
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
