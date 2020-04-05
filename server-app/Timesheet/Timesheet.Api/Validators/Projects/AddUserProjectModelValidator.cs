using FluentValidation;
using Timesheet.Core.Models.Projects;

namespace Timesheet.Api.Validators.Projects
{
    public class AddUserProjectModelValidator : AbstractValidator<AddUserProjectModel>
    {
        public AddUserProjectModelValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0);
        }
    }
}
