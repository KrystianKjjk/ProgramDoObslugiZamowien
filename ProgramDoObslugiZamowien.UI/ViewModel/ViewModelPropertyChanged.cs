using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProgramDoObslugiZamowien.UI.ViewModel
{
    public class ViewModelPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
