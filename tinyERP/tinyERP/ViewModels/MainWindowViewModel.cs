using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            BudgetViewModel = new BudgetViewModel(new UnitOfWorkFactory());
            CustomerViewModel = new CustomerViewModel(new UnitOfWorkFactory());
        }

        public BudgetViewModel BudgetViewModel { get; }
        public CustomerViewModel CustomerViewModel { get; }

        public void Init()
        {
            BudgetViewModel.Init();
            CustomerViewModel.Init();
        }
    }
}
