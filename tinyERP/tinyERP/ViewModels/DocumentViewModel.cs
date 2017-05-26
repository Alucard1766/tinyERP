using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;
using FileAccess = tinyERP.Dal.FileAccess;

namespace tinyERP.UI.ViewModels
{
    internal class DocumentViewModel : ViewModelBase
    {
        private Document _selectedDocument;
        private ObservableCollection<Document> _documentList;

        public DocumentViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public Document SelectedDocument
        {
            get { return _selectedDocument; }
            set { SetProperty(ref _selectedDocument, value, nameof(SelectedDocument)); }
        }

        public ObservableCollection<Document> DocumentList
        {
            get { return _documentList; }
            set
            {
                _documentList = value;
                OnPropertyChanged(nameof(DocumentList));
            }
        }

        public override void Load()
        {
            var documents = UnitOfWork.Documents.GetAll();
            DocumentList = new ObservableCollection<Document>(documents);
            CollectionViewSource.GetDefaultView(DocumentList).SortDescriptions.Add(new SortDescription("IssueDate", ListSortDirection.Descending));
        }

        #region New-Document-Command

        private RelayCommand _newDocumentCommand;

        public ICommand NewDocumentCommand
        {
            get { return _newDocumentCommand ?? (_newDocumentCommand = new RelayCommand(param => NewDocument())); }
        }

        private void NewDocument()
        {
            var document = new Document {IssueDate = DateTime.Today};
            var vm = new EditDocumentViewModel(new UnitOfWorkFactory(), document);
            vm.Init();
            var window = new EditDocumentView(vm);

            if (window.ShowDialog() ?? false)
            {
                DocumentList.Add(document);
            }
        }

        #endregion

        #region Open-Document-Command

        private RelayCommand _openDocumentCommand;

        public ICommand OpenDocumentCommand
        {
            get { return _openDocumentCommand ?? (_openDocumentCommand = new RelayCommand(param => OpenDocument(), CanOpenDocument)); }
        }

        private void OpenDocument()
        {
            const string fnfMessage = "Das gesuchte Dokument konnte nicht gefunden werden.";
            const string title = "Ein Fehler ist aufgetreten";
            try
            {
                FileAccess.Open(SelectedDocument.RelativePath, FileType.Document);
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

        private bool CanOpenDocument(object selectedItems)
        {
            return (selectedItems as ICollection)?.Count == 1;
        }

        #endregion
    }
}
