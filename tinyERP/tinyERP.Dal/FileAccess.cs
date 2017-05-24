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
        public const string TemplatePath = "Templates";
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
            return Path.GetFileName(destination);
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

        public static string CreateNewInvoice(Customer customer, string documentNumber)
        {
            return CreateDocumentFromTemplate(customer, documentNumber,"Rechnung.docx");
        }

        public static string CreateNewOffer(Customer customer, string documentNumber)
        {
            return CreateDocumentFromTemplate(customer, documentNumber, "Offerte.docx");
        }

        public static string CreateNewOrderConfirmation(Customer customer, string documentNumber)
        {
            return CreateDocumentFromTemplate(customer, documentNumber, "Auftragsbestätigung.docx");
        }

        private static string CreateDocumentFromTemplate(Customer customer, string documentNumber, string templateName)
        {
            var destination = Add(Path.Combine(TemplatePath, templateName));

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(Path.Combine(RepositoryPath,destination), true))
            {
                string documentText;

                using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    documentText = sr.ReadToEnd();
                }

                Regex companyRegex = new Regex("\\[Firma\\]");
                Regex streetRegex = new Regex("\\[Strasse\\]");
                Regex zipRegex = new Regex("\\[Postleitzahl\\]");
                Regex cityRegex = new Regex("\\[Ort\\]");
                Regex firstNameRegex = new Regex("\\[Vorname\\]");
                Regex lastNameRegex = new Regex("\\[Nachname\\]");
                Regex dateRegex = new Regex("\\[(?:Rechnungs|Offerten|Auftrags)datum\\]");
                Regex documentNumberRegex = new Regex("\\[(?:Rechnungs|Offerten|Auftrags)nummer\\]");

                if (customer.Company != null)
                {
                    documentText = companyRegex.Replace(documentText, customer.Company);
                }
                else
                {
                    documentText = companyRegex.Replace(documentText, "");
                }
                documentText = streetRegex.Replace(documentText, customer.Street);
                documentText = zipRegex.Replace(documentText, customer.Zip.ToString());
                documentText = cityRegex.Replace(documentText, customer.City);
                documentText = firstNameRegex.Replace(documentText, customer.FirstName);
                documentText = lastNameRegex.Replace(documentText, customer.LastName);
                documentText = dateRegex.Replace(documentText, DateTime.Today.ToShortDateString());
                documentText = documentNumberRegex.Replace(documentText, documentNumber);

                using (StreamWriter sw = new StreamWriter(wordDocument.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(documentText);
                }
            }

            return destination;
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
