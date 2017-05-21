using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using tinyERP.UI.Factories;
using tinyERP.UI.Resources;
using FileAccess = tinyERP.Dal.FileAccess;

namespace tinyERP.UI.ViewModels
{
    class EditTemplateViewModel : ViewModelBase
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
                            FileAccess.Add(Offer, FileAccess.TemplatesDirectory);
                        break;
                    case TemplateType.Confirmation:
                        Properties.Settings.Default.ConfirmationTemplatePath =
                            FileAccess.Add(Confirmation, FileAccess.TemplatesDirectory);
                        break;
                    case TemplateType.Invoice:
                        Properties.Settings.Default.InvoiceTemplatePath =
                            FileAccess.Add(Invoice, FileAccess.TemplatesDirectory);
                        break;
                    default:
                        throw new ArgumentException("Invalid Template Instance");
                }
                Properties.Settings.Default.Save();
                MessageBox.Show("Vorlage erfolgreich hochgeladen.");
            }
            catch (IOException e)
            {
                MessageBox.Show(
                    "Ein Fehler ist aufgetreten. Bitte vergewissern Sie sich, dass das Dokument " +
                    "nicht von einem anderen Programm geöffnet ist und Sie die Leserechte dafür besitzen.");
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("Bitte wählen Sie eine Word-Datei aus.");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
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

        private bool CanUploadTemplate(object filePath)
        {
            return filePath != null;
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
            catch (NullReferenceException e)
            {
                MessageBox.Show("Bitte wählen Sie eine Word-Datei aus.");
            }
        }

        #endregion


    }
}
