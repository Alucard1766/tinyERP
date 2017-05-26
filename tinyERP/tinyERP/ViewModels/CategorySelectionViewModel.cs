using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LiveCharts.Helpers;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class CategorySelectionViewModel : ViewModelBase
    {

        public CategorySelectionViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public List<Category> CategoryList { get; set; }

        public Category SelectedCategory { get; set; }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => SelectedCategory, "Es wurde keine Kategorie ausgewählt");
        }

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
                if (UnitOfWork.Complete() >= 0)
                    ((Window)window).DialogResult = true;

                ((Window)window).Close();
            }
        }

        #endregion
    }
}
