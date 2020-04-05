using FluentValidation;
using Timesheet.Core.Models.Projects;

namespace Timesheet.Api.Validators.Projects
{
    public class UpdateProjectModelValidator : AbstractValidator<UpdateProjectModel>
    {
        public UpdateProjectModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
        }
    }
}
