/*  Excel service to provide other services to access excel.
    
    1. import excel
*/
using System.IO;
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
    public interface IExcelService
    {
        IEnumerable<AgentDto> GetAgentDtos(ImportFileDto fileDto);
    }
    
    public class ExcelService : IExcelService
    {
        private List<string> _isAllowedExtensions = new List<string>(){
            ".xls",
            ".xlsx"
        };
        private long _maxFileSize = 5 * 1024 * 1024;

        public IEnumerable<AgentDto> GetAgentDtos(ImportFileDto fileDto)
        {
            this.ValidateFileDto(fileDto);
            IWorkbook workbook = this.GetWorkbook(fileDto.ImportFile);
            Mapper mapper = new Mapper(workbook);
            mapper.Map<AgentDto>("姓名", agent => agent.Name)
                  .Map<AgentDto>("身份字號", agent => agent.IdNo)
                  .Map<AgentDto>("業務編號", agent => agent.AgentNo)
                  .Map<AgentDto>("出生日期", agent => agent.Dob)
                  .Map<AgentDto>("Email", agent => agent.Email)
                  .Map<AgentDto>("行動電話", agent => agent.CellPhone);
            var rowInfos = mapper.Take<AgentDto>().ToList();
            List<AgentDto> agentDtos = new List<AgentDto>();
            foreach(RowInfo<AgentDto> rowInfo in rowInfos)
            {
                agentDtos.Add(rowInfo.Value);
            }
            return agentDtos;
        }

        private void ValidateFileDto(ImportFileDto fileDto)
        {
            ImportFileDtoValidator validator = new ImportFileDtoValidator(
                this._isAllowedExtensions,
                this.MaxFileSize
            );
            ValidationResult results = validator.Validate(fileDto);
            if(!results.IsValid)
            {
                throw new ValidationError("上傳檔案驗證失敗", results.Errors);
            }
        }

        /*  Import excel to system and database.
        */
        private IWorkbook GetWorkbook(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            Stream stream = file.OpenReadStream();
            stream.Position = 0;
            return this.GetWorkbook(stream, fileExtension);
        }

        private IWorkbook GetWorkbook(Stream stream, string fileExtension)
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

        public long MaxFileSize
        {
            get => this._maxFileSize;
            set => this._maxFileSize = value ;
        }
    }
}