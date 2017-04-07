using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Transaction> TransactionList { get; set; }
        public ObservableCollection<Category> CategoryList { get; set; }

        public BudgetViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public override void Load()
        {
            var transactions = UnitOfWork.Transactions.GetTransactionsWithCategories();
            TransactionList = new ObservableCollection<Transaction>(transactions);

            var categories = UnitOfWork.Categories.GetAll();
            CategoryList = new ObservableCollection<Category>(categories);
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

                if (UnitOfWork.Complete() > 0)
                {
                    TransactionList.Add(transaction);
                }
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
            List<Transaction> selectedItems = (items as IEnumerable)?.Cast<Transaction>().ToList();
            if (selectedItems?.Count > 0 &&
                MessageBox.Show($"Wollen Sie die ausgewählten Buchungen ({selectedItems.Count}) wirklich löschen?", "Buchungen löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                UnitOfWork.Transactions.RemoveRange(selectedItems);
                UnitOfWork.Complete();

                foreach (Transaction t in selectedItems)
                {
                    TransactionList.Remove(t);
                }
            }
        }

        private bool CanDelete()
        {
            return true;
        }

        #endregion
    }
}
