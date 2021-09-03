namespace SHS.Services
{
    /// <summary>
    /// The code that API will return.
    /// </summary>
    public class ApiResponseCode
    {
        /// <summary>
        /// API code that create agent.
        /// </summary>
        public static string CreateAgent
        {
            get => "S00";
        }

        /// <summary>
        /// API code that update agent.
        /// </summary>
        public static string UpdateAgent
        {
            get => "S01";
        }

        /// <summary>
        /// API code that get agents.
        /// </summary>
        public static string GetAgents
        {
            get => "S02";
        }

        /// <summary>
        /// API code that get agent.
        /// </summary>
        public static string GetAgent
        {
            get => "S02";
        }

        /// <summary>
        /// API code that import agents.
        /// </summary>
        public static string ImportAgents
        {
            get => "S03";
        }

        /// <summary>
        /// API code that agent is exist.
        /// </summary>
        public static string AgentIsExist
        {
            get => "F00";
        }

        /// <summary>
        /// API code that agent not exist.
        /// </summary>
        public static string AgentNotExist
        {
            get => "F01";
        }

        /// <summary>
        /// API code that agent validate error.
        /// </summary>
        public static string ValidationError
        {
            get => "FC0";
        }

        /// <summary>
        /// API code that field is empty.
        /// </summary>
        public static string IsEmpty
        {
            get => "FV0";
        }

        /// <summary>
        /// API code that field is exceed length.
        /// </summary>
        public static string ExceedLength
        {
            get => "FV1";
        }

        /// <summary>
        /// API code that field is not email format.
        /// </summary>
        public static string NotEmailFormat
        {
            get => "FV2";
        }

        /// <summary>
        /// API code that field is not datetime format.
        /// </summary>
        public static string NotDateTimeFormat
        {
            get => "FV3";
        }

        /// <summary>
        /// API code that file extension is not allowed.
        /// </summary>
        public static string FileExtensionUnavailable
        {
            get => "FF0";
        }

        /// <summary>
        /// API code that file size.
        /// </summary>
        public static string ExceedFileSize
        {
            get => "FF1";
        }
        
        /// <summary>
        /// API code that occurred unexpected error.
        /// </summary>
        public static string UnexpectedError
        {
            get => "F99";
        }
    }
}