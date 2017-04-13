using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class AddTransactionViewModel : ViewModelBase
    {
        private double? _amount;
        private Budget _budget;
        private string _name;
        private int? _privatPart;

        public AddTransactionViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public ObservableCollection<Category> CategoryList { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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

                OnPropertyChanged(nameof(Amount));
                Validator.Validate(nameof(Amount));
            }
        }

        public DateTime Date { get; set; } = DateTime.Now;
        public string Comment { get; set; }

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
                OnPropertyChanged(nameof(PrivatPart));
                Validator.Validate(nameof(PrivatPart));
            }
        }

        public Category SelectedCategory { get; set; }
        public Transaction NewTransaction { get; set; }
        public bool IsEarning { get; set; } = true;

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
                var transaction = new Transaction();
                transaction.Name = Name;
                transaction.Amount = _amount.GetValueOrDefault();
                transaction.Date = Date;
                transaction.Comment = Comment;
                transaction.PrivatePart = _privatPart.GetValueOrDefault();
                transaction.BudgetId = _budget.Id;
                transaction.CategoryId = SelectedCategory.Id;
                UnitOfWork.Transactions.Add(transaction);

                if (UnitOfWork.Complete() > 0)
                {
                    NewTransaction = transaction;
                    ((Window) window).Close();
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