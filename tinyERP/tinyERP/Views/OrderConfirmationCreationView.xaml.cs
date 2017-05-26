using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for OrderConfirmationCreationView.xaml
    /// </summary>
    public partial class OrderConfirmationCreationView : MetroWindow
    {
        public OrderConfirmationCreationView()
        {
            InitializeComponent();
        }

        public OrderConfirmationCreationView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
