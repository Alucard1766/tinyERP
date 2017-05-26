using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveCharts.Helpers;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class InvoiceCreationViewModel : ViewModelBase
    {
        private double? _amount;
        private string _invoiceNumber;


        public InvoiceCreationViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public string Amount
        {
            get { return _amount.ToString(); }
            set
            {
                if (value == null)
                {
                    _amount = null;
                }
                else
                {
                    double local;
                    double.TryParse(value, out local);
                    _amount = local;
                }

                Validator.Validate(nameof(Amount));
            }
        }

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = value;
                Validator.Validate(nameof(InvoiceNumber));
            }
        }


        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRule(nameof(Amount),
                () => RuleResult.Assert(_amount > 0, "Betrag muss grösser als 0 sein"));

            Validator.AddRequiredRule(() => InvoiceNumber, "Es muss eine Rechnungsnummer eingegeben werden");
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
