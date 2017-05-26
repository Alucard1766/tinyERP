using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
