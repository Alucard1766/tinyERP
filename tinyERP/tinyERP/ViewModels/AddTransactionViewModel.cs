using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class AddTransactionViewModel : ViewModelBase
    {
        public AddTransactionViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public ObservableCollection<Category> CategoryList { get; set; }
        private string _name;
        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(this,nameof(Name));
                Validator.Validate(nameof(Name));
            }
        }

        private double? _amount;
        public double? Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(this, nameof(Amount));
                Validator.Validate(nameof(Amount));
            }
        }

        public DateTime Date { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        private int _privatPart;
        public int PrivatPart {
            get { return _privatPart;}
            set
            {
                _privatPart = value;
                OnPropertyChanged(this,nameof(PrivatPart));
                Validator.Validate(nameof(PrivatPart));
            } }
        public Category SelectedCategory { get; set; }
        public bool CreateTransaction { get; set; }

        public override void Load()
        {
            CreateTransaction = false;
            var categories = UnitOfWork.Categories.GetAll();
            CategoryList = new ObservableCollection<Category>(categories);

            AddRules();
          
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => Name, "Bezeichnung ist notwendig");
            Validator.AddRule(nameof(PrivatPart),
                () => RuleResult.Assert(PrivatPart >= 0 && PrivatPart <= 100, "Privatanteil muss zwischen 0 und 100% liegen"));
            Validator.AddRule(nameof(Amount),
                () => RuleResult.Assert(Amount > 0, "Betrag ist notwendig und muss grösser als 0 sein"));
        }

        #region New-Command

        private RelayCommand _newCommand;

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(New, param => CanNew())); }
        }

        private void New(object window)
        {

            ValidationResult validationResult = Validator.ValidateAll();
            if (validationResult.IsValid)
            {
                ((Window)window).Close();
            }
          }

        private bool CanNew()
        {
            return true;
        }

        #endregion
    }
}