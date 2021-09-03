using Microsoft.EntityFrameworkCore;

namespace SHS.Repositories
{
    /// <summary>
    /// UnitOfWork used to cowork with all of repositories.
    /// Ref. https://www.c-sharpcorner.com/UploadFile/b1df45/unit-of-work-in-repository-pattern/
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save all changes in DbContext, confirm one transaction.
        /// </summary>
        void SaveChanges();
        
        /// <summary>
        /// Give agent repository.
        /// </summary>
        IAgentRepo AgentRepo { get; set; }
    }
    
    /// <summary>
    /// Implement UnitOfWork function.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;

        /// <summary>
        /// Constructor of UnitOfWork.
        /// </summary>
        public UnitOfWork(
            DbContext dbContext,
            IAgentRepo agentRepo
            )
        {
            this._dbContext = dbContext;
            this.AgentRepo = agentRepo;
        }

        /// <summary>
        /// Save all changes in DbContext, confirm one transaction.
        /// </summary>
        public void SaveChanges()
        {
            this._dbContext.SaveChanges();
        }

        /// <summary>
        /// Give agent repository.
        /// </summary>
        public IAgentRepo AgentRepo { get; set; }
    }
}