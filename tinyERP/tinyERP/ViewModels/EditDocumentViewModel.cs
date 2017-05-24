using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MvvmValidation;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;
using tinyERP.UI.Factories;
using FileAccess = tinyERP.Dal.FileAccess;

namespace tinyERP.UI.ViewModels
{
    internal class EditDocumentViewModel : ViewModelBase
    {
        private Document document;

        private string _name;
        private string _relativePath;
        private DateTime _issueDate;

        public EditDocumentViewModel(IUnitOfWorkFactory factory, Document document) : base(factory)
        {
            this.document = document;
            Name = this.document.Name;
            Tag = this.document.Tag;
            RelativePath = this.document.RelativePath;
            IssueDate = this.document.IssueDate;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Validator.Validate(nameof(Name));
            }
        }

        public string Tag { get; set; }


        public string RelativePath
        {
            get { return _relativePath; }
            set
            {
                SetProperty(ref _relativePath, value, nameof(RelativePath));
                Validator.Validate(nameof(RelativePath));
            }
        }

        public DateTime IssueDate
        {
            get { return _issueDate; }
            set
            {
                _issueDate = value;
                Validator.Validate(nameof(IssueDate));
            }
        }

        public override void Load()
        {
            AddRules();
        }

        private void AddRules()
        {
            Validator.AddRequiredRule(() => Name, "Name ist notwendig");
            Validator.AddRequiredRule(() => RelativePath, "Dateiname ist notwendig");
            Validator.AddRule(nameof(RelativePath),
                () => RuleResult.Assert(
                    File.Exists(_relativePath) || File.Exists(Path.Combine(FileAccess.FilesDirectory, _relativePath)),
                    "File does not exist anymore"));
            Validator.AddRequiredRule(() => IssueDate, "Das Ausstellungsdatum muss angegeben sein");
        }

        #region Save-Command

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        private void Save(object window)
        {
            var validationResult = Validator.ValidateAll();
            if (validationResult.IsValid)
            {
                document.Name = Name;
                document.Tag = Tag;
                document.IssueDate = IssueDate;
                if (document.RelativePath == null)
                {
                    document.RelativePath = FileAccess.Add(RelativePath, FileType.Document);
                }
                else if (document.RelativePath != RelativePath)
                {
                    FileAccess.Delete(document.RelativePath);
                    document.RelativePath = FileAccess.Add(RelativePath, FileType.Document);
                }

                if (document.Id == 0)
                    document = UnitOfWork.Documents.Add(document);

                if (UnitOfWork.Complete() > 0)
                    ((Window)window).DialogResult = true;

                ((Window)window).Close();
            }
        }

        #endregion

        #region ChooseFile-Command

        private RelayCommand _chooseFileCommand;

        public ICommand ChooseFileCommand
        {
            get { return _chooseFileCommand ?? (_chooseFileCommand = new RelayCommand(param => ChooseFile())); }
        }

        private void ChooseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                RelativePath = openFileDialog.FileName;
            }
        }

        #endregion

    }
}
