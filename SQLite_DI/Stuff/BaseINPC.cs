using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_DI.Stuff
{
    public class BaseINPC : INotifyPropertyChanged 
    {

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword


        protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {

            if (Object.Equals(storage, value))
                return false;

            storage = value;

            OnPropertyChanged(propertyName);
            return true;
        }


#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        protected void OnPropertyChanged(string propertyName)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
