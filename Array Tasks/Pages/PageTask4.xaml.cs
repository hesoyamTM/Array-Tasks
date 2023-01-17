using Array_Tasks.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace Array_Tasks.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTask4.xaml
    /// </summary>
    public partial class PageTask4 : Page
    {
        private const int taskCount = 6;
        private List<TaskElement> elements = new List<TaskElement>();

        public PageTask4()
        {
            InitializeComponent();

            Random rand = new Random();
            for (int i = 0; i < taskCount; i++)
                GenerateConditionAndAnswer(rand);

            tasksListBox.ItemsSource = elements;
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VisualUtility.FindItemsInListDataTemplate<TextBox>(tasksListBox, elements.Cast<TaskElementBase>().ToList());
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ((TextBox)sender).Text.Length > 0)
            {
                ((TextBox)sender).Text += "    ";
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            else if (e.Key == Key.OemOpenBrackets && ((TextBox)sender).Text.Last() == '{')
            {
                ((TextBox)sender).Text += "\n    \n}";
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length-2;
            }
        }


        private void GenerateConditionAndAnswer(Random rand)
        {
            //Случайное имя переменной и кол-во итераций
            string arrName = LetterUtility.GetRandomLetter(rand).ToUpper();
            string count = rand.Next(10, 1000).ToString();

            if (rand.Next(0, 3) == 0)
                count = LetterUtility.GetRandomLetter(rand);

            //Генерация ответа и вопроса. В ответ записывается паттерн, по которому должен соответствовать ответ пользователя
            string answer = @"^for\s*\(int\s*{0}\s*=\s*0;\s*{0}\s*<\s*" + count + @";\s*{0}(\+\+|\s*\+=\s*1|\s*=\s*{0}\s*\+\s*1)\)\s*(\{\s*(std::)*cin\s*>>\s*" + arrName + @"\[{0}\];\s*\}|(std::)*cin\s*>>\s*" + arrName + @"\[{0}\];\s*)$";
            string condition = $"Ручной ввод {count} элементов массива {arrName} с клавиатуры";

            //Добавление в список элемента
            elements.Add(new TaskElement(condition, answer, count));
        }




        public class TaskElement : TaskElementBase
        {
            private string count;

            public TaskElement(string c, string a, string _count) : base(c, a)//Переопределяем конструктор
            {
                count = _count;
                onPropChangedAction = () => OnPropertyChanged(control);
            }

            protected override void OnPropertyChanged(Control control)//Переопределяем функцию
            {
                TextBox tb = (TextBox)control;
                //Сразу определяем название переменной, которое использовал пользователь, потому что она может быть любой
                string varName = Regex.Match(tb.Text, @"(?<=int\s+)[a-zA-Z_]+").Value;

                if (count != varName && Regex.IsMatch(tb.Text.Replace("\n", ""), answer.ToString().Replace("{0}", varName)))//Просто проверяем ответ пользователя на соответсвие с паттерном (в answer как раз содержится паттерн)
                    base.OnPropertyChanged(tb);//Вызываем изначальную функцию
            }
        }
    }
}
