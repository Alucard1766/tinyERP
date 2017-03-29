using System.Windows;

namespace tinyERP.UI.ViewModels
{
    class MainWindowViewModel
    {
        private readonly BudgetViewModel budgetviewmodel;

        public MainWindowViewModel()
        {
            this.budgetviewmodel = new BudgetViewModel();
        }
        public void Init()
        {
            budgetviewmodel.Init();
        }
        public BudgetViewModel BudgetViewModel
        {
            get { return budgetviewmodel; }
        }
    }
}
