using Array_Tasks.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Array_Tasks.Utilities
{
    static public class VisualUtility
    {
        //Функция для получения потомственного графического элемента в иерархии
        //childItem это какой то тип, который задаётся в треугольных скобках, а obj в нашем случае родительский элемент
        static public childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject//where ограничивает спектр типов, которые мы можем выбрать. В нашем случае выбираемый тип должен наследствовать от DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)//Проходимся по всей графической иерархии
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);//Получаем потомственный элемент взависимости от индекса
                if (child != null && child is childItem)//Если потомственный элемент хоть чему то равняется и является типом указанным выше
                    return (childItem)child;//То приводим к этому типу и возвращаем элемент
                else//Иначе
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);//Ищем глубже в иерархии
                    if (childOfChild != null)//Если потомок нашего изначального потомка не равен null
                        return childOfChild;//То возвращаем его, иначе цикл повторяется вновь
                }
            }
            return null;//Если весь цикл завершается безрезультатно, то возвращаем null (ничего)
        }

        //Метод для поиска
        static public void FindItemsInListDataTemplate<controlType>(ListBox listBox, List<TaskElementBase> elements) where controlType : Control //Ограничиваем спектр возможных типов для controlType
        {
            for (int i = 0; i < listBox.Items.Count; i++)//Проходимся по всем элементам массива
            {
                //Получаем ListBoxItem каждого элемента нашего ListBox
                ListBoxItem listBoxItem = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(listBox.Items[i]);

                //Получаем графическое представление ListBoxItem
                ContentPresenter contentPresenter = VisualUtility.FindVisualChild<ContentPresenter>(listBoxItem);

                //Получаем графический шаблон каждого элемента
                DataTemplate dataTemplate = contentPresenter.ContentTemplate;

                //Получаем наши строку и поле для ввода
                Label label = (Label)dataTemplate.FindName("label", contentPresenter);
                controlType control = (controlType)dataTemplate.FindName("control", contentPresenter);

                //Кидаем значения в каждый элемент списка заданий
                elements[i].label = label;
                elements[i].control = control;
            }
        }
    }
}
