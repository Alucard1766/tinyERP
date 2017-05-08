using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class DocumentViewModel : ViewModelBase
    {
        private Document _selectedDocument;

        public DocumentViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public Document SelectedDocument
        {
            get { return _selectedDocument; }
            set { SetProperty(ref _selectedDocument, value, nameof(SelectedDocument)); }
        }

        public ObservableCollection<Document> DocumentList { get; set; }

        public override void Load()
        {
            var documents = UnitOfWork.Documents.GetAll();
            DocumentList = new ObservableCollection<Document>(documents);
        }

        #region New-Documend-Command

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
    }
}
