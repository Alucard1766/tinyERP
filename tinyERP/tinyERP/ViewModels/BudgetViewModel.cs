using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class BudgetViewModel : ViewModelBase
    {
        public BudgetViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public ObservableCollection<Transaction> TransactionList { get; set; }
        public ObservableCollection<Category> CategorieList { get; set; }

        public override void Load()
        {
            var transactions = UnitOfWork.Transactions.GetTransactionsWithCategories();
            TransactionList = new ObservableCollection<Transaction>(transactions);

            var categories = UnitOfWork.Categories.GetAll();
            CategorieList = new ObservableCollection<Category>(categories);
        }

        #region New-Command

        private RelayCommand _newCommand;

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(param => New(), param => CanNew())); }
        }

        private void New()
        {
            var window = new AddTransactionView(this);
            window.ShowDialog();
            if (window.AddTransaction)
            {
                var transaction = new Transaction();
                transaction.Name = "Testeintrag";
                transaction.Amount = 200;
                transaction.Date = DateTime.Today;
                transaction.Comment = "Testdaten";
                transaction.PrivatePart = 50;
                transaction.BudgetId = 1;
                transaction.CategoryId = 1;
                UnitOfWork.Transactions.Add(transaction);
                if(UnitOfWork.Complete() > 0)
                {
                    TransactionList.Add(transaction);
                }
            }
      
        }

        private bool CanNew()
        {
            //TODO: CanNew()
            return true;
        }

        #endregion
    }
}