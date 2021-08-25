using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace SHS.Services.Validators
{
    public class ValidateStrRule
    {
        public static bool IsDateFormat(string str)
        {
            if(str is null)
                return true;
            else if(str == "")
                return true;
            DateTime result;
            return DateTime.TryParse(str, out result);
        }
    }

    public class ValidateFileRule
    {
        public static bool IsAllowedExtension(IFormFile file, List<string> allowedExtensions)
        {
            if(file == null)
                return true;
            string fileExtension = Path.GetExtension(file.FileName);
            string lowerCaseExtension = fileExtension.ToLower();
            return allowedExtensions.Contains(lowerCaseExtension);
        }

        public static bool MaxSize(IFormFile file, long allowedSize)
        {
            if(file == null)
                return true;
            return file.Length <= allowedSize;
        }
    }
}