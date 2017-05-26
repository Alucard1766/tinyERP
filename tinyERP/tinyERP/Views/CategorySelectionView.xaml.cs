using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for CategorySelectionView.xaml
    /// </summary>
    public partial class CategorySelectionView : MetroWindow
    {
        public CategorySelectionView()
        {
            InitializeComponent();
        }

        public CategorySelectionView(ViewModelBase vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
