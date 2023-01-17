using Array_Tasks.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PageTask3.xaml
    /// </summary>
    public partial class PageTask3 : Page
    {
        private const string pattern = @"^[a-zA-Z_]+\s*=\s*[a-zA-Z_]+\[[\w]+\]\s*[\-*/+]\s*([a-zA-Z_]+\[[\w]+\]|[\w_]+)$";
        private const int length = 100;
        private const int taskCount = 7;

        private List<TaskElement> elements = new List<TaskElement>();

        public PageTask3()
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


        private void GenerateConditionAndAnswer(Random rand)
        {
            //Вопрос и правильный ответ
            string condition, answer;

            //Операции для вопросов и ответов
            string[] operationsCon = { "умноженный на", "делённый на", "увеличенный на", "уменьшенный на" };
            string[] operationsAns = { "*", "/", "+", "-" };

            //Случайная математическая операция
            int randVal = rand.Next(0, operationsCon.Length);
            //Первый индекс
            string indexA = LetterUtility.GetRandomLetter(rand);

            //Случайный выбор между переменной или значением
            if (rand.Next(0, 2) == 0)
                rand.Next(0, length).ToString();

            //Имя переменной и имя массива
            string varName, arrayName;

            varName = LetterUtility.GetRandomLetter(rand).ToLower();
            arrayName = LetterUtility.GetRandomLetter(rand).ToUpper();

            //Запись вопроса и правильного ответа
            condition = $"Значение переменной {varName} равно элемент массива {arrayName} с индексом {indexA}, {operationsCon[randVal]} ";
            answer = $"{varName}={arrayName}[{indexA}]{operationsAns[randVal]}";

            //Случайное действие
            int randVal2 = rand.Next(0, 3);

            if (randVal2 == 0)//В первом случае
            {
                //Второй индекс
                string indexB = LetterUtility.GetRandomLetter(rand); ;

                //Выбор между цифрой или буквой
                if (rand.Next(0, 2) == 0)
                    indexB = rand.Next(0, length).ToString();

                //Имя второго массива
                arrayName = LetterUtility.GetRandomLetter(rand).ToUpper();

                //Добавление вторых частей вопроса и ответа к первой
                condition += $"элемент массива {arrayName} с индексом {indexB}";
                answer += $"{arrayName}[{indexB}]";
            }
            else if (randVal2 == 1)//Во втором случае
            {
                //Дейсвие происходит на рандомное число
                int b = rand.Next(0, length);

                //Добавление вторых частей вопроса и ответа к первой
                condition += b.ToString();
                answer += b.ToString();
            }
            else//Третий случай
            {

                string b = LetterUtility.GetRandomLetter(rand);

                //Добавление вторых частей вопроса и ответа к первой
                condition += b;
                answer += b;
            }

            elements.Add(new TaskElement(condition, answer));
        }


        public class TaskElement : TaskElementBase//В классе TaskElementBase написаны все базовые функции для TaskElement в каждом задании. Единственное что надо будет менять, это способ проверки задания, переопределяя функцию, отвечающую за это
        {
            public TaskElement(string c, string a) : base(c, a) => onPropChangedAction = () => OnPropertyChanged(control);//Переопределяем конструктор

            protected override void OnPropertyChanged(Control control)//Переопределяем функцию
            {
                TextBox tb = (TextBox)control;

                if (Regex.IsMatch(tb.Text, pattern) && tb.Text.Replace(" ", "") == answer.ToString())//Проверка на соответствие ответа пользователя на паттерн и на правильность
                    base.OnPropertyChanged(tb);//Вызываем изначальную функцию
            }
        }
    }
}
