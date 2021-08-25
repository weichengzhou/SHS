using FluentValidation;

using SHS.Models.Dtos;


namespace SHS.Services.Validators
{
    public class AgentDtoValidator : AbstractValidator<AgentDto>
    {
        public AgentDtoValidator()
        {
            this.RuleForName();
            this.RuleForIdNo();
            this.RuleForAgentNo();
            this.RuleForDob();
            this.RuleForEmail();
            this.RuleForCellPhone();
        }

        private void RuleForName()
        {
            this.RuleFor(agent => agent.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCode.IsEmpty)
                .WithMessage("請輸入業務員名稱");
            this.RuleFor(agent => agent.Name)
                .MaximumLength(10)
                .WithErrorCode(ErrorCode.ExceedLength)
                .WithMessage("業務員名稱請不超過10個字");
        }

        private void RuleForIdNo()
        {
            this.RuleFor(agent => agent.IdNo)
                .NotEmpty()
                .WithErrorCode(ErrorCode.IsEmpty)
                .WithMessage("請輸入業務員身份證字號");
            this.RuleFor(agent => agent.IdNo)
                .MaximumLength(10)
                .WithErrorCode(ErrorCode.ExceedLength)
                .WithMessage("業務員身份證字號請不超過10個字");
        }

        private void RuleForAgentNo()
        {
            this.RuleFor(agent => agent.AgentNo)
                .NotEmpty()
                .WithErrorCode(ErrorCode.IsEmpty)
                .WithMessage("請輸入業務員編號");
            this.RuleFor(agent => agent.AgentNo)
                .MaximumLength(10)
                .WithErrorCode(ErrorCode.ExceedLength)
                .WithMessage("業務員編號請不超過10個字");
        }

        private void RuleForDob()
        {
            this.RuleFor(agent => agent.Dob)
                .Must(ValidateStrRule.IsDateFormat)
                .WithErrorCode(ErrorCode.NotDateTimeFormat)
                .WithMessage("業務員生日請輸入日期格式");
        }

        private void RuleForEmail()
        {
            this.RuleFor(agent => agent.Email)
                .EmailAddress()
                .WithErrorCode(ErrorCode.NotEmailFormat)
                .WithMessage("電子信箱格式錯誤");
            this.RuleFor(agent => agent.Email)
                .MaximumLength(320)
                .WithErrorCode(ErrorCode.ExceedLength)
                .WithMessage("電子信箱請不超過320個字");
        }

        private void RuleForCellPhone()
        {
            this.RuleFor(agent => agent.CellPhone)
                .MaximumLength(11)
                .WithErrorCode(ErrorCode.ExceedLength)
                .WithMessage("行動電話請不超過11個字");
        }
    }
}