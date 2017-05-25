using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        private ObservableCollection<Transaction> _transactionList;
        private ObservableCollection<Budget> _budgetList;
        private DateTime _fromDate, _toDate, _yearStart, _yearEnd;
        private ChartValues<double> _budgetChartValues;

        public BudgetViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public Transaction SelectedTransaction { get; set; }

        public ObservableCollection<Transaction> TransactionList
        {
            get { return _transactionList; }
            set
            {
                _transactionList = value;
                OnPropertyChanged(nameof(TransactionList));
            }
        }

        public ObservableCollection<Budget> BudgetList
        {
            get { return _budgetList; }
            set
            {
                _budgetList = value;
                OnPropertyChanged(nameof(BudgetList));
            }
        }

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


        private List<Category> CategoryList => new List<Category>(UnitOfWork.Categories.GetAll());
        
        public DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                SetProperty(ref _fromDate, value, nameof(FromDate), nameof(BudgetChartValues), nameof(AllExpensesTotal), nameof(AllRevenuesTotal));
            }
        }
        
        public DateTime ToDate
        {
            get { return _toDate; }
            set
            {
                SetProperty(ref _toDate, value, nameof(ToDate), nameof(BudgetChartValues), nameof(AllExpensesTotal), nameof(AllRevenuesTotal));
            }
        }

        public DateTime YearStart
        {
            get { return _yearStart; }
            set { SetProperty(ref _yearStart, value, nameof(YearStart)); }
        }

        public DateTime YearEnd
        {
            get { return _yearEnd; }
            set { SetProperty(ref _yearEnd, value, nameof(YearEnd)); }
        }

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
                    .Where(transaction => DateTime.Compare(transaction.Date, FromDate) >= 0 && DateTime.Compare(transaction.Date, ToDate) <= 0)
                    .Sum(transaction => transaction.Amount * ((100.0 - transaction.PrivatePart) / 100));
            }
        }

        public double AllRevenuesTotal
        {
            get
            {
                return (Budget?.Transactions ?? new Collection<Transaction>())
                    .Where(transaction => transaction.IsRevenue)
                    .Where(transaction => DateTime.Compare(transaction.Date, FromDate) >= 0 && DateTime.Compare(transaction.Date, ToDate) <= 0)
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
            CollectionViewSource.GetDefaultView(TransactionList).SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
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
            var sums = new Dictionary<Category, double>();

            foreach (var category in categories)
            {
                if (category.Transactions != null)
                {
                    var sum = category.Transactions
                        .Where(t => t.Budget.Id == budget.Id && t.Date >= FromDate && t.Date <= ToDate)
                        .Sum(t => (t.IsRevenue) ? t.Amount : -(t.Amount * (100.0 - t.PrivatePart) / 100));
                    if (category.ParentCategory == null)
                    {
                        if (sums.ContainsKey(category))
                        {
                            sums[category] += sum;
                        }
                        else
                        {
                            sums.Add(category, sum);
                        }
                    }
                    else
                    {
                        if (sums.ContainsKey(category.ParentCategory))
                        {
                            sums[category.ParentCategory] += sum;
                        }
                        else
                        {
                            sums.Add(category.ParentCategory, sum);
                        }
                    }
                }
            }

            return sums.Values.ToArray();
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
            get { return _editTransactionCommand ?? (_editTransactionCommand = new RelayCommand(EditTransaction, CanEditTransaction)); }
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

        private bool CanEditTransaction(object dataGrid)
        {
            return (dataGrid as DataGrid)?.SelectedItems.Count == 1;
        }

        #endregion

        #region Delete-Transactions-Command

        private RelayCommand _deleteTransactionsCommand;

        public ICommand DeleteTransactionsCommand
        {
            get { return _deleteTransactionsCommand ?? (_deleteTransactionsCommand = new RelayCommand(DeleteTransactions, CanDeleteTransactions)); }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")] //null-reference tested in CanDeleteTransactions-method
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
            var budget = new Budget();
            var vm = new EditBudgetViewModel(new UnitOfWorkFactory(), budget);
            vm.Init();
            var window = new EditBudgetView(vm);

            if (window.ShowDialog() ?? false)
            {
                BudgetList.Add(budget);
                Budget = budget;
            }
        }

        #endregion

        #region Edit-Budget-Command

        private RelayCommand _editBudgetCommand;

        public ICommand EditBudgetCommand
        {
            get { return _editBudgetCommand ?? (_editBudgetCommand = new RelayCommand(param => EditBudget(), param => CanEditBudget())); }
        }

        private void EditBudget()
        {
            var vm = new EditBudgetViewModel(new UnitOfWorkFactory(), Budget);
            vm.Init();
            var window = new EditBudgetView(vm);

            if (window.ShowDialog() ?? false)
            {
                OnPropertyChanged(nameof(Budget));
            }
        }

        private bool CanEditBudget()
        {
            return Budget != null;
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
