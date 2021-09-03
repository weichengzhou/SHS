/*  Excel service to provide other services to access excel.
    
    1. import excel
*/
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Npoi.Mapper;
using FluentValidation.Results;

using SHS.Services.Exceptions;
using SHS.Services.Validators;
using SHS.Models.Dtos;


namespace SHS.Services
{
    /// <summary>
    /// Interface of excel service.
    /// </summary>
    public interface IExcelService
    {
        /// <summary>
        /// Get list of agents data from excel.
        /// </summary>
        /// <param name="fileDto">Excel file.</param>
        /// <param name="sheetIndex">Index of sheet which conatins agents data.</param>
        /// <returns>List of agents data in sheet.</returns>
        IEnumerable<AgentDto> GetAgentDtos(ExcelFileDto fileDto, int sheetIndex=0);
    }
    
    /// <summary>
    /// Implement excel service.
    /// </summary>
    public class ExcelService : IExcelService
    {
        private List<string> _isAllowedExtensions = new List<string>(){
            ".xls",
            ".xlsx"
        };
        private long _maxFileSize = 5 * 1024 * 1024;

        /// <summary>
        /// Get list of agents data from excel.
        /// </summary>
        /// <param name="fileDto">Excel file.</param>
        /// <param name="sheetIndex">Index of sheet which conatins agents data.</param>
        /// <returns>List of agents data in sheet.</returns>
        public IEnumerable<AgentDto> GetAgentDtos(ExcelFileDto fileDto, int sheetIndex=0)
        {
            this.ValidateFileDto(fileDto);
            IWorkbook workbook = this.GetWorkbook(fileDto.ExcelFile);
            Mapper mapper = new Mapper(workbook);
            mapper.Map<AgentDto>("姓名", agent => agent.Name)
                  .Map<AgentDto>("身份字號", agent => agent.IdNo)
                  .Map<AgentDto>("業務編號", agent => agent.AgentNo)
                  .Map<AgentDto>("出生日期", agent => agent.Dob)
                  .Map<AgentDto>("Email", agent => agent.Email)
                  .Map<AgentDto>("行動電話", agent => agent.CellPhone);
            var rowInfos = mapper.Take<AgentDto>().ToList();
            List<AgentDto> agentDtos = new List<AgentDto>();
            foreach(var rowInfo in rowInfos)
            {
                agentDtos.Add(rowInfo.Value);
            }
            return agentDtos;
        }

        /// <summary>
        /// Validate file DTO.
        /// </summary>
        /// <param name="fileDto">Excel file DTO.</param>
        private void ValidateFileDto(ExcelFileDto fileDto)
        {
            ExcelFileDtoValidator validator = new ExcelFileDtoValidator(
                this._isAllowedExtensions,
                this.MaxFileSize
            );
            ValidationResult results = validator.Validate(fileDto);
            if(!results.IsValid)
            {
                throw new ValidationError("上傳檔案驗證失敗", results.Errors);
            }
        }

        /// <summary>
        /// Get workbook by given file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Excel workbook defined by Npoi.</returns>
        private IWorkbook GetWorkbook(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            Stream stream = file.OpenReadStream();
            stream.Position = 0;
            return this.GetWorkbookByExtension(stream, fileExtension);
        }

        /// <summary>
        /// Get different workbook by file extension.
        /// </summary>
        /// <param name="stream">File stream read from excel.</param>
        /// <param name="fileExtension">File extension of excel.</param>
        private IWorkbook GetWorkbookByExtension(Stream stream, string fileExtension)
        {
            switch(fileExtension)
            {
                case ".xls":
                    return new HSSFWorkbook(stream);
                case ".xlsx":
                    return new XSSFWorkbook(stream);
                default:
                    return null;
            }
        }

        /// <summary>
        /// The max size of file allowed.
        /// </summary>
        public long MaxFileSize
        {
            get => this._maxFileSize;
            set => this._maxFileSize = value ;
        }
    }
}