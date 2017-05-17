using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;
using tinyERP.UI.Factories;

namespace tinyERP.UI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            var vm = new MainWindowViewModel(new UnitOfWorkFactory());
            vm.Init();
            DataContext = vm;
            InitializeComponent();
        }
    }
}
