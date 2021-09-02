using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

using SHS.Controllers.Exceptions;
using SHS.Services;
using SHS.Services.Exceptions;
using SHS.Models;
using SHS.Models.Dtos;


namespace SHS.Controllers
{
    /// <summary>
    /// Controller for API which provide resource of agents.
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ControllerName("Agent")]
    [ApiController]
    public class AgentV1d0Controller : ControllerBase
    {
        private IAgentService _agentService;
        private IExcelService _excelService;
        private IApiExceptionHandler _exceptionHandler;
        private ILogger _logger;

        public AgentV1d0Controller(
            IAgentService agentService,
            IExcelService excelService,
            IApiExceptionHandler exceptionHandler,
            ILogger<AgentV1d0Controller> logger
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
                    ResponseCode = ApiResponseCode.CreateAgent,
                    Message = "業務員資料新增成功"
                };
                Uri createdUrl = new Uri($"{Request.Path}/{createdAgentDto.IdNo}");
                return this.Created(createdUrl, apiResponse);
            }
            catch(Exception exception)
            {
                this._logger.LogError(exception.Message);
                return this.GetErrorResponse(exception);
            }
        }

        /// <summary>
        /// Update agent if agent is exist.
        /// </summary>
        /// <remarks>
        /// ### Version : ###
        /// - 1.0
        /// ### Response Code : ###
        /// - S01 - Succeed, agent is updated.
        /// - F01 - Failed, agent not exists.
        /// - FC0 - Failed, agent data is not valid.
        /// - F99 - Failed, system occurred unexpected error.
        /// </remarks>
        [HttpPut("agent"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult UpdateAgentV1d0([FromBody]AgentDto agentDto)
        {
            try
            {
                ShsResponse apiResponse = new ShsResponse();
                this._agentService.UpdateAgent(agentDto);
                apiResponse.ResponseCode = ApiResponseCode.UpdateAgent;
                apiResponse.Message = "業務員資料更新完成";
                return this.StatusCode(200, apiResponse);
            }
            catch(Exception exception)
            {
                this._logger.LogError(exception.Message);
                return this.GetErrorResponse(exception);
            }
        }

        /// <summary>
        /// Get all agents.
        /// </summary>
        /// <remarks>
        /// ### Version : ###
        /// - 1.0
        /// ### Response Code : ###
        /// - S02 - Succeed, get all agents data.
        /// - F99 - Failed, system occurred unexpected error.
        /// </remarks>
        [HttpGet("agents"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult GetAllAgentsV1d0()
        {
            try
            {
                ShsResponse apiResponse = new ShsResponse();
                IEnumerable<AgentDto> agentDtos = this._agentService.GetAllAgents();
                apiResponse.ResponseCode = ApiResponseCode.GetAgents;
                apiResponse.Message = "取得所有業務員資料成功";
                apiResponse.Results = agentDtos;
                return this.StatusCode(200, apiResponse);
            }
            catch(Exception exception)
            {
                this._logger.LogError(exception.Message);
                return this.GetErrorResponse(exception);
            }
        }

        /// <summary>
        /// Get agent by id number.
        /// </summary>
        /// <remarks>
        /// ### Version : ###
        /// - 1.0
        /// ### Response Code : ###
        /// - S02 - Succeed, get agent data.
        /// - F99 - Failed, system occurred unexpected error.
        /// </remarks>
        [HttpGet("agent"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult GetAgentByIdNoV1d0(string idNo)
        {
            try
            {
                AgentDto agentDto = this._agentService.GetAgentByIdNo(idNo);
                ShsResponse apiResponse = new ShsResponse{
                    ResponseCode = ApiResponseCode.GetAgent,
                    Message = "取得業務員資料成功",
                    Results = agentDto
                };
                return this.StatusCode((int)HttpStatusCode.OK, apiResponse);
            }
            catch(Exception exception)
            {
                this._logger.LogError(exception.Message);
                return this.GetErrorResponse(exception);
            }
        }

        /// <summary>
        /// Import agent excel to system.
        /// </summary>
        /// <remarks>
        /// ### Version : ###
        /// - 1.0
        /// ### Response Code : ###
        /// - S03 - Succeed, import agents data successfully.
        /// - FC0 - Failed, agent data is not valid in file.
        /// - F99 - Failed, system occurred unexpected error.
        /// </remarks>
        [HttpPost("agents/import"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult ImportAgentsV1d0([FromForm]ExcelFileDto fileDto)
        {
            try
            {
                IEnumerable<AgentDto> agentDtos = this._excelService.GetAgentDtos(fileDto);
                this._agentService.CreateOrUpdateAgents(agentDtos);
                ShsResponse apiResponse = new ShsResponse(){
                    ResponseCode = ApiResponseCode.ImportAgents,
                    Message = "上傳業務員檔案成功"
                };
                return this.StatusCode((int)HttpStatusCode.OK, apiResponse);
            }
            catch(Exception exception)
            {
                this._logger.LogError(exception.Message);
                return this.GetErrorResponse(exception);
            }
        }

        /// <summary>
        /// Get error result by handler.
        /// </summary>
        /// <param name="exception">The exception which raised by system.</param>
        /// <returns>The api result which pack error message.</returns>
        private ObjectResult GetErrorResponse(Exception exception)
        {
            IResponse apiResponse = this._exceptionHandler.GetErrorResponse(exception);
            int httpCode = this._exceptionHandler.GetHttpCode(exception);
            return this.StatusCode(httpCode, apiResponse);
        }
    }
}