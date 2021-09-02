namespace SHS.Models.Dtos
{
    /// <summary>
    /// Data transfer object of agent.
    /// </summary>
    public class AgentDto
    {
        /// <summary>
        /// Name of agent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Identity number.
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        /// Agent number.
        /// </summary>
        public string AgentNo { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public string Dob { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Cell phone number.
        /// </summary>
        public string CellPhone { get; set; }
    }
}