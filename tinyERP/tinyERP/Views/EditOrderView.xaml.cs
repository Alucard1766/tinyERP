using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    ///     Interaction logic for EditOrderView.xaml
    /// </summary>
    public partial class EditOrderView : MetroWindow
    {
        public EditOrderView()
        {
            InitializeComponent();
        }

        public EditOrderView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
