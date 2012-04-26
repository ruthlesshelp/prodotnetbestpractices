namespace Lender.Slos.DataInterchange
{
    using System.IO;

    public class Export
    {
        public string Data { get; private set; }

        public void Save(FileInfo fileInfo)
        {
            FileSystem.WriteAllText(fileInfo, Data);
        }
    }
}
