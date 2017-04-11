using System;
using System.Collections;
using System.ComponentModel;
using MvvmValidation;
using tinyERP.Dal;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    public abstract class ViewModelBase : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private readonly IUnitOfWorkFactory _factory;
        protected IUnitOfWork UnitOfWork { get; set; }
        protected ValidationHelper Validator { get;}
        private NotifyDataErrorInfoAdapter NotifyDataErrorInfoAdapter { get;}


        protected ViewModelBase(IUnitOfWorkFactory factory)
        {
            this._factory = factory;

            Validator = new ValidationHelper();
            NotifyDataErrorInfoAdapter = new NotifyDataErrorInfoAdapter(Validator);
            NotifyDataErrorInfoAdapter.ErrorsChanged += OnErrorChanged;
        }

        private void OnErrorChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(sender, e.PropertyName);
        }

        public void OnPropertyChanged(object sender, string name)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return NotifyDataErrorInfoAdapter.GetErrors(propertyName);
        }

        public bool HasErrors => NotifyDataErrorInfoAdapter.HasErrors;
   

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { NotifyDataErrorInfoAdapter.ErrorsChanged += value; }
            remove { NotifyDataErrorInfoAdapter.ErrorsChanged -= value; }
        }
        ~ViewModelBase()
        {
            UnitOfWork?.Dispose();
        }

        public void Init()
        {
            UnitOfWork = _factory.GetUnitOfWork();
            Load();
        }

        public abstract void Load();
        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
