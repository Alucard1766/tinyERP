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
        private string _company;
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

        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                Validator.Validate(nameof(Company));
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
            Validator.AddRequiredRule(() => FirstName, "Vorname ist ein Pflichtfeld");
            Validator.AddRequiredRule(() => LastName, "Nachname ist ein Pflichtfeld");
            Validator.AddRequiredRule(() => Street, "Strasse ist ein Pflichtfeld");
            Validator.AddRequiredRule(() => Zip, "Postleitzahl ist ein Pflichtfeld");
            Validator.AddRequiredRule(() => City, "Ort ist ein Pflichtfeld");
            Validator.AddRule(nameof(Email),
                () =>
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                        return RuleResult.Assert(Regex.IsMatch(Email,
                                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                                RegexOptions.IgnoreCase),
                            "Email Adresse nicht gültig");
                    }

                    return RuleResult.Valid();
                });
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
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.Street = Street;
                customer.Zip = _zip.GetValueOrDefault();
                customer.City = City;
                customer.Email = Email;
                customer.Company = Company;

                if (customer.Id == 0)
                    customer = UnitOfWork.Customers.Add(customer);

                if (UnitOfWork.Complete() > 0)
                    ((Window) window).DialogResult = true;

                ((Window) window).Close();
            }
        }

        #endregion
    }
}
