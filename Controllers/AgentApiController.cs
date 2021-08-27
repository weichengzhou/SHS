using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SHS.Controllers.Exceptions;
using SHS.Services;
using SHS.Models;
using SHS.Models.Dtos;


namespace SHS.Controllers
{
    /// Controller for API which provide resource of agents.
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class AgentApiController : ControllerBase
    {
        private IAgentService _agentService;
        private IExcelService _excelService;
        private IApiExceptionHandler _exceptionHandler;
        private ILogger _logger;

        public AgentApiController(
            IAgentService agentService,
            IExcelService excelService,
            IApiExceptionHandler exceptionHandler,
            ILogger<AgentApiController> logger
        )
        {
            this._agentService = agentService;
            this._excelService = excelService;
            this._exceptionHandler = exceptionHandler;
            this._logger = logger;
        }

        /// <summary>
        /// Create agent if agent is not exist.
        /// </summary>
        /// <remarks>
        /// ### API Version : ###
        /// - 1.0
        /// ### Response Code : ###
        /// - S00 - Succeed, agent created.
        /// - F00 - Failed, agent is exist.
        /// - FC0 - Failed, agent data is not valid.
        /// - F99 - Failed, system occurred unexpected error.
        /// </remarks>
        [HttpPost("agent"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult CreateAgentV1d0([FromBody]AgentDto agentDto)
        {
            try
            {
                AgentDto createdAgentDto = this._agentService.CreateAgent(agentDto);
                ShsResponse apiResponse = new ShsResponse(){
                    ResponseCode = "S00",
                    Message = "業務員資料新增成功"
                };
                Uri createdUrl = new Uri($"{Request.Path}/{createdAgentDto.IdNo}");
                return this.Created(createdUrl, apiResponse);
            }
            catch(IException exception)
            {
                this._logger.LogError(exception.Message);
                IResponse apiResponse = this.GetErrorResponse(exception);
                return this.StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
            }
        }

        /*  Update agent if agent is exist.
            Version v1d0 (v1.0)

            ResponseCode: S01 - Succeed, agent updated.
                          F01 - Failed, agent not exists.
                          FC0 - Failed, agent data is not valid.
                          F99 - Failed, system occurred unexpected error.
        */
        [HttpPut("agent"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult UpdateAgentV1d0([FromBody]AgentDto agentDto)
        {
            try
            {
                ShsResponse apiResponse = new ShsResponse();
                this._agentService.UpdateAgent(agentDto);
                apiResponse.ResponseCode = "S01";
                apiResponse.Message = "業務員資料更新完成";
                return this.StatusCode(200, apiResponse);
            }
            catch(IException exception)
            {
                this._logger.LogError(exception.Message);
                IResponse apiResponse = this.GetErrorResponse(exception);
                return this.StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
            }
        }

        /*  Get all agents.
            Version v1d0 (v1.0)

            ResponseCode: S02 - Succeed, get all agents.
                          F99 - Failed, system occurred unexpected error.
        */
        [HttpGet("agents"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult GetAllAgentsV1d0()
        {
            try
            {
                ShsResponse apiResponse = new ShsResponse();
                IEnumerable<AgentDto> agentDtos = this._agentService.GetAllAgents();
                apiResponse.ResponseCode = "S02";
                apiResponse.Message = "取得所有業務員資料成功";
                apiResponse.Results = agentDtos;
                return this.StatusCode(200, apiResponse);
            }
            catch(IException exception)
            {
                this._logger.LogError(exception.Message);
                IResponse apiResponse = this.GetErrorResponse(exception);
                return this.StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
            }
        }

        [HttpGet("agent"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult GetAgentByIdNoV1d0(string idNo)
        {
            try
            {
                AgentDto agentDto = this._agentService.GetAgentByIdNo(idNo);
                ShsResponse apiResponse = new ShsResponse{
                    ResponseCode = "S02",
                    Message = "取得業務員資料成功",
                    Results = agentDto
                };
                return this.StatusCode((int)HttpStatusCode.OK, apiResponse);
            }
            catch(IException exception)
            {
                this._logger.LogError(exception.Message);
                IResponse apiResponse = this.GetErrorResponse(exception);
                return this.StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
            }
        }

        /// <summary>
        /// Import agent excel to system.
        /// </summary>
        /// <remarks>
        /// ### Version : ###
        /// - 1.0
        /// ### Response Code : ###
        /// - S03 - Succeed, import data successfully.
        /// - FC0 - Failed, import file is not valid.
        /// </remarks>
        [HttpPost("agents/import"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult ImportAgentsV1d0([FromForm]ImportFileDto fileDto)
        {
            try
            {
                IEnumerable<AgentDto> agentDtos = this._excelService.GetAgentDtos(fileDto);
                this._agentService.CreateOrUpdateAgents(agentDtos);
                ShsResponse apiResponse = new ShsResponse(){
                    ResponseCode = "S03",
                    Message = "上傳業務員檔案成功"
                };
                return this.StatusCode((int)HttpStatusCode.OK, apiResponse);
            }
            catch(IException exception)
            {
                this._logger.LogError(exception.Message);
                IResponse apiResponse = this.GetErrorResponse(exception);
                return this.StatusCode((int)HttpStatusCode.BadRequest, apiResponse);
            }
        }

        private IResponse GetErrorResponse(Exception exception)
        {
            return this._exceptionHandler.BuildErrorResponse(exception);
        }
    }
}