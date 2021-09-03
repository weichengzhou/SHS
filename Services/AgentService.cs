using System.Collections.Generic;
using AutoMapper;
using FluentValidation.Results;

using SHS.Services.Validators;
using SHS.Services.Exceptions;
using SHS.Repositories;
using SHS.Models.Dtos;
using SHS.Models.Entities;


namespace SHS.Services
{
    /// <summary>
    /// Interface of agent service.
    /// </summary>
    public interface IAgentService
    {
        /// <summary>
        /// Create agent by given data.
        /// </summary>
        /// <param name="agentDto">Agent DTO.</param>
        /// <returns>Agent which is created.</returns>
        AgentDto CreateAgent(AgentDto agentDto);

        /// <summary>
        /// Update agent by specific id number.
        /// </summary>
        /// <param name="agentDto">Agent DTO contains id number.</param>
        void UpdateAgent(AgentDto agentDto);

        /// <summary>
        /// Foreach agent in params, create agent if agent is non-exist.
        /// Otherwise, update agent.
        /// </summary>
        /// <param name="agentDtos">List of agents.</param>
        void CreateOrUpdateAgents(IEnumerable<AgentDto> agentDtos);

        /// <summary>
        /// Get all agents.
        /// </summary>
        /// <returns>List of agents in system.</returns>
        IEnumerable<AgentDto> GetAllAgents();
        
        /// <summary>
        /// Get agent by specific id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        /// <returns>The agent which id number is given.</returns>
        AgentDto GetAgentByIdNo(string idNo);
    }

    /// <summary>
    /// Implement agent service.
    /// </summary>
    public class AgentService : IAgentService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        /// <summary>
        /// Constructor of agent service.
        /// </summary>
        /// <param name="unitOfWork">The pattern used to cowork repositories.</param>
        /// <param name="mapper">The auto mapper used to reflect objects.</param>
        public AgentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        /// <summary>
        /// Create agent by given data.
        /// </summary>
        /// <param name="agentDto">Agent DTO.</param>
        /// <returns>Agent which is created.</returns>
        public AgentDto CreateAgent(AgentDto agentDto)
        {
            this.ValidateAgentDto(agentDto);
            Agent agent = this._mapper.Map<Agent>(agentDto);
            bool isAgentExist = this.AgentRepo.IsAgentExistByIdNo(agent.IdNo);
            if(isAgentExist)
            {
                string errorMessage = string.Format("業務員{0}已存在", agent.IdNo);
                throw new AgentExistsError(errorMessage);
            }
            this.AgentRepo.CreateAgent(agent);
            this._unitOfWork.SaveChanges();
            return this._mapper.Map<AgentDto>(agent);
        }

        /// <summary>
        /// Update agent by specific id number.
        /// </summary>
        /// <param name="agentDto">Agent DTO.</param>
        public void UpdateAgent(AgentDto agentDto)
        {
            this.ValidateAgentDto(agentDto);
            Agent agent = this._mapper.Map<Agent>(agentDto);
            bool isAgentExist = this.AgentRepo.IsAgentExistByIdNo(agent.IdNo);
            if(!isAgentExist)
            {
                string errorMessage = string.Format("找不到業務員{0}", agent.IdNo);
                throw new AgentNotFoundError(errorMessage);
            }
            this.AgentRepo.UpdateAgentByIdNo(agent);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Foreach agent in params, create agent if agent is non-exist.
        /// Otherwise, update agent.
        /// </summary>
        /// <param name="agentDtos">List of agents.</param>
        public void CreateOrUpdateAgents(IEnumerable<AgentDto> agentDtos)
        {
            foreach(AgentDto agentDto in agentDtos)
            {
                this.ValidateAgentDto(agentDto);
                Agent agent = this._mapper.Map<Agent>(agentDto);
                bool isAgentExist = this.AgentRepo.IsAgentExistByIdNo(agent.IdNo);
                if(isAgentExist)
                    this.AgentRepo.UpdateAgentByIdNo(agent);
                else
                    this.AgentRepo.CreateAgent(agent);
            }
            this._unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Get all agents.
        /// </summary>
        /// <returns>List of agents in system.</returns>
        public IEnumerable<AgentDto> GetAllAgents()
        {
            IEnumerable<Agent> agents = this.AgentRepo.GetAllAgents();
            return this._mapper.Map<IEnumerable<AgentDto>>(agents);
        }

       /// <summary>
        /// Get agent by specific id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        /// <returns>The agent which id number is given.</returns>
        public AgentDto GetAgentByIdNo(string idNo)
        {
            bool isAgentExist = this.AgentRepo.IsAgentExistByIdNo(idNo);
            if(!isAgentExist)
            {
                string errorMessage = string.Format("找不到業務員{0}", idNo);
                throw new AgentNotFoundError(errorMessage);
            }
            Agent agent = this.AgentRepo.GetAgentByIdNo(idNo);
            return this._mapper.Map<AgentDto>(agent);
        }

        /// <summary>
        /// Validate fields of agent DTO.
        /// </summary>
        /// <param name="agentDto">The data of agent.</param>
        private void ValidateAgentDto(AgentDto agentDto)
        {
            AgentDtoValidator validator = new AgentDtoValidator();
            ValidationResult results = validator.Validate(agentDto);
            if(!results.IsValid)
            {
                throw new ValidationError("業務員資料驗證失敗", results.Errors);
            }
        }

        /// <summary>
        /// Get agent repository in DbContext.
        /// </summary>
        private IAgentRepo AgentRepo
        {
            get => this._unitOfWork.AgentRepo;
        }
    }
}