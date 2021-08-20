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

using SHS.Services.Exceptions;
using SHS.Models.Dto;


namespace SHS.Services
{
    public interface IExcelService
    {
        IEnumerable<AgentDto> GetAgentDtos(IFormFile file);
    }
    
    public class ExcelService : IExcelService
    {
        private List<string> _isAllowedExtension = new List<string>(){
            ".xls",
            ".xlsx"
        };
        private long _maxFileSize = 5 * 1024 * 1024;

        public IEnumerable<AgentDto> GetAgentDtos(IFormFile file)
        {
            this.ValidateFile(file);
            IWorkbook workbook = this.GetWorkbook(file);
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

        private void ValidateFile(IFormFile file)
        {
            if(file is null)
                throw new FileIsNullError("未傳輸任何檔案");
            string fileExtension = Path.GetExtension(file.FileName);
            if(!this.IsAllowedExtension(fileExtension))
            {
                string errorMessage = string.Format(
                    "檔案類型必須為{0}",
                    string.Join(",", this._isAllowedExtension.ToArray())
                );
                throw new FileExtensionError(errorMessage);
            }
            if(!this.IsAllowedSize(file.Length))
            {
                string errorMessage = string.Format(
                    "檔案大小必須小於{0}",
                    this.MaxFileSize
                );
                throw new FileSizeError(errorMessage);
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

        private bool IsAllowedExtension(string fileExtension)
        {
            string lowerCaseExtension = fileExtension.ToLower();
            return this._isAllowedExtension.Contains(lowerCaseExtension);
        }

        private bool IsAllowedSize(long fileSize)
        {
            if(fileSize > this.MaxFileSize)
            {
                return false;
            }
            return true;
        }

        public long MaxFileSize
        {
            get => this._maxFileSize;
            set => this._maxFileSize = value ;
        }
    }
}