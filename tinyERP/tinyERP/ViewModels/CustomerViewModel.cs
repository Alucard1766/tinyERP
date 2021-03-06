﻿using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
    {
        private Customer _selectedCustomer;
        private ObservableCollection<Customer> _customerList;
        private string _searchTerm;

        public CustomerViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public Customer SelectedCustomer {
            get { return _selectedCustomer; }
            set { SetProperty(ref _selectedCustomer, value, nameof(SelectedCustomer)); }
        }

        public ObservableCollection<Customer> CustomerList
        {
            get { return _customerList; }
            set { SetProperty(ref _customerList, value, nameof(CustomerList)); }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value, nameof(SearchTerm)); }
        }

        public override void Load()
        {
            SearchTerm = string.Empty;
            var customers = UnitOfWork.Customers.GetAll();
            CustomerList = new ObservableCollection<Customer>(customers);
            CollectionViewSource.GetDefaultView(CustomerList).SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
        }

        #region New-Customer-Command

        private RelayCommand _newCustomerCommand;

        public ICommand NewCustomerCommand
        {
            get { return _newCustomerCommand ?? (_newCustomerCommand = new RelayCommand(param => NewCustomer())); }
        }

        private void NewCustomer()
        {
            var customer = new Customer();
            var vm = new EditCustomerViewModel(new UnitOfWorkFactory(), customer);
            vm.Init();
            var window = new EditCustomerView(vm);

            if (window.ShowDialog() ?? false)
            {
                CustomerList.Add(customer);
            }
        }

        #endregion

        #region Edit-Customer-Command

        private RelayCommand _editCustomerCommand;

        public ICommand EditCustomerCommand
        {
            get { return _editCustomerCommand ?? (_editCustomerCommand = new RelayCommand(param => EditCustomer(), CanEditCustomer)); }
        }

        private void EditCustomer()
        {
            var vm = new EditCustomerViewModel(new UnitOfWorkFactory(), SelectedCustomer);
            vm.Init();
            var window = new EditCustomerView(vm);

            if (window.ShowDialog() ?? false)
            {
                CollectionViewSource.GetDefaultView(CustomerList).Refresh();
            }
        }

        private bool CanEditCustomer(object selectedItems)
        {
            return (selectedItems as ICollection)?.Count == 1;
        }

        #endregion

        #region Delete-Customer-Command

        private RelayCommand _deleteCustomersCommand;

        public ICommand DeleteCustomersCommand
        {
            get { return _deleteCustomersCommand ?? (_deleteCustomersCommand = new RelayCommand(DeleteCustomers, CanDeleteCustomers)); }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")] //null-reference tested in CanDeleteCustomers-method
        private void DeleteCustomers(object selectedItems)
        {
            var selectedCustomers = (selectedItems as IEnumerable)?.Cast<Customer>().ToList();
            if (MessageBox.Show($"Wollen Sie die ausgewählten Kunden ({selectedCustomers.Count}) wirklich löschen?",
                    "Kunde löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                UnitOfWork.Customers.RemoveRange(selectedCustomers);
                UnitOfWork.Complete();

                foreach (var t in selectedCustomers)
                    CustomerList.Remove(t);
            }
        }

        private bool CanDeleteCustomers(object selectedItems)
        {
            return (selectedItems as ICollection)?.Count > 0;
        }

        #endregion

        #region Search-Customers-Command

        private RelayCommand _searchCustomersCommand;

        public ICommand SearchCustomersCommand
        {
            get { return _searchCustomersCommand ?? (_searchCustomersCommand = new RelayCommand(param => SearchCustomers())); }
        }

        private void SearchCustomers()
        {
            var customers = UnitOfWork.Customers.Find(c => c.FirstName.Contains(SearchTerm) || c.LastName.Contains(SearchTerm));
            CustomerList.Clear();
            foreach (var item in customers) { CustomerList.Add(item); }
        }
        
        #endregion
    }
}
