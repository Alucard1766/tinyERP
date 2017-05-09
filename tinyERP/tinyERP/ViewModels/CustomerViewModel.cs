using System.Collections.ObjectModel;
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

        #region Search-Customers-Command

        private RelayCommand _searchCustomersCommand;

        public ICommand SearchCustomersCommand
        {
            get { return _searchCustomersCommand ?? (_searchCustomersCommand = new RelayCommand(SearchCustomers)); }
        }

        private void SearchCustomers(object searchTerm)
        {
            var st = searchTerm as string;
            var customers = UnitOfWork.Customers.Find(c => c.FirstName.Contains(st) || c.LastName.Contains(st));
            CustomerList.Clear();
            foreach (var item in customers) { CustomerList.Add(item); }
        }

        #endregion
    }
}
