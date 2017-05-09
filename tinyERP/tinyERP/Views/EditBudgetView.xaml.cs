using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for EditBudgetView.xaml
    /// </summary>
    public partial class EditBudgetView : MetroWindow
    {
        public EditBudgetView()
        {
            InitializeComponent();
        }

        public EditBudgetView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
