using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyERP.BusinessLayer
{
    public static class FileAccess
    {
        private const string RepositoryPath = "Files";
        private static Random rng = new Random();

        public static string Add(string source)
        {
            var destinationFileName = RandomFileName();
            var destination = Path.Combine(RepositoryPath, destinationFileName);

            if (!Directory.Exists(RepositoryPath))
            {
                Directory.CreateDirectory(RepositoryPath);
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

        private static string RandomFileName()
        {
            const int fileNameLength = 20;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, fileNameLength)
                .Select(s => s[rng.Next(s.Length)]).ToArray());
        }
    }
}
