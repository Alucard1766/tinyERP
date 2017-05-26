using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;
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

        public string OrderConfToolTip => OrderConfirmationList.Count <= 0
            ? "Neue Auftragsbestätigung"
            : "Es kann nur eine Auftragsbestätigung erfasst werden";

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
                FileAccess.Open((string)fileName, FileType.Document);
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

        #region NewInvoice-Command

        private RelayCommand _newInvoiceCommand;

        public ICommand NewInvoiceCommand
        {
            get { return _newInvoiceCommand ?? (_newInvoiceCommand = new RelayCommand(NewInvoice)); }
        }

        private void NewInvoice(object window)
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

                var vm = new InvoiceCreationViewModel(new UnitOfWorkFactory());
                vm.Init();
                var windowView = new InvoiceCreationView(vm);

                if (windowView.ShowDialog() ?? false)
                {
                    var invoiceNumber = vm.InvoiceNumber;
                    var amount = double.Parse(vm.Amount);
                    string fileName;
                    try
                    {
                        fileName = FileAccess.CreateNewInvoice(order.Customer, invoiceNumber);
                    }
                    catch (Win32Exception)
                    {
                        MessageBox.Show("Das Dokument konnte nicht erstellt werden. Eventuell haben Sie das Template noch geöffnet.", "Ein Fehler ist aufgetreten");
                        return;
                    }
                    

                    var document = new Document
                    {
                        IssueDate = DateTime.Now,
                        Name = invoiceNumber,
                        Tag = OrderNumber,
                        RelativePath = fileName
                    };

                    document = UnitOfWork.Documents.Add(document);

                    var invoice = new Invoice
                    {
                        Amount = amount,
                        InvoiceNumber = invoiceNumber,
                        IsPayed = false,
                        Order = order,
                        Document = document
                    };

                    invoice = UnitOfWork.Invoices.Add(invoice);

                    UnitOfWork.Complete();

                    InvoiceList.Add(invoice);
                }
            }
        }

        #endregion

        #region InvoicePayed-Command

        private RelayCommand _invoicePayedCommand;

        public ICommand InvoicePayedCommand
        {
            get { return _invoicePayedCommand ?? (_invoicePayedCommand = new RelayCommand(InvoicePayed)); }
        }

        private void InvoicePayed(object invoiceItem)
        {
            var budget = UnitOfWork.Budgets.GetBudgetByYear(DateTime.Today);

            if (budget == null)
            {
                MessageBox.Show(
                    "Die Rechnung kann nicht als bezahlt markiert werden, da noch kein Budget zu diesem Jahr erfasst wurde.",
                    "Transaktion kann nicht erstellt werden", MessageBoxButton.OK);
                return;
            }

            var vm = new CategorySelectionViewModel(new UnitOfWorkFactory());
            vm.Init();
            var windowView = new CategorySelectionView(vm);


            if (windowView.ShowDialog() ?? false)
            {
                var category = vm.SelectedCategory;
                Invoice invoice = (Invoice)invoiceItem;
                invoice.IsPayed = true;
                CollectionViewSource.GetDefaultView(InvoiceList).Refresh();

                var transaction = new Transaction
                {
                    Name = "Rechnung zu Auftrag " + order.OrderNumber,
                    Amount = invoice.Amount,
                    Date = DateTime.Today,
                    Document = invoice.Document,
                    Category = category,
                    Budget = UnitOfWork.Budgets.GetBudgetByYear(DateTime.Today)
                };

                UnitOfWork.Transactions.Add(transaction);

                UnitOfWork.Complete();
            }
        }

        #endregion

        #region NewOffer-Command

        private RelayCommand _newOfferCommand;

        public ICommand NewOfferCommand
        {
            get { return _newOfferCommand ?? (_newOfferCommand = new RelayCommand(NewOffer)); }
        }

        private void NewOffer(object window)
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

                var vm = new OfferCreationViewModel(new UnitOfWorkFactory());
                vm.Init();
                var windowView = new OfferCreationView(vm);

                if (windowView.ShowDialog() ?? false)
                {
                    var offerNumber = vm.OfferNumber;
                    string fileName;
                    try
                    {
                        fileName = FileAccess.CreateNewOffer(order.Customer, offerNumber);
                    }
                    catch (Win32Exception)
                    {
                        MessageBox.Show("Das Dokument konnte nicht erstellt werden. Eventuell haben Sie das Template noch geöffnet.", "Ein Fehler ist aufgetreten");
                        return;
                    }
                    
                    var document = new Document
                    {
                        IssueDate = DateTime.Now,
                        Name = offerNumber,
                        Tag = OrderNumber,
                        RelativePath = fileName
                    };

                    document = UnitOfWork.Documents.Add(document);

                    var offer = new Offer
                    {
                        OfferNumber = offerNumber,
                        Order = order,
                        Document = document
                    };

                    offer = UnitOfWork.Offers.Add(offer);

                    UnitOfWork.Complete();

                    OfferList.Add(offer);
                }
            }
        }

        #endregion

        #region NewOrderConfirmation-Command

        private RelayCommand _newOrderConfirmationCommand;

        public ICommand NewOrderConfirmationCommand
        {
            get { return _newOrderConfirmationCommand ?? (_newOrderConfirmationCommand = new RelayCommand(NewOrderConfirmation, CanMakeNewOrder)); }
        }

        private void NewOrderConfirmation(object window)
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

                var vm = new OrderConfirmationCreationViewModel(new UnitOfWorkFactory());
                vm.Init();
                var windowView = new OrderConfirmationCreationView(vm);

                if (windowView.ShowDialog() ?? false)
                {
                    var orderConfNumber = vm.OrderConfNumber;
                    string fileName;

                    try
                    {
                        fileName = FileAccess.CreateNewOrderConfirmation(order.Customer, orderConfNumber);
                    }
                    catch (Win32Exception)
                    {
                        MessageBox.Show("Das Dokument konnte nicht erstellt werden. Eventuell haben Sie das Template noch geöffnet.", "Ein Fehler ist aufgetreten");
                        return;
                    }
                    
                    var document = new Document
                    {
                        IssueDate = DateTime.Now,
                        Name = orderConfNumber,
                        Tag = OrderNumber,
                        RelativePath = fileName
                    };

                    document = UnitOfWork.Documents.Add(document);

                    var orderConfirmation = new OrderConfirmation
                    {
                        OrderConfNumber = orderConfNumber,
                        Order = order,
                        Document = document
                    };

                    orderConfirmation = UnitOfWork.OrderConfirmations.Add(orderConfirmation);

                    UnitOfWork.Complete();

                    OrderConfirmationList.Add(orderConfirmation);
                }
            }
        }

        private bool CanMakeNewOrder(object window)
        {
            return OrderConfirmationList.Count <= 0;
        }

        #endregion
    }
}
