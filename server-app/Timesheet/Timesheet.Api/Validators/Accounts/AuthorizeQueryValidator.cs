namespace Timesheet.Api.Validators.Accounts
{
    using FluentValidation;
    using Timesheet.Core.Models.Queries.Accounts;

    public class AuthorizeQueryValidator : AbstractValidator<AuthorizeQuery>
    {
        public AuthorizeQueryValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
        }
    }
}
