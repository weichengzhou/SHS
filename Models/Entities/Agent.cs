using System;

namespace SHS.Models.Entities
{
    /// <summary>
    /// Agent model.
    /// Db table : agent
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Id of table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of agent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// National identification number
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        /// Agent number.
        /// </summary>
        public string AgentNo { get; set; }

        /// <summary>
        /// Date of birth.
        /// </summary>
        public DateTime? Dob { get; set; }

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
