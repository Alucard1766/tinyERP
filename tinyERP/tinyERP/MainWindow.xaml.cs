using System.Windows;
using MahApps.Metro.Controls;
using tinyERP.UI.ViewModels;
using System.Windows.Controls;
using System;

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

        private void TabHandler(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = Int32.Parse((string) button.Tag);
            MainTabControl.SelectedIndex = index;
        }
    }
}
