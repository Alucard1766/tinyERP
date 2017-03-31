using System.Windows;
using System.Windows.Navigation;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    class MainWindowViewModel
    {
        public BudgetViewModel BudgetViewModel { get; }

        public MainWindowViewModel()
        {
            BudgetViewModel = new BudgetViewModel(new UnitOfWorkFactory());
        }

        public void Init()
        {
            BudgetViewModel.Init();
        }
    }
}
