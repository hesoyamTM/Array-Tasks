using Array_Tasks.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Array_Tasks.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        //Страницы
        private Page task1 = new PageTask1();
        private Page task2 = new PageTask2();
        private Page task3 = new PageTask3();
        private Page task4 = new PageTask4();
        private Page task5 = new PageTask5();


        private Page _CurPage = new PageTask1();
        public Page currentPage
        {
            //На выходе мы просто получаем значение переменной _CurPage
            get => _CurPage;
            //Просто задаём значение для _CurPage взависимости от входного значения
            set => Set(ref _CurPage, value);
        }

        
        //Комманды для открытия страниц для каждой кнопки
        public ICommand OpenPageTask1 { get => new RelayCommand(() => currentPage = task1); }
        public ICommand OpenPageTask2 { get => new RelayCommand(() => currentPage = task2); }
        public ICommand OpenPageTask3 { get => new RelayCommand(() => currentPage = task3); }
        public ICommand OpenPageTask4 { get => new RelayCommand(() => currentPage = task4); }
        public ICommand OpenPageTask5 { get => new RelayCommand(() => currentPage = task5); }




        //Переменная, чтобы пользователь не создавал окон больше одного
        private ProgramInfoDialog currentInfoDialog;

        //Команда для показа информации о программе
        public ICommand ShowProgramInfo { get => new RelayCommand(() => ShowProgramInfoDialog()); }

        //Метод для показа диаологового окна с информацией о программе
        private void ShowProgramInfoDialog()
        {
            //Если окна нет
            if (currentInfoDialog == null)
            {
                //Создаём его, показываем и активируем
                currentInfoDialog = new ProgramInfoDialog();
                currentInfoDialog.Show();
                currentInfoDialog.Activate();

                //При закрытии задаём переменной значение null
                currentInfoDialog.Closed += (obj, e) => currentInfoDialog = null;
            }
        }
    }
}
