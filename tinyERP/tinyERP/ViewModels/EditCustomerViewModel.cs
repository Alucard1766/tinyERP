using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class EditCustomerViewModel : ViewModelBase
    {
        private Customer customer;

        private string _firstName;
        private string _lastName;
        private string _street;
        private string _city;
        private string _firma;
        private string _email;
        private int? _zip;

        public EditCustomerViewModel(IUnitOfWorkFactory factory, Customer customer) : base(factory)
        {
            this.customer = customer;
            FirstName = this.customer.FirstName;
        }
        
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                Validator.Validate(nameof(FirstName));
            }
        }

        public string Zip
        {
            get { return _zip.ToString(); }
            set
            {
                if (value == null)
                {
                    _zip = null;
                }
                else
                {
                    int local;
                    int.TryParse(value, out local);
                    _zip = local;
                }

                Validator.Validate(nameof(Zip));
            }
        }

        public string Firma
        {
            get { return _firma; }
            set
            {
                _firma = value;
                Validator.Validate(nameof(Firma));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                Validator.Validate(nameof(LastName));
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                Validator.Validate(nameof(Street));
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                Validator.Validate(nameof(City));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                Validator.Validate(nameof(Email));
            }
        }
        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => FirstName, "Vorname ist notwendig");
            Validator.AddRequiredRule(() => LastName, "Nachname ist notwendig");
            Validator.AddRequiredRule(() => Street, "Strasse ist notwendig");
            Validator.AddRequiredRule(() => Zip, "Postleitzahl ist notwendig");
            Validator.AddRequiredRule(() => City, "Ort ist notwendig");
            Validator.AddRule(nameof(Email), 
                () => RuleResult.Assert(Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase),
                "Email Adresse nicht gültig"));
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
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.Street = Street;
                customer.Zip = _zip.GetValueOrDefault();
                customer.City = City;

                if (customer.Id == 0)
                    customer = UnitOfWork.Customers.Add(customer);

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
