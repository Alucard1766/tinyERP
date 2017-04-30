using System;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class EditOrderViewModel : ViewModelBase
    {
        private Order order;

        public EditOrderViewModel(IUnitOfWorkFactory factory, Order order) : base(factory)
        {
            this.order = order;
            OrderNumber = order.OrderNumber;
            SelectedState = order.State;
            CreationDate = order.CreationDate;
            StateModificationDate = order.StateModificationDate;
        }

        public string OrderNumber { get; }

        public State SelectedState { get; set; }

        public DateTime CreationDate { get; }

        public DateTime StateModificationDate { get; }

        //public ObservableCollection<Customer> CustomerList { get; set; }

        //public Customer SelectedCustomer { get; set; }

        public override void Load()
        {
            //var customers = UnitOfWork.Customers.GetAll();
            //CustomerList = new ObservableCollection<Customer>(customers);
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => SelectedState, "Status ist notwendig");
        }

        #region Save-Command

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save, param => CanSave())); }
        }

        private void Save(object window)
        {
            if (order.State != SelectedState)
            {
                order.StateModificationDate = DateTime.Today;
                order.State = SelectedState;
            }

            if (order.Id == 0)
                order = UnitOfWork.Orders.Add(order);

            if (UnitOfWork.Complete() > 0)
                ((Window)window).DialogResult = true;

            ((Window)window).Close();
        }

        private bool CanSave()
        {
            return Validator.ValidateAll().IsValid;
        }

        #endregion
    }
}
