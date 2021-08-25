using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SHS.Services;
using SHS.Services.Exceptions;
using SHS.Models;
using SHS.Models.Dtos;


namespace SHS.Controllers
{
    /*  Controller for API which provide resource of agents.
    */
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class AgentApiController : ControllerBase
    {
        private IAgentService _agentService;
        private IExcelService _excelService;
        private ILogger _logger;

        public AgentApiController(
            IAgentService agentService,
            IExcelService excelService,
            ILogger<AgentApiController> logger
        )
        {
            this._agentService = agentService;
            this._excelService = excelService;
            this._logger = logger;
        }

        /*  Create agent if agent is not exist.
            Version v1d0 (v1.0)

            ResponseCode: S00 - Succeed, agent created.
                          F00 - Failed, agent is exist.
                          FC0 - Failed, agent data is not valid.
                          F99 - Failed, system occurred unexpected error.
        */
        [HttpPost("agent"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult CreateAgentV1d0([FromBody]AgentDto agentDto)
        {
            ShsResponse apiResponse = new ShsResponse();
            try
            {
                this._agentService.CreateAgent(agentDto);
                apiResponse.ResponseCode = "S00";
                apiResponse.Message = "業務員資料新增成功";
            }
            catch(ShsException error)
            {
                apiResponse.ResponseCode = error.Code;
                apiResponse.Message = error.Message;
                apiResponse.Results = error.Results;
            }
            catch(Exception error)
            {
                apiResponse.ResponseCode = "F99";
                apiResponse.Message = "系統發生錯誤";
                this._logger.LogError(error.Message);
            }
            return this.StatusCode(200, apiResponse);
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
            ShsResponse apiResponse = new ShsResponse();
            try
            {
                this._agentService.UpdateAgent(agentDto);
                apiResponse.ResponseCode = "S01";
                apiResponse.Message = "業務員資料更新完成";
            }
            catch(ShsException error)
            {
                apiResponse.ResponseCode = error.Code;
                apiResponse.Message = error.Message;
                apiResponse.Results = error.Results;
            }
            catch(Exception error)
            {
                apiResponse.ResponseCode = "F99";
                apiResponse.Message = "系統發生錯誤";
                this._logger.LogError(error.Message);
            }
            return this.StatusCode(200, apiResponse);
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
            ShsResponse apiResponse = new ShsResponse();
            try
            {
                IEnumerable<AgentDto> agentDtos = this._agentService.GetAllAgents();
                apiResponse.ResponseCode = "S02";
                apiResponse.Message = "取得所有業務員資料成功";
                apiResponse.Results = agentDtos;
            }
            catch(Exception error)
            {
                apiResponse.ResponseCode = "F99";
                apiResponse.Message = "系統發生錯誤";
                this._logger.LogError(error.Message);
            }
            return this.StatusCode(200, apiResponse);
        }

        /*  Import agent excel to system.
            Version v1d0 (v1.0)

            ResponseCode: S03 - Succeed, import data successfully.
                          FC0 - Failed, import file is not valid.
                          F99 - Failed, system occurred unexpected error.
        */
        [HttpPost("agents/importExcel"), MapToApiVersion("1.0")]
        [Produces("application/json")]
        public IActionResult ImportExcelV1d0([FromForm]ImportFileDto fileDto)
        {
            ShsResponse apiResponse = new ShsResponse();
            try
            {
                IEnumerable<AgentDto> agentDtos = this._excelService.GetAgentDtos(fileDto);
                this._agentService.CreateOrUpdateAgents(agentDtos);
                apiResponse.ResponseCode = "S03";
                apiResponse.Message = "上傳業務員檔案成功";
            }
            catch(ShsException error)
            {
                apiResponse.ResponseCode = error.Code;
                apiResponse.Message = error.Message;
                apiResponse.Results = error.Results;
            }
            catch(Exception error)
            {
                apiResponse.ResponseCode = "F99";
                apiResponse.Message = "系統發生錯誤";
                this._logger.LogError(error.Message);
            }
            return this.StatusCode(200, apiResponse);
        }
    }
}