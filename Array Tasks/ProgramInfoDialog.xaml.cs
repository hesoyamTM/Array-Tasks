using System;
using System.Collections.Generic;
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

        public string info
        {
            //Брать данные будем из текстового файла в ресурсах
            get
            {
                //Обращаемся с ресурсам и берём наш текстовый файл с информацией
                StreamResourceInfo resourceInfo = Application.GetResourceStream(new Uri(@"Source\ProgramInfo.txt", UriKind.Relative));

                //Создаём StreamReader и указываем поток из информации о ресурсе. Указываем кодировку
                using (StreamReader streamReader = new StreamReader(resourceInfo.Stream, Encoding.Default))
                    return streamReader.ReadToEnd();//Просто читаем до конца
            }
        }
    }
}
