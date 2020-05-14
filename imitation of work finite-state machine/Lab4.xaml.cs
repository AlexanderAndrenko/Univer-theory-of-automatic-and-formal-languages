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
                SetRadioButtonStartNonterminal();
                start.Text = fm.GetNameNonterminal(1);//По умолчанию стартовым задаётся первый нетерминал. 0 зарезервированно под финальный.
                SetCheckBoxFinalNonterminal();
                finals.Text = "t";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string chainrules = ChainRules.Text;
            File.WriteAllText("rule.txt", chainrules);
            ShowFileText.Text = chainrules;
            fm = new FiniteStateMachine(chainrules);
            CorrectRule.Text = fm.addedRules;
            SetRadioButtonStartNonterminal();
            SetCheckBoxFinalNonterminal();
        }

        private void SetRadioButtonStartNonterminal()
        {
            spRadioButton.Children.Clear();

            List<RadioButton> rb = new List<RadioButton>();

            for (int index = 0; index < fm.CountQuantityNonterminal() - 1; index++)
            {
                rb.Add(new RadioButton());
                rb[index].IsChecked = false;
                rb[index].Content = fm.GetNameNonterminal(index + 1);
                rb[index].GroupName = "startNonterminal";
                rb[index].Checked += RadioButton_Checked;
                spRadioButton.Children.Add(rb[index]);
            }
        }

        private void SetCheckBoxFinalNonterminal()
        {
            spCheckBox.Children.Clear();

            List<CheckBox> cb = new List<CheckBox>();

            for (int index = 0; index < fm.CountQuantityNonterminal() - 1; index++)
            {
                cb.Add(new CheckBox());
                cb[index].IsChecked = false;
                cb[index].Content = fm.GetNameNonterminal(index + 1);
                cb[index].Checked += CheckBox_Checked;
                spRadioButton.Children.Add(cb[index]);
            }
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            start.Text = pressed.Content.ToString();
            fm.SetStartNonterminal(pressed.Content.ToString());
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            finals.Text = check.Content.ToString();
            fm.SetFinalState(check.Content.ToString());
        }
    }
}
