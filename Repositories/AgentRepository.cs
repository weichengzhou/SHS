using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using SHS.Models.Entities;


namespace SHS.Repositories
{
    public interface IAgentRepo
    {
        void CreateAgent(Agent agent);

        void UpdateAgentByIdNo(Agent agent);

        Agent GetAgentByIdNo(string idNo);

        IEnumerable<Agent> GetAllAgents();
        
        bool IsAgentExistByIdNo(string idNo);
    }

    public class AgentRepo : IAgentRepo
    {
        private DbContext _dbContext;

        public AgentRepo(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreateAgent(Agent agent)
        {
            this.Agents.Add(agent);
        }

        public void UpdateAgentByIdNo(Agent agent)
        {
            // 用身份證字號找到原本的業務員資料複製其流水號
            Agent oldAgent = this.GetAgentByIdNo(agent.IdNo);
            agent.Id = oldAgent.Id;
            this.Agents.Update(agent);
        }

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

        public IEnumerable<Agent> GetAllAgents()
        {
            return this.Agents.ToList();
        }

        public bool IsAgentExistByIdNo(string idNo)
        {
            return this.GetAgentByIdNo(idNo) != null;
        }

        private DbSet<Agent> Agents
        {
            get => this._dbContext.Set<Agent>();
        }
    }
}