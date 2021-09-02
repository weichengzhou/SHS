using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace SHS.Models.ValidAttributes
{
    /// <summary>
    /// The validator attribute, check the field less than size limit.
    /// </summary>
    public class MaxSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Defaults 5MB (5 * 1024 * 1024)
        /// </summary>
        /// <param name=maxSize>The maximum of file size.</param>
        public MaxSizeAttribute(int maxSize=5242880)
        {
            this.MaxSize = maxSize;
        }

        /// <summary>
        /// Validate the file is less than MaxSize.
        /// </summary>
        /// <param name="value">The value of field.</param>
        /// <param name="validationContext"></param>
        /// <returns>Result of validation.</returns>
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if(file != null)
            {
                if(file.Length > this.MaxSize)
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Add validation attribute to field.
        /// The html component will have this tag.
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            AttributeMethod.MergeAttribute(context.Attributes, "data-val", "true");
            AttributeMethod.MergeAttribute(
                context.Attributes,
                "data-val-maxsize",
                this.GetErrorMessage()
            );
            AttributeMethod.MergeAttribute(
                context.Attributes,
                "data-val-maxsize-maxsize",
                this.MaxSize.ToString()
            );
        }

        /// <summary>
        /// Get message if validate error.
        /// </summary>
        /// <returns>The string of error message.</returns>
        public string GetErrorMessage()
        {
            return $"檔案不可超過{this.MaxSize} bytes.";
        }

        /// <summary>
        /// The max size of file.
        /// </summary>
        public int MaxSize { get; }
    }

    /// <summary>
    /// The validator attribute, check the field extensions is in allowed extensions. 
    /// </summary>
    public class AllowedExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        private string[] _allowedExtensions;

        /// <summary>
        /// Initialize AllowedExtensionsAttribute with allowed extensions.
        /// </summary>
        /// <param name="allowedExtensions">The file extensions are allowed.</param>
        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            this._allowedExtensions = allowedExtensions;
        }
        
        /// <summary>
        /// Validate file extension is allowed.
        /// </summary>
        /// <param name="value">The value of field.</param>
        /// <param name="validationContext"></param>
        /// <returns>Result of validation.</returns>
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if(file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if(!this._allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Add validation attribute to field.
        /// The html component will have this tag.
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            AttributeMethod.MergeAttribute(context.Attributes, "data-val", "true");
            AttributeMethod.MergeAttribute(
                context.Attributes,
                "data-val-allowedextensions",
                this.GetErrorMessage()
            );
            AttributeMethod.MergeAttribute(
                context.Attributes,
                "data-val-allowedextensions-allowedextensions",
                this.AllowedExtensions
            );
        }

        /// <summary>
        /// Get message if validate error.
        /// </summary>
        /// <returns>The string of error message.</returns>
        public string GetErrorMessage()
        {
            return $"檔案型別僅支援{this.AllowedExtensions}";
        }

        /// <summary>
        /// The string combined by allowed extensions.
        /// </summary>
        public string AllowedExtensions
        {
            get => string.Join(",", this._allowedExtensions);
        }
    }
}