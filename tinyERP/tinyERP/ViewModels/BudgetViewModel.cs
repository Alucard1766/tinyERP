using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        public Transaction SelectedTransaction { get; set; }

        public ObservableCollection<Transaction> TransactionList { get; set; }

        public ObservableCollection<Budget> BudgetList { get; set; }

        private Budget _budget;

        public Budget Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value, nameof(Budget), nameof(AllExpensesTotal), nameof(AllRevenuesTotal)); }
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
                    if (!transaction.IsRevenue)
                    {
                        result += transaction.Amount;
                    }
                }
                return result;
            }
        }

        public double AllRevenuesTotal
        {
            get
            {
                double result = 0.0;
                var expenses = UnitOfWork.Transactions.GetTransactionsBetween(new DateTime(Budget.Year, 1, 1),
                    new DateTime(Budget.Year, 12, 31));
                foreach (var transaction in expenses)
                {
                    if (transaction.IsRevenue)
                    {
                        result += transaction.Amount;
                    }
                }
                return result;
            }
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AllRevenuesTotal));
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

        #region New-Transaction-Command

        private RelayCommand _newTransactionCommand;

        public ICommand NewTransactionCommand
        {
            get { return _newTransactionCommand ?? (_newTransactionCommand = new RelayCommand(param => NewTransaction(), param => CanNewTransaction())); }
        }

        private void NewTransaction()
        {
            var transaction = new Transaction()
            {
                IsRevenue = true,
                Date = DateTime.Today
            };
            var vm = new EditTransactionViewModel(new UnitOfWorkFactory(), transaction);
            vm.Init();
            var window = new EditTransactionView(vm);

            if (window.ShowDialog() ?? false)
            {
                TransactionList.Add(transaction);
            }
            //TODO: Clean ViewModel after closing window?
        }

        private bool CanNewTransaction()
        {
            return true;
        }

        #endregion

        #region Edit-Transaction-Command

        private RelayCommand _editTransactionCommand;

        public ICommand EditTransactionCommand
        {
            get { return _editTransactionCommand ?? (_editTransactionCommand = new RelayCommand(EditTransaction, param => CanEditTransaction())); }
        }

        private void EditTransaction(object dataGrid)
        {
            var vm = new EditTransactionViewModel(new UnitOfWorkFactory(), SelectedTransaction);
            vm.Init();
            var window = new EditTransactionView(vm);

            if (window.ShowDialog() ?? false)
            {
                (dataGrid as DataGrid)?.Items.Refresh();
                OnPropertyChanged(nameof(AllRevenuesTotal));
                OnPropertyChanged(nameof(AllExpensesTotal));
            }
            //TODO: Clean ViewModel after closing window?
        }

        private bool CanEditTransaction()
        {
            return SelectedTransaction != null;
        }

        #endregion

        #region Delete-Transactions-Command

        private RelayCommand _deleteTransactionsCommand;

        public ICommand DeleteTransactionsCommand
        {
            get { return _deleteTransactionsCommand ?? (_deleteTransactionsCommand = new RelayCommand(DeleteTransactions, CanDeleteTransactions)); }
        }

        private void DeleteTransactions(object selectedItems)
        {
            var selectedTransactions = (selectedItems as IEnumerable)?.Cast<Transaction>().ToList();
            if (selectedTransactions?.Count > 0 &&
                MessageBox.Show($"Wollen Sie die ausgewählten Buchungen ({selectedTransactions.Count}) wirklich löschen?",
                    "Buchungen löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                UnitOfWork.Transactions.RemoveRange(selectedTransactions);
                UnitOfWork.Complete();

                foreach (var t in selectedTransactions)
                    TransactionList.Remove(t);
            }
        }

        private bool CanDeleteTransactions(object selectedItems)
        {
            return (selectedItems as ICollection)?.Count > 0;
        }

        #endregion


        #region New-Budget-Command

        private RelayCommand _newBudgetCommand;

        public ICommand NewBudgetCommand
        {
            get { return _newBudgetCommand ?? (_newBudgetCommand = new RelayCommand(param => NewBudget(), param => CanNewBudget())); }
        }

        private void NewBudget()
        {
            var vm = new DetailedBudgetViewModel(new UnitOfWorkFactory());
            vm.Init();
            var window = new DetailedBudgetView(vm);
            window.ShowDialog();
            if (vm.NewBudget != null)
            {
                BudgetList.Add(vm.NewBudget);
                Budget = vm.NewBudget;
            }
            //TODO: Clean ViewModel after closing window?
        }

        private bool CanNewBudget()
        {
            return true;
        }

        #endregion

        #region Delete-Budget-Command

        private RelayCommand _deleteBudgetCommand;

        public ICommand DeleteBudgetCommand
        {
            get { return _deleteBudgetCommand ?? (_deleteBudgetCommand = new RelayCommand(param => DeleteBudget(), param => CanDeleteBudget())); }
        }

        private void DeleteBudget()
        {
            if (Budget.Transactions?.Count > 0 &&
                MessageBox.Show(
                    $"Wenn sie das Budget von {Budget.Year} löschen, werden alle zugehörigen Buchungen ({Budget.Transactions.Count}) ebenfalls gelöscht. " +
                    $"Wollen sie das Budget trotzdem löschen?",
                    "Budget löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var deletedBudget = Budget;
                var deletedTransactions = Budget.Transactions.ToList();
                
                UnitOfWork.Transactions.RemoveRange(deletedTransactions);
                UnitOfWork.Budgets.Remove(deletedBudget);
                UnitOfWork.Complete();

                foreach (var t in deletedTransactions)
                    TransactionList.Remove(t);

                Budget = BudgetList[0]; //TODO: What if DB/List empty?
                BudgetList.Remove(deletedBudget);
            }
        }

        private bool CanDeleteBudget()
        {
            return Budget != null;
        }

        #endregion
    }
}