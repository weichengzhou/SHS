using Microsoft.AspNetCore.Http;


namespace SHS.Models.Dtos
{
    public class ImportFileDto
    {
        public IFormFile ImportFile { get; set; }
    }
}