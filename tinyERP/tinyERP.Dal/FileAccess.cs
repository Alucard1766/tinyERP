using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace tinyERP.Dal
{
    public static class FileAccess
    {
        public const string FilesDirectory = "Files";
        public const string TemplatesDirectory = "Templates";
        private static readonly Random Rng = new Random();
        //Rng is defined here to prevent too many instantiations in rapid succession, which would potentially lead to same numbers generated

        public static string Add(string sourceFile, string targetDirectory)
        {
            if (sourceFile == null)
            {
                throw new ArgumentNullException($"Keinen Pfad angegeben");
            }
            var destinationFileName = Path.GetFileName(sourceFile);
            var destination = Path.Combine(targetDirectory, destinationFileName);

            Directory.CreateDirectory(targetDirectory);
            if (targetDirectory.Equals(FilesDirectory))
            {
                if (File.Exists(destination))
                {
                    MakePathUnique(ref destination);
                }
                File.Copy(sourceFile, destination);
            }
            else if (targetDirectory.Equals(TemplatesDirectory))
            {
                File.Copy(sourceFile, destination, true);
            }
            else
            {
                throw new ArgumentException("Unzulässiger Pfad");
            }
            return destinationFileName;
        }

        public static void Delete(string fileName)
        {
            var file = Path.Combine(FilesDirectory, fileName);
            File.Delete(file);
        }

        public static void Open(string fileName)
        {
            var file = Path.Combine(FilesDirectory, fileName);
            Process.Start(file);
        }

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")] //null-value is tested in Add method
        private static void MakePathUnique(ref string destination)
        {
            var pathWithoutExtension  = Path.Combine(FilesDirectory, Path.GetFileNameWithoutExtension(destination));
            var extension = Path.GetExtension(destination);
            do
            {
                destination = $"{pathWithoutExtension}_{RandomSuffix()}{extension}";
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
