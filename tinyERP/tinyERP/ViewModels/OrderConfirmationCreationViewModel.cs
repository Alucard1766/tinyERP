using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class OrderConfirmationCreationViewModel : ViewModelBase
    {
        private string _orderConfNumber;

        public OrderConfirmationCreationViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public string OrderConfNumber
        {
            get { return _orderConfNumber; }
            set
            {
                _orderConfNumber = value;
                Validator.Validate(nameof(OrderConfNumber));
            }
        }

        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => OrderConfNumber, "Es muss eine Auftragsbestätigungsnummer eingegeben werden");
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
                if (UnitOfWork.Complete() >= 0)
                    ((Window)window).DialogResult = true;

                ((Window)window).Close();
            }
        }

        #endregion
    }
}
