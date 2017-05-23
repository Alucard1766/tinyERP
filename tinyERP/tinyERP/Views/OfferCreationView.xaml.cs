using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for OfferCreationView.xaml
    /// </summary>
    public partial class OfferCreationView : MetroWindow
    {
        public OfferCreationView()
        {
            InitializeComponent();
        }

        public OfferCreationView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
