using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    ///     Interaction logic for AddTransactionView.xaml
    /// </summary>
    public partial class AddTransactionView : MetroWindow
    {
        public AddTransactionView()
        {
            InitializeComponent();
        }

        public AddTransactionView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}