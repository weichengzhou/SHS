using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AutoMapper;

using SHS.Services;
using SHS.Services.Exceptions;
using SHS.Models.ViewModels;
using SHS.Models.Dtos;


namespace SHS.Controllers
{
    /// <summary>
    /// Controller that decide which view will display and provide AgentViewModel.
    /// </summary>
    public class AgentController : Controller
    {
        private IAgentService _agentService;
        private IExcelService _excelService;
        private IMapper _mapper;
        private ILogger _logger;

        public AgentController(
            IAgentService agentService,
            IExcelService excelService,
            IMapper mapper,
            ILogger<AgentController> logger
        )
        {
            this._agentService = agentService;
            this._excelService = excelService;
            this._mapper = mapper;
            this._logger = logger;
        }

        /// <summary>
        /// Index of Agents page.
        /// 1. Provide the table that contains all agents.
        /// 2. Link to Detail page, Edit page links.
        /// 3. Has a button to create page.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            var agentViewModels = this.GetAgentViewModels();
            if(TempData["SuccessMessage"] != null)
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            if(TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            return View(agentViewModels);
        }

        /// <summary>
        /// Given basic create form, all fields are empty.
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        /// <summary>
        /// Post data to create form, call this function.
        /// 1. Try to create the agent.
        /// 2. return to index page.
        /// </summary>
        /// <param name="agentViewModel">The agent model will create.</param>
        [HttpPost]
        public IActionResult Create([FromForm]AgentViewModel agentViewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    AgentDto agentDto = this._mapper.Map<AgentDto>(agentViewModel);
                    this._agentService.CreateAgent(agentDto);
                    TempData["SuccessMessage"] = "業務員資料更新完成";
                }
            }
            catch(ShsException error)
            {
                TempData["ErrorMessage"] = error.Message;
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "系統發生錯誤";
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Given basic edit form.
        /// 1. Get the agent by given id.
        /// 2. Filled the data into form.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        [HttpGet]
        public IActionResult Edit(string idNo)
        {
            AgentDto agentDto = this._agentService.GetAgentByIdNo(idNo);
            var agentViewModel = this._mapper.Map<AgentViewModel>(agentDto);
            return PartialView(agentViewModel);
        }

        /// <summary>
        /// Post data to edit form, call this function.
        /// 1. Try to edit agent.
        /// 2. return to index page.
        /// </summary>
        /// <param name="agentViewModel">The agent model will update.</param>
        [HttpPost]
        public IActionResult Edit([FromForm]AgentViewModel agentViewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    AgentDto agentDto = this._mapper.Map<AgentDto>(agentViewModel);
                    this._agentService.UpdateAgent(agentDto);
                    TempData["SuccessMessage"] = "業務員資料更新完成";
                }
            }
            catch(ShsException error)
            {
                ViewBag.ErrorMessage = error.Message;
            }
            catch(Exception)
            {
                ViewBag.ErrorMessage = "系統發生錯誤";
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Given basic import excel form.
        /// </summary>
        [HttpGet]
        public IActionResult ImportExcel()
        {
            return PartialView();
        }

        /// <summary>
        /// Post excel file to form, call this function.
        /// 1. Try to get all agents data in excel.
        /// 2. Create agent if agent not exist,
        ///    Update agent if agent exist.
        /// </summary>
        /// <param name="fileViewModel">The file model which will import data to system.</param>
        [HttpPost]
        public IActionResult ImportExcel([FromForm]ExcelFileViewModel fileViewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    ExcelFileDto fileDto = this._mapper.Map<ExcelFileDto>(fileViewModel);
                    IEnumerable<AgentDto> agentDtos = this._excelService.GetAgentDtos(fileDto);
                    this._agentService.CreateOrUpdateAgents(agentDtos);
                    TempData["SuccessMessage"] = "上傳業務員檔案成功";
                }
            }
            catch(ShsException error)
            {
                TempData["ErrorMessage"] = error.Message;
            }
            catch(Exception)
            {
                TempData["ErrorMessage"] = "系統發生錯誤";
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Provide the agent data by id number.
        /// </summary>
        /// <param name="idNo">The id number of agent.</param>
        [HttpGet]
        public IActionResult Detail(string idNo)
        {
            AgentDto agentDto = this._agentService.GetAgentByIdNo(idNo);
            var agentViewModel = this._mapper.Map<AgentViewModel>(agentDto);
            return PartialView(agentViewModel);
        }

        /// <summary>
        /// Get all agents in system.
        /// </summary>
        /// <returns>List of agent which are in system.</returns>
        private IEnumerable<AgentViewModel> GetAgentViewModels()
        {
            IEnumerable<AgentDto> agentDtos = this._agentService.GetAllAgents();
            return this._mapper.Map<IEnumerable<AgentViewModel>>(agentDtos);
        }
    }
}