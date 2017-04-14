using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    ///     Interaction logic for EditTransactionView.xaml
    /// </summary>
    public partial class EditTransactionView : MetroWindow
    {
        public EditTransactionView()
        {
            InitializeComponent();
        }

        public EditTransactionView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}