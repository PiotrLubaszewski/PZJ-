using FluentValidation;
using Timesheet.Core.Models.ProjectTasks;

namespace Timesheet.Api.Validators.Projects
{
    public class AddProjectTaskModelValidator : AbstractValidator<AddProjectTaskModel>
    {
        public AddProjectTaskModelValidator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(3);
        }
    }
}
