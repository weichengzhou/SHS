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
    /*  Controller that decide which view will display and provide AgentViewModel.
    */
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

        /*  Index of Agents page.
            1. Provide the table that contains all agents.
            2. Link to Detail page, Edit page links.
            3. Has a button to create page.
        */
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

        /*  Given basic create form, all fields are empty.
        */
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        /*  Post data to create form, call this function.
            1. Try to create the agent.
            2. return to index page.
        */
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

        /*  Given basic edit form.
            1. Get the agent by given id.
            2. Filled the data into form.
        */
        [HttpGet]
        public IActionResult Edit(string idNo)
        {
            AgentDto agentDto = this._agentService.GetAgentByIdNo(idNo);
            var agentViewModel = this._mapper.Map<AgentViewModel>(agentDto);
            return PartialView(agentViewModel);
        }

        /*  Post data to edit form, call this function.
            1. Try to edit agent.
            2. return to index page.
        */
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

        /*  Given basic import excel form.
        */
        [HttpGet]
        public IActionResult ImportExcel()
        {
            return PartialView();
        }

        /*  Post excel file to form, call this function.
            1. Try to get all agents data in excel.
            2. Create agent if agent not exist,
               Update agent if agent exist.
        */
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

        /*  Provide the agent data by id number.
        */
        [HttpGet]
        public IActionResult Detail(string idNo)
        {
            AgentDto agentDto = this._agentService.GetAgentByIdNo(idNo);
            var agentViewModel = this._mapper.Map<AgentViewModel>(agentDto);
            return PartialView(agentViewModel);
        }

        private IEnumerable<AgentViewModel> GetAgentViewModels()
        {
            IEnumerable<AgentDto> agentDtos = this._agentService.GetAllAgents();
            return this._mapper.Map<IEnumerable<AgentViewModel>>(agentDtos);
        }
    }
}