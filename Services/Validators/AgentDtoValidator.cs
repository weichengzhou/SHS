using FluentValidation;

using SHS.Services;
using SHS.Models.Dtos;


namespace SHS.Services.Validators
{
    /// <summary>
    /// Use FluentValidation to validate agent DTO.
    /// </summary>
    public class AgentDtoValidator : AbstractValidator<AgentDto>
    {
        /// <summary>
        /// Define the validation rule of agent DTO.
        /// </summary>
        public AgentDtoValidator()
        {
            this.RuleForName();
            this.RuleForIdNo();
            this.RuleForAgentNo();
            this.RuleForDob();
            this.RuleForEmail();
            this.RuleForCellPhone();
        }

        /// <summary>
        /// Define the validation rule of name.
        /// </summary>
        private void RuleForName()
        {
            this.RuleFor(agent => agent.Name)
                .NotEmpty()
                .WithErrorCode(ApiResponseCode.IsEmpty)
                .WithMessage("請輸入業務員名稱");
            this.RuleFor(agent => agent.Name)
                .MaximumLength(10)
                .WithErrorCode(ApiResponseCode.ExceedLength)
                .WithMessage("業務員名稱請不超過10個字");
        }

        /// <summary>
        /// Define the validation rule of id number.
        /// </summary>
        private void RuleForIdNo()
        {
            this.RuleFor(agent => agent.IdNo)
                .NotEmpty()
                .WithErrorCode(ApiResponseCode.IsEmpty)
                .WithMessage("請輸入業務員身份證字號");
            this.RuleFor(agent => agent.IdNo)
                .MaximumLength(10)
                .WithErrorCode(ApiResponseCode.ExceedLength)
                .WithMessage("業務員身份證字號請不超過10個字");
        }

        /// <summary>
        /// Define the validation rule of agent number.
        /// </summary>
        private void RuleForAgentNo()
        {
            this.RuleFor(agent => agent.AgentNo)
                .NotEmpty()
                .WithErrorCode(ApiResponseCode.IsEmpty)
                .WithMessage("請輸入業務員編號");
            this.RuleFor(agent => agent.AgentNo)
                .MaximumLength(10)
                .WithErrorCode(ApiResponseCode.ExceedLength)
                .WithMessage("業務員編號請不超過10個字");
        }

        /// <summary>
        /// Define the validation rule of date of birth.
        /// </summary>
        private void RuleForDob()
        {
            this.RuleFor(agent => agent.Dob)
                .Must(ValidateStrRule.IsDateFormat)
                .WithErrorCode(ApiResponseCode.NotDateTimeFormat)
                .WithMessage("業務員生日請輸入日期格式");
        }

        /// <summary>
        /// Define the validation rule of email address.
        /// </summary>
        private void RuleForEmail()
        {
            this.RuleFor(agent => agent.Email)
                .EmailAddress()
                .WithErrorCode(ApiResponseCode.NotEmailFormat)
                .WithMessage("電子信箱格式錯誤");
            this.RuleFor(agent => agent.Email)
                .MaximumLength(320)
                .WithErrorCode(ApiResponseCode.ExceedLength)
                .WithMessage("電子信箱請不超過320個字");
        }

        /// <summary>
        /// Define the validation rule of cell phone number.
        /// </summary>
        private void RuleForCellPhone()
        {
            this.RuleFor(agent => agent.CellPhone)
                .MaximumLength(11)
                .WithErrorCode(ApiResponseCode.ExceedLength)
                .WithMessage("行動電話請不超過11個字");
        }
    }
}