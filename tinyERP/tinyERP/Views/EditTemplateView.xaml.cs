using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for EditTemplateView.xaml
    /// </summary>
    public partial class EditTemplateView : MetroWindow
    {
        public EditTemplateView()
        {
            InitializeComponent();
        }

        public EditTemplateView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();

        }
    }
}
