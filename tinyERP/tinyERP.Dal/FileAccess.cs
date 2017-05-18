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

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")] //null-value is tested in Add-method
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
