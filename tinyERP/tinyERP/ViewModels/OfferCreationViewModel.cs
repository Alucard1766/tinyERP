using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class OfferCreationViewModel : ViewModelBase
    {
        private string _offerNumber;

        public OfferCreationViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public string OfferNumber
        {
            get { return _offerNumber; }
            set
            {
                _offerNumber = value;
                Validator.Validate(nameof(OfferNumber));
            }
        }

        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => OfferNumber, "Es muss eine Offertennummer eingegeben werden");
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
