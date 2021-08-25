using System.Collections.Generic;
using FluentValidation;

using SHS.Models.Dtos;


namespace SHS.Services.Validators
{
    public class ImportFileDtoValidator : AbstractValidator<ImportFileDto>
    {
        public ImportFileDtoValidator(List<string> allowedExtensions, long allowedSize=5242880)
        {
            this.RuleFor(file => file.ImportFile)
                .NotEmpty()
                .WithErrorCode(ErrorCode.IsEmpty)
                .WithMessage("未傳輸任何檔案");
            this.RuleFor(file => file.ImportFile)
                .Must(file => {
                    return ValidateFileRule.IsAllowedExtension(file, allowedExtensions);
                })
                .WithErrorCode(ErrorCode.FileExtensionUnavailable)
                .WithMessage("檔案型別有誤");
            this.RuleFor(file => file.ImportFile)
                .Must(file => {
                    return ValidateFileRule.MaxSize(file, allowedSize);
                })
                .WithErrorCode(ErrorCode.ExceedFileSize)
                .WithMessage("檔案過大");
        }
    }
}