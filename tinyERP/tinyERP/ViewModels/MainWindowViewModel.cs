using tinyERP.UI.Factories;

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
    }
}
