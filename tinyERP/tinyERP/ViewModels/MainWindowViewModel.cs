using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            BudgetViewModel = new BudgetViewModel(new UnitOfWorkFactory());
            OrderViewModel = new OrderViewModel(new UnitOfWorkFactory());
        }

        public BudgetViewModel BudgetViewModel { get; }

        public OrderViewModel OrderViewModel { get; }

        public void Init()
        {
            BudgetViewModel.Init();
            OrderViewModel.Init();
        }
    }
}
