using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using SHS.Models.ValidAttributes;


namespace SHS.Models.ViewModels
{
    public class ImportFileViewModel
    {
        [Required(ErrorMessage = "請選擇上傳檔案")]
        [MaxSize]
        [AllowedExtensions(new string[]{".xlsx", ".xls"})]
        public IFormFile ImportFile { get; set; }
    }
}