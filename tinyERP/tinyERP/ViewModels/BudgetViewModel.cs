using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
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

        public ObservableCollection<Budget> BudgetList { get; set; }

        private Budget _budget;

        public Budget Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value, nameof(Budget), nameof(AllExpensesTotal), nameof(AllRevenueTotal)); }
        }


        public double AllExpensesTotal
        {
            get
            {
                double result = 0.0;
                var expenses = UnitOfWork.Transactions.GetTransactionsBetween(new DateTime(Budget.Year, 1, 1),
                    new DateTime(Budget.Year, 12, 31));
                foreach (var transaction in expenses)
                {
                    if (!transaction.IsEarning)
                    {
                        result += transaction.Amount;
                    }
                }
                return result;
            }
        }

        public double AllRevenueTotal
        {
            get
            {
                double result = 0.0;
                var expenses = UnitOfWork.Transactions.GetTransactionsBetween(new DateTime(Budget.Year, 1, 1),
                    new DateTime(Budget.Year, 12, 31));
                foreach (var transaction in expenses)
                {
                    if (transaction.IsEarning)
                    {
                        result += transaction.Amount;
                    }
                }
                return result;
            }
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AllRevenueTotal));
            OnPropertyChanged(nameof(AllExpensesTotal));
        }

        public override void Load()
        {
            var transactions = UnitOfWork.Transactions.GetTransactionsWithCategories();
            TransactionList = new ObservableCollection<Transaction>(transactions);
            TransactionList.CollectionChanged += ContentCollectionChanged;
            var budgets = UnitOfWork.Budgets.GetAll();
            BudgetList = new ObservableCollection<Budget>(budgets);
            Budget = BudgetList[0]; //TODO: What if DB empty?
        }

        #region New-Command

        private RelayCommand _newCommand;

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(param => New(), param => CanNew())); }
        }

        private void New()
        {
            var vm = new AddTransactionViewModel(new UnitOfWorkFactory());
            vm.Init();
            var window = new AddTransactionView(vm);
            window.ShowDialog();

            if (vm.CreateTransaction)
            {
                var transaction = new Transaction();
                transaction.Name = vm.Name;
                transaction.Amount = vm.Amount ?? 0;
                transaction.Date = vm.Date;
                transaction.Comment = vm.Comment;
                transaction.PrivatePart = vm.PrivatPart;
                transaction.BudgetId = 1;
                transaction.CategoryId = vm.SelectedCategory.Id;
                UnitOfWork.Transactions.Add(transaction);

                if (UnitOfWork.Complete() > 0)
                    TransactionList.Add(transaction);
            }
        }

        private bool CanNew()
        {
            return true;
        }

        #endregion

        #region Delete-Command

        private RelayCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, param => CanDelete())); }
        }

        private void Delete(object items)
        {
            var selectedItems = (items as IEnumerable)?.Cast<Transaction>().ToList();
            if (selectedItems?.Count > 0 &&
                MessageBox.Show($"Wollen Sie die ausgewählten Buchungen ({selectedItems.Count}) wirklich löschen?",
                    "Buchungen löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                UnitOfWork.Transactions.RemoveRange(selectedItems);
                UnitOfWork.Complete();

                foreach (var t in selectedItems)
                    TransactionList.Remove(t);
            }
        }

        private bool CanDelete()
        {
            return true;
        }

        #endregion
    }
}