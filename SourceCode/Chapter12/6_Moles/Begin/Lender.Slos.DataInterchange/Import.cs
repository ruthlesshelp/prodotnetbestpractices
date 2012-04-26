namespace Lender.Slos.DataInterchange
{
    public class Import
    {
        public string Data { get; private set; }

        public void Load(System.IO.FileInfo fileInfo)
        {
            Data = FileSystem.ReadAllText(fileInfo);
        }
    }
}
