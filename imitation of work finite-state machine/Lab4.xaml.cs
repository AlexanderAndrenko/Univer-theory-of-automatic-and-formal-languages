using System;
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
using System.IO;
using System.Data;
using System.Collections.ObjectModel;
using finite_state_machine;

namespace Translator
{
    /// <summary>
    /// Interaction logic for Lab4.xaml
    /// </summary>
    public partial class Lab4 : Page
    {
        private FiniteStateMachine fm;
        public string textOfFile = "";
        public ObservableCollection<string> lineOfWorkProcess { get; set; }
        public Lab4()
        {
            InitializeComponent();

            if (File.Exists("rule.txt"))
            {
                StreamReader sr = new StreamReader("rule.txt");
                textOfFile += sr.ReadLine();
                ShowFileText.Text = textOfFile;
                sr.Close();
                fm = new FiniteStateMachine(textOfFile);
                CorrectRule.Text = fm.addedRules;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string chainrules = ChainRules.Text;
            File.WriteAllText("rule.txt", chainrules);
            ShowFileText.Text = chainrules;
            fm = new FiniteStateMachine(chainrules);
            CorrectRule.Text = fm.addedRules;
        }

        private void setChainOfCommand_Click(object sender, RoutedEventArgs e)
        {
            lineOfWorkProcess = new ObservableCollection<string>();
            workProcess.ItemsSource = lineOfWorkProcess;
            if (fm.ParseWord(chainOfcommand.Text, lineOfWorkProcess, chainOfcommand.Text))
            {
                lineOfWorkProcess.Add("Цепочка относится к заданной регулярной грамматике");
            }
        }
    }
}
