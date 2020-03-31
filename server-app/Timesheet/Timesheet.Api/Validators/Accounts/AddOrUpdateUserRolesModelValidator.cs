namespace Timesheet.Api.Validators.Accounts
{
    using FluentValidation;
    using System.Linq;
    using Timesheet.Core.Models.Accounts;

    public class AddOrUpdateUserRolesModelValidator : AbstractValidator<AddOrUpdateUserRolesModel>
    {
        public AddOrUpdateUserRolesModelValidator()
        {
            RuleFor(x => x.RolesIds).NotNull().NotEmpty().Must(x => (bool)x?.Any());
        }
    }
}
