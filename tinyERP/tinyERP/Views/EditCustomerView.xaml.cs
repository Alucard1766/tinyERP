using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    ///     Interaction logic for EditTransactionView.xaml
    /// </summary>
    public partial class EditCustomerView : MetroWindow
    {
        public EditCustomerView()
        {
            InitializeComponent();
        }

        public EditCustomerView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
