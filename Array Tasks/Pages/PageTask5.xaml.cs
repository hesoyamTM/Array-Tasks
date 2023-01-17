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
    /// Логика взаимодействия для PageTask5.xaml
    /// </summary>
    public partial class PageTask5 : Page
    {
        private const string pattern = @"^[a-zA-Z_]+\s+(&*[a-zA-Z_]+[0-9a-zA-Z_]*((\[[1-9]+[0-9a-zA-Z\s\+\s+]*\]\s*)+|\[\]\s*=+\s*\{[\w\s\,\-]+\})|\*&*[a-zA-Z_]+[0-9a-zA-Z_]*\s*=+\s*new [a-zA-Z_]+\[[1-9]+[0-9a-zA-Z\s\+\s+]*\])$";
        private const int taskCountPerColumn = 9;

        private List<TaskElement> elementsForLeftColumn = new List<TaskElement>();
        private List<TaskElement> elementsForRightColumn = new List<TaskElement>();


        public PageTask5()
        {
            InitializeComponent();

            Random rand = new Random();
            for (int i = 0; i < taskCountPerColumn; i++)
                GenerateCondition(rand, elementsForLeftColumn);
            for (int i = 0; i < taskCountPerColumn; i++)
                GenerateCondition(rand, elementsForRightColumn);

            listBoxLeftColumn.ItemsSource = elementsForLeftColumn;
            listBoxRightColumn.ItemsSource = elementsForRightColumn;
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VisualUtility.FindItemsInListDataTemplate<CheckBox>(listBoxLeftColumn, elementsForLeftColumn.Cast<TaskElementBase>().ToList());
            VisualUtility.FindItemsInListDataTemplate<CheckBox>(listBoxRightColumn, elementsForRightColumn.Cast<TaskElementBase>().ToList());
        }


        private void GenerateCondition(Random rand, List<TaskElement> taskElements)
        {
            //В переменную result записывается вопрос
            string result;
            //Необходимые списки для генерации вопроса
            string[] types = { "int", "string", "long", "float", "bool", "double", "short" };
            string[] bracket1 = { "[", "{", "(", "|" };
            string[] bracket2 = { "]", "}", ")", "|" };
            //Случайно выбираем тип массива, который будет использоваться
            int massiveType = rand.Next(5);

            if (massiveType == 0)//Динамический
            {
                //Выбираем случайно тип данных массива
                int type = rand.Next(types.Length);
                //Сразу записываем результат
                result = $"{types[type]} *{VarName(rand).Replace(" ", "")} = new {types[type]}[{rand.Next(-10, 100)}]";
            }
            else if (massiveType == 1)//Значение которого задаётся при объявлении
            {
                //Элементы в массиве
                string elements = " ";

                //Рандомно от 2 до 5 раз записываем какое случайное число, которое будет использоваться в качестве элемента массива
                for (int i = 0; i < rand.Next(3, 6); i++)
                {
                    if (i == 0)//Первый элемент без запятой
                        elements += rand.Next(-100, 100);
                    else//Перед остальными всегда запятая
                        elements += $", {rand.Next(-100, 100)}";
                }
                //Не забываем про пробел
                elements += " ";

                //Рандомные скобочки
                int bracketType = (rand.Next(3) > 0) ? 0 : rand.Next(1, bracket1.Length);
                //Запись результата
                result = $"int {VarName(rand)}{bracket1[bracketType]}{bracket2[bracketType]} = {{{elements}}}";
            }
            else//Обычный
            {
                //Рандомный тип данных и скобочки
                int type = rand.Next(types.Length);
                int bracketType = (rand.Next(3) > 0) ? 0 : rand.Next(1, bracket1.Length);
                //Записываем результат
                result = $"{types[type]} {VarName(rand)}{bracket1[bracketType]}{rand.Next(-10, 100)}{bracket2[bracketType]}";

                //Если повезёт, то массив будет двумерный
                if (rand.Next(3) == 0)
                    result += $"{bracket1[bracketType]}{rand.Next(-10, 100)}{bracket2[bracketType]}";
            }

            //Записываем элемент в список. На место ответа пишем null, потому что он пока ничему не равняется
            taskElements.Add(new TaskElement(result, null, () => CheckingRightColumnAnswers(taskElements)));
        }

        private string VarName(Random rand)
        {
            //Объявляем результат
            string result = " ";

            //Просто на рандом добавляем разные символы
            if (rand.Next(4) == 0)
                result += "&";

            if (rand.Next(4) == 0)
                result += "_";

            //Рандом между буквой и цифрой
            if (rand.Next(4) == 0)
                result += rand.Next(10).ToString();
            else
                result += LetterUtility.GetRandomLetter(rand);

            //Возврат
            return result;
        }


        private void CheckingRightColumnAnswers(List<TaskElement> taskElements)
        {
            bool keepRight = true;

            for (int i = 0; i < taskCountPerColumn; i++)
            {
                keepRight = keepRight && Equals(((CheckBox)taskElements[i].control).IsChecked, Regex.IsMatch(taskElements[i].condition, pattern));

                if (!keepRight)
                    break;
            }

            if (keepRight)
            {
                for (int i = 0; i < taskCountPerColumn; i++)
                {
                    taskElements[i].answer = keepRight;
                    ((CheckBox)taskElements[i].control).IsChecked = true;
                }
            }
        }



        public class TaskElement : TaskElementBase
        {
            private Action action;

            public TaskElement(string c, string a, Action _action) : base(c, a)//Переопределяем конструктор
            {
                action = _action;
                onPropChangedAction = () => OnPropertyChanged(control);
            }

            protected override void OnPropertyChanged(Control control)//Переопределяем функцию
            {
                action();

                if (answer != null && (bool)answer)
                    base.OnPropertyChanged(control);
            }
        }
    }
}
