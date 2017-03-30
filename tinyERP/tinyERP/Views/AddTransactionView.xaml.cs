using MahApps.Metro.Controls;

namespace tinyERP.UI.Views
{
    /// <summary>
    /// Interaction logic for AddTransactionView.xaml
    /// </summary>
    public partial class AddTransactionView : MetroWindow
    {
        public bool AddTransaction { get; set; }
        public AddTransactionView()
        {
            InitializeComponent();
        }
        public AddTransactionView(object vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void AddNewTransaction(object sender, System.Windows.RoutedEventArgs e)
        {
            AddTransaction = true;
            this.Close();
        }

        private void RemoveTransaction(object sender, System.Windows.RoutedEventArgs e)
        {
            AddTransaction = false;
            this.Close();
        }
    }
}
