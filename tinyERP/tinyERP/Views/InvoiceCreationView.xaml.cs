using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for InvoiceCreationView.xaml
    /// </summary>
    public partial class InvoiceCreationView : MetroWindow
    {
        public InvoiceCreationView()
        {
            InitializeComponent();
        }

        public InvoiceCreationView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
