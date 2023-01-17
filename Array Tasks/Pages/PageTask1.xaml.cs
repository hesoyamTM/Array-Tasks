using Array_Tasks.Utilities;
using GalaSoft.MvvmLight.Command;
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
    /// Логика взаимодействия для PageTask1.xaml
    /// </summary>
    public partial class PageTask1 : Page
    {
        const string pattern = @"^[\w]+[\s]+[a-zA-z_]\[[0-9]+\]";//Паттерн для регулярных выражений
        const int taskount = 10;//Кол-во заданий

        private List<TaskElement> elements = new List<TaskElement>();//Список из элементов заданий
        private int completedTasksCount = 0;//Кол-во завершённых заданий


        public PageTask1()//Конструктор страницы
        {
            InitializeComponent();//Инициализация элементов интерфейса

            Random rand = new Random();//Рандом
            GenerateConditionAndAnswer(rand);//Создание вопросов и правильных ответов
        }

        //Поиск мы осуществляем только после полной загрузки ListBox, иначе при получении UIElement (графического элемента) оно будет равняться null (ничему)
        private void tasksListBox_Loaded(object sender, RoutedEventArgs e) => VisualUtility.FindItemsInListDataTemplate<TextBox>(tasksListBox, elements.Cast<TaskElementBase>().ToList());//Поиск графических элементов в списке для дальнейшей работы



        private void GenerateConditionAndAnswer(Random rand)//Реализация функции
        {
            string[] types = { "int", "float", "string", "bool", "long", "double" };//Все типы данных переменных, которые будут использоваться в вопросах

            for (int i = 0; i < taskount; i++)//Создаём 10 заданий
            {
                string condition, answer, name, count, type;//Все необходимые переменные

                if (rand.Next(0, 4) != 0)//Если выпадает шанс 75%
                    name = LetterUtility.GetRandomLetter(rand);//То в нижний регистр
                else//Если 25%
                    name = LetterUtility.GetRandomLetter(rand).ToUpper();//То верхний

                count = rand.Next(2, 100).ToString();//Случайное кол-во элементов в массиве от 2 дл 99

                type = types[rand.Next(0, types.Length)];//Случайный выбор типа данных из предложенного списка

                answer = $"{type}{name}[{count}]";//Составление правильного ответа без пробелов
                condition = $"Массив {name} состоит из {count} элементов типа данных {type}";//Составление вопроса

                elements.Add(new TaskElement(condition, answer));//Добавление в элемента в список
            }

            tasksListBox.ItemsSource = elements;//Вывод списка на экран
            DataContext = this;//Чтобы работал вывод
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
