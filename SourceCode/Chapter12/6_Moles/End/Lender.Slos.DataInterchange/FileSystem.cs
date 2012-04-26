namespace Lender.Slos.DataInterchange
{
    using System;

    public static class FileSystem
    {
        public static string ReadAllText(
            System.IO.FileInfo fileInfo)
        {
            if (fileInfo == null) throw new ArgumentNullException("fileInfo");

            if (fileInfo.Exists)
            {
                return System.IO.File.ReadAllText(fileInfo.FullName);
            }

            return null;
        }

        public static void WriteAllText(
            System.IO.FileInfo fileInfo, 
            string contents)
        {
            if (fileInfo == null) throw new ArgumentNullException("fileInfo");

            System.IO.File.WriteAllText(fileInfo.FullName, contents);
        }
    }
}