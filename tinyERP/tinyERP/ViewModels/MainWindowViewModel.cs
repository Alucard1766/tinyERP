using System.Windows;
using System.Windows.Navigation;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    class MainWindowViewModel
    {
        public BudgetViewModel BudgetViewModel { get; }
        private readonly UnitOfWorkFactory _factory;

        public MainWindowViewModel()
        {
            _factory = new UnitOfWorkFactory();
            BudgetViewModel = new BudgetViewModel(_factory);
        }
        public void Init()
        {
            BudgetViewModel.Init();
        }
    }
}
