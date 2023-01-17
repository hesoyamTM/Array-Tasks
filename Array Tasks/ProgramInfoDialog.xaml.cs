using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Array_Tasks
{
    /// <summary>
    /// Логика взаимодействия для ProgramInfoDialog.xaml
    /// </summary>
    public partial class ProgramInfoDialog : Window
    {
        public ProgramInfoDialog()
        {
            InitializeComponent();
            //Иначе текст показываться не будет
            DataContext = this;
        }

        //Методы для работоспособнсти тулбара
        private void ExitButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.Close();
        private void ToolBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => this.DragMove();

        //Метод для перехода по ссылке
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
