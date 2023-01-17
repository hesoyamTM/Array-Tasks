using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Array_Tasks.Themes
{
    static public class ThemeManager
    {
        //Наши темы
        public enum Themes
        {
            White = 0,
            Dark = 1
        }

        //Пути к темам
        static private string[] ThemesUri = {
            @"Themes\WhiteTheme.xaml",
            @"Themes\DarkTheme.xaml"
        };

        static public Themes currentTheme = Themes.White;


        //Метод, меняющий темы
        static public void SelectTheme(Themes theme)
        {
            //Получаем путь к нашей нужной теме
            Uri themeUri = new Uri(ThemesUri[(int)theme], UriKind.Relative);
            //Загружаем её
            ResourceDictionary resourceDictionary = Application.LoadComponent(themeUri) as ResourceDictionary;
            //Применяем на всю программу
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            //Записываем в переменную
            currentTheme = theme;
        }
    }
}
