using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Array_Tasks.Pages
{
    public class TaskElementBase//Базовый класс для элементов задания для каждой страницы. Здесь описаны самые базовые функции, которые выполняет каждый наследуемый класс
    {
        public string condition { protected set; get; }//Вопрос
        public object answer { set; get; }//Верный ответ
        public Label label { set; get; }//Строка как элемент интерфейса
        public Control control { set; get; }//Поле ввода
        protected Action onPropChangedAction;

        public TaskElementBase(string _condition, string _answer)//Конструктор
        {
            //Просто задаём значение переменных
            condition = _condition;
            answer = _answer;
        }


        private ICommand _propertyChanged;//Выполняемая комманда, являет деталью реализацией, поэтому скрыта
        public ICommand PropertyChanged//Комманда для интерфейса взаимодействия класса. С ним взаимодействуют другие объекты и классы
        {
            get//При получении значения
            {
                if (_propertyChanged == null)//Если _textChanged не имеет значения
                    _propertyChanged = new RelayCommand(onPropChangedAction);//То создаётся новый экземпляр класса RelayCommand, в качестве аргумента указываем на наше действие

                return _propertyChanged;//В итоге возвращаем _textChanged
            }
        }

        protected virtual void OnPropertyChanged(Control tb)//Функция 
        {
            //Окрашевание в лаймовый цвет поля и текста
            tb.Foreground = Brushes.Lime;
            label.Foreground = Brushes.Lime;
            //Неактивность поля
            tb.IsEnabled = false;
        }
    }
}
