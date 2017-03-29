using System.ComponentModel;

namespace tinyERP.UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Init()
        {
            // new Factory
            Load();
        }
        public abstract void Load();
    }
}
