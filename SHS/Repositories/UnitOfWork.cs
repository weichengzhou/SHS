using Microsoft.EntityFrameworkCore;

namespace SHS.Repositories
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        
        IAgentRepo AgentRepo { get; set; }
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        public UnitOfWork(
            DbContext dbContext,
            IAgentRepo agentRepo
            )
        {
            this._dbContext = dbContext;
            this.AgentRepo = agentRepo;
        }

        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
        }

        public IAgentRepo AgentRepo { get; set; }
    }
}