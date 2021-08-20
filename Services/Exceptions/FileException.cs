namespace SHS.Services.Exceptions
{
    public class FileIsNullError : ShsException
    {
        public FileIsNullError(string message)
            : base(message, "FE2"){}
    }
    
    /*  檔案的副檔名有誤, 導致後續操作會有問題
    */
    public class FileExtensionError : ShsException
    {
        public FileExtensionError(string message)
            : base(message, "FE0"){}
    }

    public class FileSizeError : ShsException
    {
        public FileSizeError(string message)
            : base(message, "FE1"){}
    }
}