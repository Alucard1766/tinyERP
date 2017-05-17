using System;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            BudgetViewModel = new BudgetViewModel(new UnitOfWorkFactory());
            CustomerViewModel = new CustomerViewModel(new UnitOfWorkFactory());
            OrderViewModel = new OrderViewModel(new UnitOfWorkFactory());
            DocumentViewModel = new DocumentViewModel(new UnitOfWorkFactory());
        }

        public BudgetViewModel BudgetViewModel { get; }

        public CustomerViewModel CustomerViewModel { get; }

        public OrderViewModel OrderViewModel { get; }

        public DocumentViewModel DocumentViewModel { get; }

        public void Init()
        {
            BudgetViewModel.Init();
            CustomerViewModel.Init();
            OrderViewModel.Init();
            DocumentViewModel.Init();
        }

        #region Edit-Templates-Command

        private RelayCommand _editTemplatesCommand;

        public ICommand EditTemplatesCommand
        {
            get { return _editTemplatesCommand ?? (_editTemplatesCommand = new RelayCommand(param => EditTemplates())); }
        }

        private void EditTemplates()
        {
            var document = new Document { IssueDate = DateTime.Today };
            var vm = new EditTemplateViewModel(null);
            vm.Load();
            var window = new EditTemplateView(vm);

            if (window.ShowDialog() ?? false)
            {
            }
        }

        #endregion

    }
}
