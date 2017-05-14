using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LiveCharts;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class BudgetViewModel : ViewModelBase
    {
        private Budget _budget;
        private DateTime _fromDate;
        private DateTime _toDate;
        private ChartValues<double> _budgetChartValues;

        public BudgetViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public Transaction SelectedTransaction { get; set; }

        public ObservableCollection<Transaction> TransactionList { get; set; }

        public Budget Budget
        {
            get { return _budget; }
            set
            {
                SetProperty(ref _budget, value, nameof(Budget), nameof(AllExpensesTotal), nameof(AllRevenuesTotal), nameof(BudgetChartValues));

                if (Budget != null)
                    SetDatePickersToSelectedYear();
            }
        }

        public ObservableCollection<Budget> BudgetList { get; set; }

        private List<Category> CategoryList => new List<Category>(UnitOfWork.Categories.GetAll());
        
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                SetProperty(ref _fromDate, value, nameof(FromDate), nameof(BudgetChartValues));
            }
        }
        
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                SetProperty(ref _toDate, value, nameof(ToDate), nameof(BudgetChartValues));
            }
        }

        public DateTime YearStart { get; set; }

        public DateTime YearEnd { get; set; }

        public ChartValues<double> BudgetChartValues
        {
            get
            {
                _budgetChartValues.Clear();

                if (Budget != null)
                    _budgetChartValues.AddRange(CalculateCategorySums(CategoryList, Budget));

                return _budgetChartValues;
            }
            set { SetProperty(ref _budgetChartValues, value, nameof(BudgetChartValues)); }
        }

        public string[] Labels
        {
            get
            {
                List<string> categoryNames = new List<string>();
                foreach (var category in CategoryList)
                {
                    categoryNames.Add(category.Name);
                }
                return categoryNames.ToArray();
            }
        }

        public double AllExpensesTotal
        {
            get
            {
                return (Budget?.Transactions ?? new Collection<Transaction>())
                    .Where(transaction => !transaction.IsRevenue)
                    .Sum(transaction => transaction.Amount * ((100.0 - transaction.PrivatePart) / 100));
            }
        }

        public double AllRevenuesTotal
        {
            get
            {
                return (Budget?.Transactions ?? new Collection<Transaction>())
                    .Where(transaction => transaction.IsRevenue)
                    .Sum(transaction => transaction.Amount);
            }
        }

        public override void Load()
        {
            var transactions = UnitOfWork.Transactions.GetTransactionsWithCategories();
            TransactionList = new ObservableCollection<Transaction>(transactions);
            TransactionList.CollectionChanged += ContentCollectionChanged;
            var budgets = UnitOfWork.Budgets.GetAll();
            BudgetList = new ObservableCollection<Budget>(budgets);
            Budget = BudgetList.Count > 0 ? BudgetList[0] : null;
            BudgetChartValues = new ChartValues<double>();
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AllRevenuesTotal));
            OnPropertyChanged(nameof(AllExpensesTotal));
            OnPropertyChanged(nameof(BudgetChartValues));
        }

        private void SetDatePickersToSelectedYear()
        {
            var yearStart = new DateTime(Budget.Year, 1, 1);
            var yearEnd = new DateTime(Budget.Year, 12, 31);
            FromDate = yearStart;
            YearStart = yearStart;
            ToDate = yearEnd;
            YearEnd = yearEnd;
        }

        public double[] CalculateCategorySums(IEnumerable<Category> categories, Budget budget)
        {
            var sums = new List<double>();

            foreach (var category in categories)
            {
                if (category.Transactions != null)
                {
                    var sum = category.Transactions
                        .Where(t => t.Budget.Id == budget.Id && t.Date >= FromDate && t.Date <= ToDate)
                        .Sum(t => (t.IsRevenue) ? t.Amount : -(t.Amount * (100.0 - t.PrivatePart) / 100));
                    sums.Add(sum);
                }
            }

            return sums.ToArray();
        }

        #region New-Transaction-Command

        private RelayCommand _newTransactionCommand;

        public ICommand NewTransactionCommand
        {
            get { return _newTransactionCommand ?? (_newTransactionCommand = new RelayCommand(param => NewTransaction(), param => CanNewTransaction())); }
        }

        private void NewTransaction()
        {
            var transaction = new Transaction();
            var vm = new EditTransactionViewModel(new UnitOfWorkFactory(), transaction);
            vm.Init();
            var window = new EditTransactionView(vm);

            if (window.ShowDialog() ?? false)
            {
                TransactionList.Add(transaction);
            }
        }

        private bool CanNewTransaction()
        {
            return Budget != null;
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
                ContentCollectionChanged(this, null);
            }
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
            if (MessageBox.Show($"Wollen Sie die ausgewählten Buchungen ({selectedTransactions.Count}) wirklich löschen?",
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

        #region Search-Transactions-Command

        private RelayCommand _searchTransactionsCommand;

        public ICommand SearchTransactionsCommand {
            get { return _searchTransactionsCommand ?? (_searchTransactionsCommand = new RelayCommand(SearchTransactions)); }
        }

        private void SearchTransactions(object searchTerm)
        {
            var transactions = UnitOfWork.Transactions.GetTransactionsWithCategoriesFilteredBy(searchTerm as string);
            TransactionList.Clear();
            foreach (var item in transactions) { TransactionList.Add(item); }
        }

        #endregion

        #region New-Budget-Command

        private RelayCommand _newBudgetCommand;

        public ICommand NewBudgetCommand
        {
            get { return _newBudgetCommand ?? (_newBudgetCommand = new RelayCommand(param => NewBudget())); }
        }

        private void NewBudget()
        {
            var vm = new EditBudgetViewModel(new UnitOfWorkFactory());
            vm.Init();
            var window = new EditBudgetView(vm);
            window.ShowDialog();
            if (vm.NewBudget != null)
            {
                BudgetList.Add(vm.NewBudget);
                Budget = vm.NewBudget;
            }
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
            string message;

            if (Budget.Transactions?.Count > 0)
            {
                message =
                    $"Wenn sie das Budget von {Budget.Year} löschen, werden alle zugehörigen Buchungen ({Budget.Transactions.Count}) ebenfalls gelöscht. " +
                    "Wollen sie das Budget trotzdem löschen?";
            }
            else
            {
                message = $"Wollen sie das Budget von {Budget.Year} wirklich löschen?";
            }

            if (MessageBox.Show(message, "Budget löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var deletedBudget = Budget;
                var deletedTransactions = Budget.Transactions?.ToList() ?? new List<Transaction>();
                
                UnitOfWork.Transactions.RemoveRange(deletedTransactions);
                UnitOfWork.Budgets.Remove(deletedBudget);
                UnitOfWork.Complete();

                foreach (var t in deletedTransactions)
                    TransactionList.Remove(t);

                BudgetList.Remove(deletedBudget);
                Budget = BudgetList.Count > 0 ? BudgetList[0] : null;
            }
        }

        private bool CanDeleteBudget()
        {
            return Budget != null;
        }

        #endregion
    }
}
