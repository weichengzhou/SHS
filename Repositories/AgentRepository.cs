using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using SHS.Models.Entities;


namespace SHS.Repositories
{
    /// <summary>
    /// The interface of agent repository.
    /// </summary>
    public interface IAgentRepo
    {
        /// <summary>
        /// Create agent by given data.
        /// </summary>
        /// <param name="agent">The data of agent.</param>
        void CreateAgent(Agent agent);

        /// <summary>
        /// Update agent by specific id number.
        /// </summary>
        /// <param name="agent">Agent data contains id number.</param>
        void UpdateAgentByIdNo(Agent agent);

        /// <summary>
        /// Get agent by specific id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        /// <returns>The agent which id number is given.</returns>
        Agent GetAgentByIdNo(string idNo);

        /// <summary>
        /// Get all agents.
        /// </summary>
        /// <returns>List of agents in system.</returns>
        IEnumerable<Agent> GetAllAgents();
        
        /// <summary>
        /// Check agent is exist by id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        bool IsAgentExistByIdNo(string idNo);
    }

    /// <summary>
    /// Implement agent repository.
    /// </summary>
    public class AgentRepo : IAgentRepo
    {
        private DbContext _dbContext;

        /// <summary>
        /// The construct of agent repository.
        /// </summary>
        public AgentRepo(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Create agent by given data.
        /// </summary>
        /// <param name="agent">The data of agent.</param>
        public void CreateAgent(Agent agent)
        {
            this.Agents.Add(agent);
        }

        /// <summary>
        /// Update agent by specific id number.
        /// </summary>
        /// <param name="agent">Agent data contains id number.</param>
        public void UpdateAgentByIdNo(Agent agent)
        {
            // Use id number to find agent.
            Agent oldAgent = this.GetAgentByIdNo(agent.IdNo);
            // Copy id (the sequence of table) to agent.
            agent.Id = oldAgent.Id;
            this.Agents.Update(agent);
        }

        /// <summary>
        /// Get agent by specific id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        /// <returns>The agent which id number is given.</returns>
        public Agent GetAgentByIdNo(string idNo)
        {
            Agent agent = this.Agents
                .Where(entity => entity.IdNo == idNo)
                .FirstOrDefault();
            // If found agent, detached it.
            if(agent != null)
                this._dbContext.Entry(agent).State = EntityState.Detached;
            return agent;
        }

        /// <summary>
        /// Get all agents.
        /// </summary>
        /// <returns>List of agents in system.</returns>
        public IEnumerable<Agent> GetAllAgents()
        {
            return this.Agents.ToList();
        }

        /// <summary>
        /// Check agent is exist by id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        public bool IsAgentExistByIdNo(string idNo)
        {
            return this.GetAgentByIdNo(idNo) != null;
        }

        /// <summary>
        /// Get set of agents in DbContext.
        /// </summary>
        private DbSet<Agent> Agents
        {
            get => this._dbContext.Set<Agent>();
        }
    }
}