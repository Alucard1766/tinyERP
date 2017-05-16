using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace tinyERP.Dal
{
    public static class FileAccess
    {
        public const string RepositoryPath = "Files";
        public const string TemplatePath = "Templates";
        private static readonly Random Rng = new Random();
        //Rng is defined here to prevent too many instantiations in rapid succession, which would potentially lead to same numbers generated

        public static string Add(string sourceFile, string targetDirectory)
        {
            if(sourceFile == null)
                throw new ArgumentNullException($"Keinen Pfad angegeben");
            var destinationFileName = Path.GetFileName(sourceFile);
            var destination = Path.Combine(targetDirectory, destinationFileName);

            Directory.CreateDirectory(targetDirectory);
            if (targetDirectory.Equals(RepositoryPath))
            {
                if (File.Exists(destination))
                {
                    MakePathUnique(targetDirectory, ref destinationFileName);
                }
                File.Copy(sourceFile, Path.Combine(targetDirectory, destinationFileName));
            }
            else if (targetDirectory.Equals(TemplatePath))
            {
                File.Copy(sourceFile, Path.Combine(targetDirectory, destinationFileName), true);
            }
            else
            {
                throw new ArgumentException("Unzulässiger Pfad");
            }
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

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")] //null-value is tested in Add method
        private static void MakePathUnique(string targetDirectory, ref string fileName)
        {
            var fileWithoutExtension  = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName));
            var extension = Path.GetExtension(fileName);
            do
            {
                fileName = $"{fileWithoutExtension}_{RandomSuffix()}{extension}";
            }
            while (File.Exists(fileName));
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
