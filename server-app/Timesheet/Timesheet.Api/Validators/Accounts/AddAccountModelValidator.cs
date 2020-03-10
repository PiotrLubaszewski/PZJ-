namespace Timesheet.Api.Validators.Accounts
{
    using FluentValidation;
    using Timesheet.Core.Models.Accounts;

    public class AddAccountModelValidator : AbstractValidator<AddAccountModel>
    {
        public AddAccountModelValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
        }
    }
}
