namespace SHS.Services.Validators
{
    public class ErrorCode
    {
        public static string IsEmpty { get => "FV0"; }
        public static string ExceedLength { get => "FV1"; }
        public static string NotEmailFormat { get => "FV2"; }
        public static string NotDateTimeFormat { get => "FV3"; }
        public static string FileExtensionUnavailable { get => "FE0"; }
        public static string ExceedFileSize { get => "FE1"; }
    }
}