using Microsoft.AspNetCore.Http;


namespace SHS.Models.Dtos
{
    public class ExcelFileDto
    {
        public IFormFile ExcelFile { get; set; }
    }
}