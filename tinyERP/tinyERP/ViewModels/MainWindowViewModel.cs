using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            BudgetViewModel = new BudgetViewModel(new UnitOfWorkFactory());
        }

        public BudgetViewModel BudgetViewModel { get; }

        public void Init()
        {
            BudgetViewModel.Init();
        }
    }
}