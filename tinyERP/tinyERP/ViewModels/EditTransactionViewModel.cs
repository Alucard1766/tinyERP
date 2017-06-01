using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts.Helpers;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class EditTransactionViewModel : ViewModelBase
    {
        private Budget budget;
        private Transaction transaction;

        private string _name;
        private double? _amount;
        private int? _privatPart;
        private bool _isRevenue;

        public EditTransactionViewModel(IUnitOfWorkFactory factory, Transaction transaction) : base(factory)
        {
            this.transaction = transaction;
            Name = this.transaction.Name;
            Amount = (this.transaction.Amount.CompareTo(0) == 0) ? null : this.transaction.Amount.ToString(CultureInfo.InvariantCulture);
            IsRevenue = this.transaction.IsRevenue;
            PrivatPart = this.transaction.PrivatePart.ToString();
            Date = this.transaction.Date;
            Comment = this.transaction.Comment;
            budget = this.transaction.Budget;
            SelectedCategory = this.transaction.Category;
        }
        
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Validator.Validate(nameof(Name));
            }
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

        public bool IsRevenue
        {
            get { return _isRevenue; }
            set
            {
                SetProperty(ref _isRevenue, value, nameof(IsRevenue), nameof(IsExpense));
            }
        }

        public bool IsExpense => !IsRevenue;

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

        public List<Category> CategoryList { get; set; }

        public Category SelectedCategory { get; set; }

        public override void Load()
        {
            var categories = UnitOfWork.Categories.GetAll();
            CategoryList = new List<Category>();
            categories.Where(c => c.ParentCategoryId == null).OrderBy(c => c.Name).ForEach(c =>
            {
                CategoryList.Add(c);
                if (c.SubCategories != null)
                {
                    CategoryList.AddRange(c.SubCategories.OrderBy(sc => sc.Name));
                }
            });
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => Name, "Bezeichnung ist notwendig");
            Validator.AddRule(nameof(PrivatPart),
                () => RuleResult.Assert(IsRevenue || _privatPart >= 0 && _privatPart <= 100,
                    "Privatanteil muss zwischen 0 und 100 Prozent liegen"));
            Validator.AddRule(nameof(Amount),
                () => RuleResult.Assert(_amount > 0, "Betrag muss grösser als 0 sein"));
            Validator.AddRule(nameof(Date), () =>
            {
                budget = UnitOfWork.Budgets.GetBudgetByYear(Date);
                return RuleResult.Assert(budget != null,
                    $"Budget wurde nicht gefunden - Erstellen Sie zuerst das Budget für das Jahr {Date.Year}");
            });
            Validator.AddRequiredRule(() => Date, "Datum ist notwendig");
            Validator.AddRequiredRule(() => SelectedCategory, "Es wurde keine Kategorie ausgewählt - Erstellen Sie zuerst eine passende Kategorie");
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
                transaction.Name = Name;
                transaction.Amount = _amount.GetValueOrDefault();
                transaction.IsRevenue = IsRevenue;
                transaction.PrivatePart = IsRevenue ? 0 : _privatPart.GetValueOrDefault();
                transaction.Date = Date;
                transaction.Comment = Comment;
                transaction.Budget = budget;
                transaction.Category = SelectedCategory;

                if (transaction.Id == 0)
                    transaction = UnitOfWork.Transactions.Add(transaction);

                if (UnitOfWork.Complete() > 0)
                    ((Window) window).DialogResult = true;

                ((Window) window).Close();
            }
        }

        #endregion
    }
}
