using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class EditBudgetViewModel : ViewModelBase
    {
        private Budget budget;

        private int? _year;
        private double? _revenue;
        private double? _expenses;

        public EditBudgetViewModel(IUnitOfWorkFactory factory, Budget budget) : base(factory)
        {
            this.budget = budget;
            Year = (this.budget.Year == 0) ? null : this.budget.Year.ToString();
            Revenue = (this.budget.Revenue.CompareTo(0) == 0) ? null : this.budget.Revenue.ToString(CultureInfo.CurrentCulture);
            Expenses = (this.budget.Expenses.CompareTo(0) == 0) ? null : this.budget.Expenses.ToString(CultureInfo.CurrentCulture);
        }

        public bool IsNewBudget => budget.Id == 0;

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

                Validator.Validate(nameof(Revenue));
            }
        }

        public string Expenses
        {
            get { return _expenses.ToString(); }
            set
            {
                if (value == null)
                {
                    _expenses = null;
                }
                else
                {
                    double local;
                    double.TryParse(value, out local);
                    _expenses = local;
                }

                Validator.Validate(nameof(Expenses));
            }
        }

        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRule(nameof(Year), () => RuleResult.Assert(_year != null && _year > 1900, "Das Jahr muss grösser sein als 1900"));
            Validator.AddRule(nameof(Year), () => RuleResult.Assert(_year <= 9999, "Das Jahr muss kleiner sein als 10'000"));
            Validator.AddRule(nameof(Revenue),
                () => RuleResult.Assert(_revenue != null && _revenue >= 0, "Die Einnahmen müssen positiv sein"));
            Validator.AddRule(nameof(Expenses),
                () => RuleResult.Assert(_expenses != null && _expenses >= 0, "Die Ausgaben müssen positiv sein"));
            Validator.AddRule(nameof(Year), 
                () => RuleResult.Assert(!IsNewBudget || UnitOfWork.Budgets.GetBudgetByYear(new DateTime(_year.GetValueOrDefault(), 1, 1)) == null, 
                    "Zu dem ausgewählten Jahr existiert bereits ein Budget"));
        }

        #region Save-Command

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        private void Save(object window)
        {
            var validationResult = Validator.ValidateAll();
            if (validationResult.IsValid)
            {
                budget.Year = _year.GetValueOrDefault();
                budget.Revenue = _revenue.GetValueOrDefault();
                budget.Expenses = _expenses.GetValueOrDefault();

                if (IsNewBudget)
                    budget = UnitOfWork.Budgets.Add(budget);

                if (UnitOfWork.Complete() > 0)
                    ((Window)window).DialogResult = true;

                ((Window)window).Close();
            }
        }

        #endregion
    }
}
