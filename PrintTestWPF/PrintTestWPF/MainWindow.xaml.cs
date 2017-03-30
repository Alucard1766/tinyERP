using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Spire.Pdf;

namespace PrintTestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnPrintPdf_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile("Projektplan_tinyERPV1.0.pdf");
            PrintDialog dialogPrint = new PrintDialog();
            dialogPrint.UserPageRangeEnabled = false;
            dialogPrint.PageRangeSelection = PageRangeSelection.AllPages;

            bool? print = dialogPrint.ShowDialog();

            if (print == true)
            {
                PrintDocument printDoc = doc.PrintDocument;
                printDoc.PrinterSettings.PrinterName = dialogPrint.PrintQueue.FullName;
                printDoc.Print();
            }
        }

        private void btnPrintDocx_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            this.pdfViewer1.PrintDialog = dialog;

        }
    }
}
