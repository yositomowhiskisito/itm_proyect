using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibUtilities.Core
{
    public abstract class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            try
            {
                if (object.Equals(storage, value))
                    return false;

                storage = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                var eventHandler = this.PropertyChanged;
                if (eventHandler == null)
                    return;
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception)
            {

            }
        }
    }
}