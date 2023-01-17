using Array_Tasks.Utilities;
using GalaSoft.MvvmLight.Command;
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

namespace Array_Tasks.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTask2.xaml
    /// </summary>
    public partial class PageTask2 : Page
    {
        //Кол-во заданий в одном столбце
        const int taskCountPerColumn = 8;

        //Элементы в столбцах
        private List<TaskElement> elementsLeftColumn = new List<TaskElement>();
        private List<TaskElement> elementsRightColumn = new List<TaskElement>();
        //Данные по массиву
        private object[] array;
        private string arrayName;

        public PageTask2()
        {
            //Инициализация компонентов
            InitializeComponent();

            //Рандом
            Random rand = new Random();

            //Генерация массива
            GenerateArray(rand);

            //Генерация заданий для двух столбцов
            for (int i = 0; i < taskCountPerColumn; i++)
                GenerateConditionAndAnswer(rand, elementsLeftColumn);
            for (int i = 0; i < taskCountPerColumn; i++)
                GenerateConditionAndAnswer(rand, elementsRightColumn);

            //Вывод данных для пользователя
            listBoxLeftColumn.ItemsSource = elementsLeftColumn;
            listBoxRightColumn.ItemsSource = elementsRightColumn;
            arrayListView.ItemsSource = array;
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Находим поля для ответа и строку с заданием для дальнейшего взаимодействия
            VisualUtility.FindItemsInListDataTemplate<TextBox>(listBoxLeftColumn, elementsLeftColumn.Cast<TaskElementBase>().ToList());
            VisualUtility.FindItemsInListDataTemplate<TextBox>(listBoxRightColumn, elementsRightColumn.Cast<TaskElementBase>().ToList());
        }


        private void GenerateConditionAndAnswer(Random rand, List<TaskElement> elements)
        {
            string[] operators = { "*", "+", "-" };
            //Рандомное значение для дальнейших действий
            int randVal = rand.Next(0, 11);
            //Вопрос и правильный ответ
            string condition, answer;
            //Первый элемент
            int indexA = rand.Next(1, array.Length);
            int a = (int)array[indexA];
            //Второй элемент
            int indexB = rand.Next(1, array.Length);
            int b = (int)array[indexB];

            if (randVal <= 2)
            {
                //Рандомное число
                b = rand.Next(0, 6);

                //Случайное математическое действие
                string _operator = operators[rand.Next(0, operators.Length)];
                //Вопрос и правильный ответ
                condition = $"{arrayName}[{indexA - 1}]{_operator}{b}";
                answer = StringToOperator(_operator, a, b).ToString();
            }
            else if (randVal >= 3 && randVal <= 6)
            {
                //То же самое, но второе значение выбирается из массива
                string _operator = operators[rand.Next(1, operators.Length)];
                condition = $"{arrayName}[{indexA - 1}]{_operator}{arrayName}[{indexB - 1}]";
                answer = StringToOperator(_operator, a, b).ToString();
            }
            else
            {
                //Только одно значение из массива, без мат. действий
                condition = $"{arrayName}[{indexA - 1}]";
                answer = a.ToString();
            }

            //Добавление вопроса и правильного ответа в список
            elements.Add(new TaskElement(condition, answer));
        }

        private void GenerateArray(Random rand)
        {
            //Генерация самого массива
            array = new object[rand.Next(7, 11)];

            //Имя массива
            arrayName = LetterUtility.GetRandomLetter(rand);
            array[0] = arrayName + "  :   ";

            //Генерация элементов массива
            for (int i = 1; i < array.Length; i++)
                array[i] = rand.Next(-25, 25);
        }

        private int StringToOperator(string _operator, int a, int b)
        {
            //Про определённое мат. действии в string, выполняем дейтсвие
            if (_operator == "*")
                return a * b;
            else if (_operator == "-")
                return a - b;
            else
                return a + b;
        }


        class TaskElement : TaskElementBase
        {
            //Всё то же самое что и в первом задании
            public TaskElement(string c, string a) : base(c, a) => onPropChangedAction = () => OnPropertyChanged(control);

            protected override void OnPropertyChanged(Control control)
            {
                TextBox tb = (TextBox)control;

                if (Equals(tb.Text, answer))//Единственное отличие в проверке. Здесь мы паттерны не используем
                    base.OnPropertyChanged(tb);
            }
        }
    }
}
