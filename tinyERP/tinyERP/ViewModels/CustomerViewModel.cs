using System.Collections.ObjectModel;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

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
            get { return _newCustomerCommand ?? (_newCustomerCommand = new RelayCommand(param => NewCustomer(), param => CanNewCustomer())); }
        }

        private void NewCustomer()
        {
            //var transaction = new Transaction()
            //{
            //    IsRevenue = true,
            //    Date = DateTime.Today
            //};
            //var vm = new EditTransactionViewModel(new UnitOfWorkFactory(), transaction);
            //vm.Init();
            //var window = new EditTransactionView(vm);

            //if (window.ShowDialog() ?? false)
            //{
            //    CustomerList.Add(customer);
            //}
        }

        private bool CanNewCustomer()
        {
            return true;
        }

        #endregion

    }
}
