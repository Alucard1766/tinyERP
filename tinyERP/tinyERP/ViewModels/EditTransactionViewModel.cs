using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class EditTransactionViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Validator.Validate(nameof(Name));
            }
        }

        private double? _amount;
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

        public bool IsRevenue { get; set; }
        public bool IsExpense => !IsRevenue;

        private int? _privatPart;
        public string PrivatPart
        {
            get { return _privatPart.ToString(); }
            set
            {
                if (value == null)
                {
                    _privatPart = null;
                }
                else
                {
                    int local;
                    int.TryParse(value, out local);
                    _privatPart = local;
                }

                Validator.Validate(nameof(PrivatPart));
            }
        }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        private Budget _budget;

        public ObservableCollection<Category> CategoryList { get; set; }
        public Category SelectedCategory { get; set; }

        private Transaction _transaction;

        public EditTransactionViewModel(IUnitOfWorkFactory factory, Transaction transaction) : base(factory)
        {
            _transaction = transaction;
            Name = _transaction.Name;
            Amount = _transaction.Amount.ToString(CultureInfo.InvariantCulture);
            IsRevenue = _transaction.IsRevenue;
            PrivatPart = _transaction.PrivatePart.ToString();
            Date = _transaction.Date;
            Comment = _transaction.Comment;
            _budget = _transaction.Budget;
            SelectedCategory = _transaction.Category;
        }

        public override void Load()
        {
            var categories = UnitOfWork.Categories.GetAll();
            CategoryList = new ObservableCollection<Category>(categories);
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => Name, "Bezeichnung ist notwendig");
            Validator.AddRule(nameof(PrivatPart),
                () => RuleResult.Assert(_privatPart >= 0 && _privatPart <= 100,
                    "Privatanteil muss zwischen 0 und 100 Prozent liegen"));
            Validator.AddRule(nameof(Amount),
                () => RuleResult.Assert(_amount > 0, "Betrag muss grösser als 0 sein"));
            Validator.AddRule(nameof(Date), () =>
            {
                _budget = UnitOfWork.Budgets.GetBudgetByYear(Date);
                return RuleResult.Assert(_budget != null,
                    $"Budget wurde nicht gefunden - Erstellen Sie zuerst das Budget für das Jahr {Date.Year}");
            });
            Validator.AddRule(nameof(Date), () =>
                RuleResult.Assert(Date.Date.CompareTo(DateTime.Now.Date) <= 0,
                    "Ungültiges Datum - Datum liegt in der Zukunft"));
            Validator.AddRule(nameof(SelectedCategory), () =>
                RuleResult.Assert(SelectedCategory != null,
                    "Es wurde keine Kategorie ausgewählt - Erstellen Sie zuerst eine passende Kategorie"));
        }

        #region Save-Command

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save, param => CanSave())); }
        }

        private void Save(object window)
        {
            var validationResult = Validator.ValidateAll();
            if (validationResult.IsValid)
            {
                _transaction.Name = Name;
                _transaction.Amount = _amount.GetValueOrDefault();
                _transaction.IsRevenue = IsRevenue;
                _transaction.PrivatePart = _privatPart.GetValueOrDefault();
                _transaction.Date = Date;
                _transaction.Comment = Comment;
                _transaction.BudgetId = _budget.Id;
                _transaction.CategoryId = SelectedCategory.Id;

                if (_transaction.Id == 0)
                    _transaction = UnitOfWork.Transactions.Add(_transaction);

                if (UnitOfWork.Complete() > 0)
                    ((Window) window).DialogResult = true;

                ((Window) window).Close();
            }
        }

        private bool CanSave()
        {
            return true;
        }

        #endregion
    }
}