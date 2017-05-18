using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal
{
    public static class FileAccess
    {
        public const string RepositoryPath = "Files";
        private static readonly Random Rng = new Random();
        //Rng is defined here to prevent too many instantiations in rapid succession, which would potentially lead to same numbers generated

        public static string Add(string source)
        {
            if(source == null)
                throw new ApplicationException("Kein Pfadname angegeben");
            var destinationFileName = Path.GetFileName(source);
            var destination = Path.Combine(RepositoryPath, destinationFileName);

            Directory.CreateDirectory(RepositoryPath);
            if (File.Exists(destination))
            {
                MakePathUnique(ref destination);
            }
            File.Copy(source, destination);
            return destinationFileName;
        }

        public static void Delete(string fileName)
        {
            var file = Path.Combine(RepositoryPath, fileName);
            File.Delete(file);
        }

        public static void Open(string fileName)
        {
            var file = Path.Combine(RepositoryPath, fileName);
            Process.Start(file);
        }

        public static string CreateInvoice(Customer customer, string invoiceNumber)
        {
            var fileName = Add(Path.Combine("Templates", "Rechnung.docx"));

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(Path.Combine(RepositoryPath, fileName), true))
            {
                string documentText = null;
                using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    documentText = sr.ReadToEnd();
                }

                Regex companyRegex = new Regex("\\[Company\\]");
                Regex streetRegex = new Regex("\\[Street\\]");
                Regex zipRegex = new Regex("\\[Zip\\]");
                Regex cityRegex = new Regex("\\[City\\]");
                Regex firstNameRegex = new Regex("\\[FirstName\\]");
                Regex lastNameRegex = new Regex("\\[LastName\\]");
                Regex issueDateRegex = new Regex("\\[IssueDate\\]");
                Regex invoiceNumberRegex = new Regex("\\[InvoiceNumber\\]");

                //TODO: Handle optional fields
                documentText = companyRegex.Replace(documentText, customer.Company);
                documentText = streetRegex.Replace(documentText, customer.Street);
                documentText = zipRegex.Replace(documentText, customer.Zip.ToString());
                documentText = cityRegex.Replace(documentText, customer.City);
                documentText = firstNameRegex.Replace(documentText, customer.FirstName);
                documentText = lastNameRegex.Replace(documentText, customer.LastName);
                documentText = issueDateRegex.Replace(documentText, DateTime.Today.ToShortDateString());
                documentText = invoiceNumberRegex.Replace(documentText, invoiceNumber);

                using (StreamWriter sw = new StreamWriter(wordDocument.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(documentText);
                }
            }

            return fileName;
        }

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")] //null-value is tested in Add method
        private static void MakePathUnique(ref string destination)
        {
            var fileWithoutExtension  = Path.Combine(Path.GetDirectoryName(destination), Path.GetFileNameWithoutExtension(destination));
            var extension = Path.GetExtension(destination);
            do
            {
                destination = $"{fileWithoutExtension}_{RandomSuffix()}{extension}";
            }
            while (File.Exists(destination));
        }

        private static string RandomSuffix()
        {
            const int suffixLength = 5;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, suffixLength)
                .Select(s => s[Rng.Next(s.Length)]).ToArray());
        }
    }
}
