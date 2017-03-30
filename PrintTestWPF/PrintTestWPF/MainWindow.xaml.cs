using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using Spire.Pdf;
using System;
using System.Diagnostics;
using System.Linq;

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

        private void btnOpenPdf_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Projektplan_tinyERPV1.0.pdf");
        }

        private void btnPrintPdfAdobe_Click(object sender, RoutedEventArgs e)
        {
            Pdf.PrintPDF(@"D:\Projekte\GitHub\tinyERP\PrintTestWPF\PrintTestWPF\bin\Debug\Projektplan_tinyERPV1.0.pdf");
        }

        private void btnOpenDocx_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Projektplan_tinyERP.docx");
        }

        private void btnOpenUnknown_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("Projektplan_tinyERP.unknown");
        }
    }

    public class Pdf
    {
        public static bool PrintPDF(string pdfFileName)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                proc.StartInfo.FileName =
                  @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", pdfFileName);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                KillAdobe("AcroRd32");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //For whatever reason, sometimes adobe likes to be a stage 5 clinger.
        //So here we kill it with fire.
        private static bool KillAdobe(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }
    }//END Class
}
