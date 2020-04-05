using FluentValidation;
using Timesheet.Core.Models.Projects;

namespace Timesheet.Api.Validators.Projects
{
    public class AddProjectModelValidator : AbstractValidator<AddProjectModel>
    {
        public AddProjectModelValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
        }
    }
}
