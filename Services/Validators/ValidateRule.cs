using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace SHS.Services.Validators
{
    /// <summary>
    /// Customize the validation rule, check the field which type is string.
    /// </summary>
    public class ValidateStrRule
    {
        /// <summary>
        //// Check the value is date format.
        /// </summary>
        /// <param name="str">The value in field.</param>
        /// <returns>The value is date format or not.</param>
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

    /// <summary>
    /// Customize the validation rule, check the field which type if IFormFile.
    /// </summary>
    public class ValidateFileRule
    {
        /// <summary>
        /// Check the value is in allowed extensions.
        /// </summary>
        /// <param name="file">The value of field.</param>
        /// <param name="allowedExtensions">List of allowed extensions.</param>
        /// <returns>The value is in allowed extensions or not.</returns>
        public static bool IsAllowedExtensions(IFormFile file, List<string> allowedExtensions)
        {
            if(file == null)
                return true;
            string fileExtension = Path.GetExtension(file.FileName);
            string lowerCaseExtension = fileExtension.ToLower();
            return allowedExtensions.Contains(lowerCaseExtension);
        }

        /// <summary>
        /// Check the value is less or equals max size.
        /// </summary>
        /// <param name="file">The value of field.</param>
        /// <param name="maxSize">The max size of file.</param>
        /// <returns>The value is less or equals max size.</returns>
        public static bool MaxSize(IFormFile file, long maxSize)
        {
            if(file == null)
                return true;
            return file.Length <= maxSize;
        }
    }
}