using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace SHS.Models.ValidAttributes
{
    public class MaxSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        /*  Defaults 5MB (5 * 1024 * 1024)
        */
        public MaxSizeAttribute(int maxSize=5242880)
        {
            this.MaxSize = maxSize;
        }

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

        public string GetErrorMessage()
        {
            return $"檔案不可超過{this.MaxSize} bytes.";
        }

        public int MaxSize { get; }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        private string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            this._allowedExtensions = allowedExtensions;
        }
        
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

        public string GetErrorMessage()
        {
            return $"檔案型別僅支援{this.AllowedExtensions}";
        }

        public string AllowedExtensions
        {
            get => string.Join(",", this._allowedExtensions);
        }
    }
}