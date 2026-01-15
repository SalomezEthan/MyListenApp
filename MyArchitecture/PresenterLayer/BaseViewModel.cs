using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyArchitecture.PresenterLayer
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected void SetValue<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                OnPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
