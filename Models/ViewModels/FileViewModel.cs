using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using SHS.Models.ValidAttributes;


namespace SHS.Models.ViewModels
{
    /// <summary>
    /// The ViewModel of excel file.
    /// </summary>
    public class ExcelFileViewModel
    {
        /// <summary>
        /// The excel file which < 5MB and extensions is xlsx or xls.
        /// </summary>
        [Required(ErrorMessage = "請選擇上傳檔案")]
        [MaxSize]
        [AllowedExtensions(new string[]{".xlsx", ".xls"})]
        public IFormFile ExcelFile { get; set; }
    }
}