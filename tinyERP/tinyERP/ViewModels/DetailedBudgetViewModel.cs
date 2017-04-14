using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class DetailedBudgetViewModel : ViewModelBase
    {
        private double? _revenue;
        private double? _expense;
        private int? _year;

        public DetailedBudgetViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }


        public string Year
        {
            get { return _year.ToString(); }
            set
            {
                if (value == null)
                {
                    _year = null;
                }
                else
                {
                    int local;
                    int.TryParse(value, out local);
                    _year = local;
                }
                OnPropertyChanged(nameof(Year));
                Validator.Validate(nameof(Year));
            }
        }

        public string Revenue
        {
            get { return _revenue.ToString(); }
            set
            {
                if (value == null)
                {
                    _revenue = null;
                }
                else
                {
                    double local;
                    double.TryParse(value, out local);
                    _revenue = local;
                }

                OnPropertyChanged(nameof(Revenue));
                Validator.Validate(nameof(Revenue));
            }
        }

        public string Expense
        {
            get { return _expense.ToString(); }
            set
            {
                if (value == null)
                {
                    _expense = null;
                }
                else
                {
                    double local;
                    double.TryParse(value, out local);
                    _expense = local;
                }

                OnPropertyChanged(nameof(Expense));
                Validator.Validate(nameof(Expense));
            }
        }

        public Budget NewBudget { get; set; }

        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRule(nameof(Year), () => RuleResult.Assert(_year != null && _year > 1900, "Das Jahr muss grösser sein als 1900"));
            Validator.AddRule(nameof(Revenue),
                () => RuleResult.Assert(_revenue != null && _revenue > 0, "Die Einnahmen müssen positiv sein"));
            Validator.AddRule(nameof(Expense),
                () => RuleResult.Assert(_expense != null && _expense > 0, "Die Ausgaben müssen positiv sein"));
            Validator.AddRule(nameof(Year), 
                () => RuleResult.Assert(UnitOfWork.Budgets.GetBudgetByYear(new DateTime(_year.GetValueOrDefault(), 1, 1)) == null, 
                "Zu dem ausgewählten Jahr existiert bereits ein Budget"));

        }

        #region New-Command

        private RelayCommand _newCommand;

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(New, param => CanNew())); }
        }

        private void New(object window)
        {
            var validationResult = Validator.ValidateAll();
            if (validationResult.IsValid)
            {
                var budget = new Budget();
                budget.Year = _year.GetValueOrDefault();
                budget.Expenses = _expense.GetValueOrDefault();
                budget.Revenue = _revenue.GetValueOrDefault();
                UnitOfWork.Budgets.Add(budget);

                if (UnitOfWork.Complete() > 0)
                {
                    NewBudget = budget;
                    ((Window)window).Close();
                }
                else
                {
                    MessageBox.Show(
                        "Die Buchung konnte nicht erfasst werden, versuchen Sie es später nocheinmal oder kontaktieren Sie unseren Support für weitere Unterstützung");
                }
            }
        }

        private bool CanNew()
        {
            return true;
        }

        #endregion
    }
}