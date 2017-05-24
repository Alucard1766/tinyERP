using System.Windows.Input;
using tinyERP.UI.Factories;

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
            switch (SelectedTab)
            {
                case 0:
                    BudgetViewModel.Load();
                    break;
                case 1:
                    DocumentViewModel.Load();
                    break;
                case 2:
                    CustomerViewModel.Load();
                    break;
                case 3:
                    OrderViewModel.Load();
                    break;
            }
        }

        #endregion
    }
}
