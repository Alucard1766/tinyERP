using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class AddTransactionViewModel : ViewModelBase
    {
        public AddTransactionViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public ObservableCollection<Category> CategoryList { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; } = 0.0;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        public int PrivatPart { get; set; }
        public Category SelectedCategory { get; set; }
        public bool CreateTransaction { get; set; }

        public override void Load()
        {
            CreateTransaction = false;
            var categories = UnitOfWork.Categories.GetAll();
            CategoryList = new ObservableCollection<Category>(categories);
        }

        #region New-Command

        private RelayCommand _newCommand;

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(New, param => CanNew())); }
        }

        private void New(object window)
        {
            if (Name?.Length > 0 && Amount > 0 && Date != null && PrivatPart >= 0 && SelectedCategory != null)
            {
                CreateTransaction = true;
                ((Window) window).Close();
            }
        }

        private bool CanNew()
        {
            return true;
        }

        #endregion
    }
}