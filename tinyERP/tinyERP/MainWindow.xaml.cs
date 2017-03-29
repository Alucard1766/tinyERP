using System.Windows;
using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;

namespace tinyERP.UI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            var vm = new MainWindowViewModel();
            vm.Init();
            DataContext = vm;
            InitializeComponent();
        }
    }
}