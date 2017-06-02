using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using tinyERP.Dal.Types;
using tinyERP.UI.Factories;
using tinyERP.UI.Resources;
using FileAccess = tinyERP.Dal.FileAccess;

namespace tinyERP.UI.ViewModels
{
    internal class EditTemplateViewModel : ViewModelBase
    {
        private string _offer;
        private string _confirmation;
        private string _invoice;

        public EditTemplateViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public string Offer
        {
            get { return _offer; }
            set { SetProperty(ref _offer, value, nameof(Offer)); }
        }

        public string Confirmation
        {
            get { return _confirmation; }
            set { SetProperty(ref _confirmation, value, nameof(Confirmation)); }
        }

        public string Invoice
        {
            get { return _invoice; }
            set { SetProperty(ref _invoice, value, nameof(Invoice)); }
        }

        public override void Load()
        {
            Offer = Properties.Settings.Default.OfferTemplatePath;
            Confirmation = Properties.Settings.Default.ConfirmationTemplatePath;
            Invoice = Properties.Settings.Default.InvoiceTemplatePath;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")] // argument null exception is caught in calling method
        private void CheckIfWordFile(string filePath)
        {
            if (!(Path.GetExtension(filePath).ToLower().Equals(".docx") ||
                  Path.GetExtension(filePath).ToLower().Equals(".doc")))
            {
                throw new ArgumentException("Ungültiger Dateityp, nur Word-Dokumente werden akzeptiert.");
            }
        }

        #region Upload-Template-Command

        private RelayCommand _uploadTemplateCommand;

        public ICommand UploadTemplateCommand
        {
            get
            {
                return _uploadTemplateCommand ?? (_uploadTemplateCommand =
                    new RelayCommand(UploadTemplate, CanUploadTemplate));
            }
        }

        private void UploadTemplate(object templateType)
        {
            try
            {
                switch ((TemplateType)templateType)
                {
                    case TemplateType.Offer:
                        Properties.Settings.Default.OfferTemplatePath =
                            FileAccess.Add(Offer, FileType.Template);
                        break;
                    case TemplateType.Confirmation:
                        Properties.Settings.Default.ConfirmationTemplatePath =
                            FileAccess.Add(Confirmation, FileType.Template);
                        break;
                    case TemplateType.Invoice:
                        Properties.Settings.Default.InvoiceTemplatePath =
                            FileAccess.Add(Invoice, FileType.Template);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
                }
                Properties.Settings.Default.Save();
                MessageBox.Show("Vorlage erfolgreich hochgeladen.");
            }
            catch (IOException)
            {
                MessageBox.Show(
                    "Ein Fehler ist aufgetreten. Bitte vergewissern Sie sich, dass das Dokument " +
                    "nicht von einem anderen Programm geöffnet ist und Sie die Leserechte dafür besitzen.");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Bitte wählen Sie eine Word-Datei aus.");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanUploadTemplate(object templateType)
        {
            bool canUpload;
            switch ((TemplateType)templateType)
            {
                case TemplateType.Offer:
                    canUpload = Offer != null && !Offer.Equals(Properties.Settings.Default.OfferTemplatePath);
                    break;
                case TemplateType.Confirmation:
                    canUpload = Confirmation != null && !Confirmation.Equals(Properties.Settings.Default.ConfirmationTemplatePath);
                    break;
                case TemplateType.Invoice:
                    canUpload = Invoice != null && !Invoice.Equals(Properties.Settings.Default.InvoiceTemplatePath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
            }
            return canUpload;
        }

        #endregion

        #region ChooseFile-Command

        private RelayCommand _chooseFileCommand;

        public ICommand ChooseFileCommand
        {
            get { return _chooseFileCommand ?? (_chooseFileCommand = new RelayCommand(ChooseFile)); }
        }

        private void ChooseFile(object templateType)
        {
            var openFileDialog = new OpenFileDialog();
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    switch ((TemplateType) templateType)
                    {
                        case TemplateType.Offer:
                            var offer = openFileDialog.FileName;
                            CheckIfWordFile(offer);
                            Offer = offer;
                            break;
                        case TemplateType.Confirmation:
                            var confirmation = openFileDialog.FileName;
                            CheckIfWordFile(confirmation);
                            Confirmation = confirmation;
                            break;
                        case TemplateType.Invoice:
                            var invoice = openFileDialog.FileName;
                            CheckIfWordFile(invoice);
                            Invoice = invoice;
                            break;
                        default:
                            throw new ArgumentException("Invalid Template Instance");

                    }
                }
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Bitte wählen Sie eine Word-Datei aus.");
            }
        }

        #endregion

        #region Open-Template-Command

        private RelayCommand _openTemplateCommand;

        public ICommand OpenTemplateCommand
        {
            get { return _openTemplateCommand ?? (_openTemplateCommand = new RelayCommand(OpenTemplate, CanOpenTemplate)); }
        }

        private void OpenTemplate(object templateType)
        {
            const string fnfMessage = "Das gesuchte Dokument konnte nicht gefunden werden.";
            const string title = "Ein Fehler ist aufgetreten";
            try
            {
                switch ((TemplateType)templateType)
                {
                    case TemplateType.Offer:
                        FileAccess.Open(Properties.Settings.Default.OfferTemplatePath, FileType.Template);
                        break;
                    case TemplateType.Confirmation:
                        FileAccess.Open(Properties.Settings.Default.ConfirmationTemplatePath, FileType.Template);
                        break;
                    case TemplateType.Invoice:
                        FileAccess.Open(Properties.Settings.Default.InvoiceTemplatePath, FileType.Template);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(fnfMessage, title);
            }
            catch (Win32Exception)
            {
                MessageBox.Show(fnfMessage, title);
            }
        }

        private bool CanOpenTemplate(object templateType)
        {
            string storedPath;
            switch ((TemplateType)templateType)
            {
                case TemplateType.Offer:
                    storedPath = Properties.Settings.Default.OfferTemplatePath;
                    break;
                case TemplateType.Confirmation:
                    storedPath = Properties.Settings.Default.ConfirmationTemplatePath;
                    break;
                case TemplateType.Invoice:
                    storedPath = Properties.Settings.Default.InvoiceTemplatePath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null);
            }
            return storedPath?.Length > 0;
        }

        #endregion
    }
}
