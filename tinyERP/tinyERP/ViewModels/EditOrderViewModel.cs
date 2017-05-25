using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;
using tinyERP.UI.Factories;
using FileAccess = tinyERP.Dal.FileAccess;

namespace tinyERP.UI.ViewModels
{
    internal class EditOrderViewModel : ViewModelBase
    {
        private Order order;

        private string _title;

        public EditOrderViewModel(IUnitOfWorkFactory factory, Order order) : base(factory)
        {
            this.order = order;
            OrderNumber = this.order.OrderNumber;
            Title = this.order.Title;
            SelectedState = this.order.State;
            CreationDate = this.order.CreationDate;
            StateModificationDate = this.order.StateModificationDate;
            SelectedCustomer = this.order.Customer;
        }

        public string OrderNumber { get; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Validator.Validate(nameof(Title));
            }
        }

        public State SelectedState { get; set; }

        public DateTime CreationDate { get; }

        public DateTime StateModificationDate { get; }

        public ObservableCollection<Customer> CustomerList { get; set; }

        public Customer SelectedCustomer { get; set; }

        public ObservableCollection<Offer> OfferList { get; set; }

        public ObservableCollection<Invoice> InvoiceList { get; set; }

        public ObservableCollection<OrderConfirmation> OrderConfirmationList { get; set; }

        public override void Load()
        {
            var customers = UnitOfWork.Customers.GetAll();
            CustomerList = new ObservableCollection<Customer>(customers);
            var offers = UnitOfWork.Orders.GetOffersAndDocumentsByOrderId(order.Id);
            OfferList = new ObservableCollection<Offer>(offers);
            var invoices = UnitOfWork.Orders.GetInvoicesAndDocumentsByOrderId(order.Id);
            InvoiceList = new ObservableCollection<Invoice>(invoices);
            var orderConfirmations = UnitOfWork.Orders.GetOrderConfirmationWithDocumentByOrderId(order.Id);
            OrderConfirmationList = new ObservableCollection<OrderConfirmation>(orderConfirmations);
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => Title, "Bezeichnung ist notwendig");
            Validator.AddRequiredRule(() => SelectedCustomer, "Kunde ist notwendig");
        }

        #region Save-Command

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        private void Save(object window)
        {
            if (Validator.ValidateAll().IsValid)
            {
                if (order.State != SelectedState)
                {
                    order.StateModificationDate = DateTime.Today;
                    order.State = SelectedState;
                }

                order.Title = Title;
                order.Customer = SelectedCustomer;

                if (order.Id == 0)
                    order = UnitOfWork.Orders.Add(order);

                if (UnitOfWork.Complete() > 0)
                    ((Window) window).DialogResult = true;

                ((Window) window).Close();
            }
        }

        #endregion

        #region OpenDocument-Command

        private RelayCommand _openDocumentCommand;

        public ICommand OpenDocumentCommand
        {
            get { return _openDocumentCommand ?? (_openDocumentCommand = new RelayCommand(Open)); }
        }

        private void Open(object fileName)
        {
            const string fnfMessage = "Das gesuchte Dokument konnte nicht gefunden werden.";
            const string title = "Ein Fehler ist aufgetreten";
            try
            {
                FileAccess.Open((string)fileName);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(fnfMessage, title);
            }
            catch (Win32Exception)
            {
                MessageBox.Show(fnfMessage, title);
            }

        }

        #endregion
    }
}
