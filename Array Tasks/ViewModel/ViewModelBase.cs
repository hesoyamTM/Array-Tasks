using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Array_Tasks.ViewModel
{
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        //Переменная, хранящая события, обробатываемые при изменении параметра
        public event PropertyChangedEventHandler PropertyChanged;

        //Метод, вызывающий все функции в событии PropertyChanged при его наличии
        protected virtual void onPropertyChanged([CallerMemberName] string property = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        //Метод, задающий значение для переменной
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            onPropertyChanged(PropertyName);

            return true;
        }
    }
}
