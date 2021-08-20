using System.Collections.Generic;
using AutoMapper;
using FluentValidation.Results;

using SHS.Services.Validators;
using SHS.Services.Exceptions;
using SHS.Repositories;
using SHS.Models.Dto;
using SHS.Models.Entities;


namespace SHS.Services
{
    public interface IAgentService
    {
        void CreateAgent(AgentDto agentDto);

        void UpdateAgent(AgentDto agentDto);

        void CreateOrUpdateAgents(IEnumerable<AgentDto> agentDtos);

        IEnumerable<AgentDto> GetAllAgents();

        AgentDto GetAgentByIdNo(string idNo);
    }

    public class AgentService : IAgentService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AgentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public void CreateAgent(AgentDto agentDto)
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
        }

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

        public IEnumerable<AgentDto> GetAllAgents()
        {
            IEnumerable<Agent> agents = this.AgentRepo.GetAllAgents();
            return this._mapper.Map<IEnumerable<AgentDto>>(agents);
        }

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

        /*  驗證業務員DTO物件

            :param AgentDto agentDto: 業務員 DTO
            :raise ValidationError
        */
        private void ValidateAgentDto(AgentDto agentDto)
        {
            AgentDtoValidator validator = new AgentDtoValidator();
            ValidationResult results = validator.Validate(agentDto);
            if(!results.IsValid)
            {
                throw new ValidationError("業務員資料驗證失敗", results.Errors);
            }
        }

        private IAgentRepo AgentRepo
        {
            get => this._unitOfWork.AgentRepo;
        }
    }
}