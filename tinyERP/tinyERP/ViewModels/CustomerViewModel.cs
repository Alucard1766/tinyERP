using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class CustomerViewModel : ViewModelBase
    {
        public Customer SelectedCustomer { get; set; }

        public CustomerViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public ObservableCollection<Customer> CustomerList { get; set; }

        public override void Load()
        {
            var customers = UnitOfWork.Customers.GetAll();
            CustomerList = new ObservableCollection<Customer>(customers);
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
            get { return _editCustomerCommand ?? (_editCustomerCommand = new RelayCommand(EditCustomer, param => CanEditCustomer())); }
        }

        private void EditCustomer(object dataGrid)
        {
            var vm = new EditCustomerViewModel(new UnitOfWorkFactory(), SelectedCustomer);
            vm.Init();
            var window = new EditCustomerView(vm);

            if (window.ShowDialog() ?? false)
            {
                (dataGrid as DataGrid)?.Items.Refresh();
            }
        }

        private bool CanEditCustomer()
        {
            return SelectedCustomer != null;
        }

        #endregion

        #region Delete-Customer-Command

        private RelayCommand _deleteCustomersCommand;

        public ICommand DeleteCustomersCommand
        {
            get { return _deleteCustomersCommand ?? (_deleteCustomersCommand = new RelayCommand(DeleteCustomers, CanDeleteCustomers)); }
        }

        private void DeleteCustomers(object selectedItems)
        {
            var selectedCustomers = (selectedItems as IEnumerable)?.Cast<Customer>().ToList();
            if (MessageBox.Show($"Wollen Sie die ausgewählten Buchungen ({selectedCustomers.Count}) wirklich löschen?",
                    "Buchungen löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
    }
}
