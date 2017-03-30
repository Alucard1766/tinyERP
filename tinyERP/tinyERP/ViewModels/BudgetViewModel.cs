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
            var transactions = UnitOfWork.Transactions.GetAll();
            TransactionList = new ObservableCollection<Transaction>(transactions);

            var categories = UnitOfWork.Categories.GetAll();
            CategorieList = new ObservableCollection<Category>(categories);
        }

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get { return newCommand ?? (newCommand = new RelayCommand(param => New(), param => CanNew())); }
        }

        private void New()
        {
            var window = new AddTransactionView(this);
            window.ShowDialog();
      
        }

        private bool CanNew()
        {
            //TODO: CanNew()
            return true;
        }

        #endregion
    }
}