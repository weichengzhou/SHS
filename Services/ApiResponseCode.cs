namespace SHS.Services
{
    public class ApiResponseCode
    {
        public static string CreateAgent
        {
            get => "S00";
        }

        public static string UpdateAgent
        {
            get => "S01";
        }

        public static string GetAgents
        {
            get => "S02";
        }

        public static string GetAgent
        {
            get => "S02";
        }

        public static string ImportAgents
        {
            get => "S03";
        }

        public static string AgentIsExist
        {
            get => "F00";
        }

        public static string AgentNotExist
        {
            get => "F01";
        }

        public static string ValidationError
        {
            get => "FC0";
        }

        public static string IsEmpty
        {
            get => "FV0";
        }

        public static string ExceedLength
        {
            get => "FV1";
        }

        public static string NotEmailFormat
        {
            get => "FV2";
        }

        public static string NotDateTimeFormat
        {
            get => "FV3";
        }

        public static string FileExtensionUnavailable
        {
            get => "FF0";
        }

        public static string ExceedFileSize
        {
            get => "FF1";
        }
        
        public static string UnexpectedError
        {
            get => "F99";
        }
    }
}