using System.ComponentModel;

namespace tinyERP.UI.ViewModels
{
    public abstract class ViewModelBase
    {
        public void Init()
        {
            // new Factory
            Load();
        }
        public abstract void Load();
    }
}
