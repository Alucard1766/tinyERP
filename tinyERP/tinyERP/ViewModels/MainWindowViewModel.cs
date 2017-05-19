using System;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private int _selectedTab;

        public int SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                _selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
            }
        }

        public MainWindowViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
            BudgetViewModel = new BudgetViewModel(factory);
            CustomerViewModel = new CustomerViewModel(factory);
            OrderViewModel = new OrderViewModel(factory);
            DocumentViewModel = new DocumentViewModel(factory);
        }

        public BudgetViewModel BudgetViewModel { get; }

        public CustomerViewModel CustomerViewModel { get; }

        public OrderViewModel OrderViewModel { get; }

        public DocumentViewModel DocumentViewModel { get; }

        public override void Load()
        {
            BudgetViewModel.Init();
            CustomerViewModel.Init();
            OrderViewModel.Init();
            DocumentViewModel.Init();
        }

        #region Switch-Tab-Command

        private RelayCommand _switchTabCommand;

        public ICommand SwitchTabCommand
        {
            get { return _switchTabCommand ?? (_switchTabCommand = new RelayCommand(SwitchTab)); }
        }

        private void SwitchTab(object tag)
        {
            SelectedTab = int.Parse((string)tag);
        }

        #endregion

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
