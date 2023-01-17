using Array_Tasks.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using Array_Tasks.ViewModel;
using Array_Tasks.Themes;

namespace Array_Tasks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        //Методы для работоспособнсти тулбара
        private void MinimizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.WindowState = WindowState.Minimized;
        private void ExitButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();
        private void ToolBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        private void ThemeChangeTheme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Меняем тему в зависимости от текущей
            if (ThemeManager.currentTheme == ThemeManager.Themes.Dark)
                ThemeManager.SelectTheme(ThemeManager.Themes.White);
            else
                ThemeManager.SelectTheme(ThemeManager.Themes.Dark);
        }
    }
}
