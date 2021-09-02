using Microsoft.AspNetCore.Http;


namespace SHS.Models.Dtos
{
    /// <summary>
    /// Data transfer object of excel file.
    /// </summary>
    public class ExcelFileDto
    {
        /// <summary>
        /// The excel file that will import to system.
        /// </summary>
        public IFormFile ExcelFile { get; set; }
    }
}