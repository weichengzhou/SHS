using System.Collections.Generic;
using FluentValidation;

using SHS.Services;
using SHS.Models.Dtos;


namespace SHS.Services.Validators
{
    /// <summary>
    /// Use FluentValidation to validate excel file DTO.
    /// </summary>
    public class ExcelFileDtoValidator : AbstractValidator<ExcelFileDto>
    {
        /// <summary>
        /// Define the validation rule of excel file DTO.
        /// </summary>
        public ExcelFileDtoValidator(List<string> allowedExtensions, long allowedSize=5242880)
        {
            this.RuleFor(file => file.ExcelFile)
                .NotEmpty()
                .WithErrorCode(ApiResponseCode.IsEmpty)
                .WithMessage("未傳輸任何檔案");
            this.RuleFor(file => file.ExcelFile)
                .Must(file => {
                    return ValidateFileRule.IsAllowedExtensions(file, allowedExtensions);
                })
                .WithErrorCode(ApiResponseCode.FileExtensionUnavailable)
                .WithMessage("檔案型別有誤");
            this.RuleFor(file => file.ExcelFile)
                .Must(file => {
                    return ValidateFileRule.MaxSize(file, allowedSize);
                })
                .WithErrorCode(ApiResponseCode.ExceedFileSize)
                .WithMessage("檔案過大");
        }
    }
}