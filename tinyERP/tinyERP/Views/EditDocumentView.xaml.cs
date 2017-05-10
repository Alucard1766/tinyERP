using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for EditDocumentView.xaml
    /// </summary>
    public partial class EditDocumentView : MetroWindow
    {
        public EditDocumentView()
        {
            InitializeComponent();
        }

        public EditDocumentView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
