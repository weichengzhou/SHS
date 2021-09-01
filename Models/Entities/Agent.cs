using System;

namespace SHS.Models.Entities
{
    public class Agent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string IdNo { get; set; }

        public string AgentNo { get; set; }

        public DateTime? Dob { get; set; }

        public string Email { get; set; }
        
        public string CellPhone { get; set; }
    }
}
