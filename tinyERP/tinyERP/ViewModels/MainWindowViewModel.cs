using System.Windows;
using System.Windows.Navigation;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    class MainWindowViewModel
    {
        public readonly BudgetViewModel Budgetviewmodel;
        private readonly UnitOfWorkFactory _factory;

        public MainWindowViewModel()
        {
            _factory = new UnitOfWorkFactory();
            Budgetviewmodel = new BudgetViewModel(_factory);
        }
        public void Init()
        {
            Budgetviewmodel.Init();
        }

    }
}
