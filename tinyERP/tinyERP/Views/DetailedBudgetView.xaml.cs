using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for DetailedBudgetView.xaml
    /// </summary>
    public partial class DetailedBudgetView : MetroWindow
    {
        public DetailedBudgetView()
        {
            InitializeComponent();
        }

        public DetailedBudgetView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
